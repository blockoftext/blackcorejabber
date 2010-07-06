using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace WindowsFormsApplication1
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

        public List<User> getRoster()
        {
            List<User> rosterList = new List<User>();
            User tempuser = new User();
            tempuser.username = "god";
            tempuser.resource = "mtolympus";
            rosterList.Add(tempuser);
            return rosterList;
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
            foreach (KeyValuePair<string, string> kvp in message)
            {
                Program.form1.log(kvp.Key + ":" + kvp.Value, username, 2);
            }

           /* string value = "";
             message.TryGetValue("to", out value);
             Program.form1.log("Message To: " + value, username, 2);

             message.TryGetValue("body", out value);
             Program.form1.log("Message To: " + value, username, 2);

             message.TryGetValue("to", out value);
             Program.form1.log("Message To: " + value, username, 2);*/
        }
    }
}
