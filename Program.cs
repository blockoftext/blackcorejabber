using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
namespace BlackCoreJabber
{

    static class Program
    {
        public static int logLevel = 9;
        public static SocketHandler ha;
        public static MainWindow mainWindow;
        public static List<User> userList;
        public static string hostName = "192.168.1.100";
        public static database userDatabase;
        public static Thread userCheck;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ha = new SocketHandler();
            userDatabase = new database();
            userList = new List<User>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainWindow = new MainWindow();

            userCheck = new Thread(new ThreadStart(ha.checkUserConnected));
            userCheck.Start();

            Application.Run(mainWindow);
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

        public static void cleanup()
        {
            try
            {
                userCheck.Abort();
                userDatabase.disconnect();
                ha.disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
