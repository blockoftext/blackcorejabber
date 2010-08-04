using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
namespace BlackCoreJabber
{
   
    public class User
    {
        public static List<User> userCache;    

        //basic info
        public string username;
        public string password;   
        public string streamid;

        //corp and api info
        public int corpid;
        int allianceid;
        string userapiid;
        string userapikey;
        int dbid;

        public bool isConnected = false;

        //presence
        public bool hasPresence = false;
        public string currentPresence = "";
        public string currentStatus = "";
        public DateTime presenceTime = DateTime.MinValue;

        public List<Resource> activeResourceList = new List<Resource>();
        
        public User() { }

        public User(int dbid, string username, string password, int corpid, int allianceid, string userapiid, string userapikey)
        {
            this.dbid = dbid;
            this.username = username;
            this.password = password;
            this.corpid = corpid;
            this.allianceid = allianceid;
            this.userapiid = userapiid;
            this.userapikey = userapikey;
        }

        public static List<string[]> getUserDetailList()
        {
            List<string[]> corpList = Program.userDatabase.getMoreResult("select * from BlackCore.user");
            return corpList;
        }

        #region Static Methods
        public static bool loadUsers()
        {
            userCache = new List<User>();
            List<string[]> listing = User.getUserDetailList();
            if (listing.Count == 0) return false;
            foreach (string[] s in listing)
            {  
                User temp = new User(int.Parse(s[0]), s[1], s[2], int.Parse(s[3]), int.Parse(s[4]), s[5], s[6]);
                userCache.Add(temp);
            }
            return true;

        }

        public static User getUserByUsername(string username)
        {
            foreach (User u in userCache)
            {
                if(u.username.Equals(username))
                {
                    return u;
                }
            }
            return null;
        }

        public static void updateTable(DataGridView table)
        {
            table.Rows.Clear();
            foreach (User a in userCache)
            {
                string[] tempstring = new string[6];
                tempstring[0] = a.dbid.ToString();
                tempstring[1] = a.username;
                tempstring[2] = Corperation.getCorpNameByID(a.corpid);
                tempstring[3] = Alliance.getAllianceNameByID(a.allianceid);
                tempstring[4] = a.userapiid;
                tempstring[5] = a.userapikey;
                table.Rows.Add(tempstring);
            }
        }

        public static void addUserToDatabase(string username, string password, int corpid, int allianceid, int apiid, string apikey)
        {
            string dbstring = "INSERT INTO `blackcore`.`user` (`username`, `password`, `corpid`, `allianceid`, `eveapiid`, `eveapikey`) VALUES ('" + username + "', '" + Program.CalculateMD5Hash(password) + "', '" + corpid + "', '" + allianceid +"', '" + apiid + "', '" + apikey + "');";
            Console.WriteLine(dbstring);
            Program.userDatabase.getResult(dbstring);
        }

        #endregion 
        public Resource getActiveResource()
        {
            if (isConnected == false)
            {
                return null;
            }
            return activeResourceList[0];

        }

        public void addActiveResource(Resource r)
        {
            activeResourceList.Add(r);
        }
        //grabs password in database 
        public bool getPasswordFromDatabase()
        {
            string querystring = "select password from blackcore.user where username = '" + username + "'";
            string result = Program.userDatabase.getResult(querystring);
     
            password = result;
            return false;
        }


        public List<User> getRoster()
        {
            return userCache;
        }

        public User getUserByJID(string username)
        {
            foreach (User u in User.userCache)
            {
                if (username.Equals(u.username))                {
            
                    return u;
                }
            }
            return null;
        }

        public string getJID()
        {
            return username + "@" + Program.hostName;
        }
        public string getFullJID()
        {
            return username + "@" + Program.hostName + "/" + getActiveResource().name;
        }
        public bool sendMessage(string message)
        {
            if (message != null && this.getActiveResource().workSocket.Connected)
            {
                Program.ha.sendMessage(this.getActiveResource().workSocket, message);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void recieveMessage(Dictionary<string, string> message)
        {
           
            string body = "";
            string destination = "";
            string state = "";
            User target;

            //TODO: proper parsing instead of a foreach
            foreach (KeyValuePair<string, string> kvp in message)
            {
                if (kvp.Key.Equals("body"))
                {
                    body = kvp.Value;
                }
                else if (kvp.Key.Equals("to"))
                {
                    destination = kvp.Value;
                }
                else if (kvp.Key.Equals("state"))
                {
                    state = kvp.Value;
                }
              //  Program.mainWindow.log(kvp.Key + ":" + kvp.Value, username, 2);
            }

            if (destination != null)
            {
                 string[] temp = destination.Split('@');
                target = getUserByJID(temp[0]);
                
                if (!target.isConnected)
                {
                    return;
                }

                if (target == null)
                {
                    Program.mainWindow.log("Message recipient not connected: " + destination, username, 2);
                    return;
                }
                if (body != null && !body.Equals(""))
                {
  
                    string response = XMPPStanza.getMessage(target.getFullJID(), this.getFullJID(), body);

                    target.sendMessage(response);
                    Program.mainWindow.log("Sent message to : " + target.getFullJID(), username, 2);
                }
                else
                {
                    string response = XMPPStanza.getMessageNoBody(target.getFullJID(), this.getFullJID(), state);


                    target.sendMessage(response);
                    Program.mainWindow.log("State: " + state, username, 2);
                }
            }
   
        }
    }
}
