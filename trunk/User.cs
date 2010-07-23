using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

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
        public string id;
        //current resource
        public string resource;

        public int auth_type = -1;
        public bool isAuthed = false;

        //grabs password in database 
        public bool getPasswordFromDatabase()
        {
            string querystring = "select password from blackcore.users where username = '" + username + "'";
            string result = Program.userDatabase.getResult(querystring);
            //Program.mainWindow.log(result, username, 3);
            password = result;
            return false;
        }

        public List<User> getRoster()
        {
            /*List<User> rosterList = new List<User>();
            User tempuser = new User();
            tempuser.username = "god";
            tempuser.resource = "mtolympus";
            rosterList.Add(tempuser);
            return rosterList;*/

            return Program.userList;
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
