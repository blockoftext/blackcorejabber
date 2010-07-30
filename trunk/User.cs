using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
namespace BlackCoreJabber
{
    // State object for reading client data asynchronously

    public class User
    {
        // Client  socket.

        public Socket workSocket = null;
        // Size of receive buffer.
        public static int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();

        //fully qualified JID
        public string username;
        //debug password
        public string password;
        //current streamid
        public string streamid;
        //current resource
        public string resource;

        int corpid;
        int allianceid;
        string userapiid;
        string userapikey;
        int dbid;

        public int auth_type = -1;
        public bool isAuthed = false;

        public static List<User> userCache;

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

        //grabs password in database 
        public bool getPasswordFromDatabase()
        {
            string querystring = "select password from blackcore.user where username = '" + username + "'";
            string result = Program.userDatabase.getResult(querystring);
            //Program.mainWindow.log(result, username, 3);
            password = result;
            return false;
        }

        public static List<string[]> getUserDetailList()
        {
            List<string[]> corpList = Program.userDatabase.getMoreResult("select * from BlackCore.user");
            return corpList;
        }

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

        public List<User> getRoster()
        {
            /*List<User> rosterList = new List<User>();
            User tempuser = new User();
            tempuser.username = "god";
            tempuser.resource = "mtolympus";
            rosterList.Add(tempuser);
            return rosterList;*/

            return userCache;
        }

        public User getUserByJID(string username)
        {
            foreach (User u in Program.userList)
            {
                if (username.Equals(u.username))
                {
                    return u;
                }
            }
            return null;
        }

        public bool sendMessage(string message)
        {
            if (message != null && workSocket.Connected)
            {
                Program.ha.sendMessage(workSocket, message);
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
                Program.mainWindow.log(kvp.Key + ":" + kvp.Value, username, 2);
            }

            if (destination != null)
            {
                 string[] temp = destination.Split('@');
                target = getUserByJID(temp[0]);
                if (target == null)
                {
                    Program.mainWindow.log("Message recipient not connected: " + destination, username, 2);
                    return;
                }
                if (body != null && !body.Equals(""))
                {
                    string response = "<message from='" + username + "@" + Program.hostName + "' to='" + destination + "@" + Program.hostName + "' type='chat'><body>" + body
                        + "</body><active/></message>";

                    target.sendMessage(response);
                    Program.mainWindow.log("Sent: " + body, username, 2);
                }
                else
                {
                   /* string response = "<message from='" + username + "@" + Program.hostName + "' to='" + destination + "@" + Program.hostName + "' type='chat'><" + state + "></message>";

                    target.sendMessage(response);
                    Program.mainWindow.log("State: " + state, username, 2);*/
                }
            }
   
        }
    }
}
