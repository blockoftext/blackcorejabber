using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
namespace BlackCoreJabber
{
    public class Resource
    {
        public string streamid;
        public string name;
        public Socket workSocket = null;
        // Size of receive buffer.
        public static int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();

        public User parentUser;

        public bool isAuthed = false;

        public int auth_type = -1;

        public bool sendMessage(string message)
        {
            if (message != null && this.workSocket.Connected)
            {
                Program.ha.sendMessage(workSocket, message);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getFullJID()
        {
            return parentUser.username + "@" + Program.hostName + "/" + name;
        }
    }
}
