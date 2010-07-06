using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
namespace WindowsFormsApplication1
{

    static class Program
    {
        public static int logLevel = 9;
        public static SocketHandler ha;
        public static Form1 form1;
        public static List<User> userList;
        public static string hostName = "192.168.1.100";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ha = new SocketHandler();
            userList = new List<User>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();

            Thread userCheck = new Thread(new ThreadStart(ha.checkUserConnected));
            userCheck.Start();

            Application.Run(form1);
           
        }

        public static List<string> getConnectedUserNames()
        {
            List<string> newList = new List<string>();
            foreach (User u in userList)
            {
                newList.Add(u.username + "/" + u.resource + " : " + u.workSocket.RemoteEndPoint.ToString());
            }
            return newList;
        }

       
    }
}
