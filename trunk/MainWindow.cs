﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCoreJabber
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                if (Program.ha.connect())
                {
                    log("Socket Open", null, 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error opening socket, " + e);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        public delegate void textDelegate(string text);

        public void addText(string text)
        {
            if (this.mainConsole.InvokeRequired)
            {
                textDelegate d = addText;
                this.Invoke(d, new object[] { text });
            }
            else
            {
                // mainConsole.Text = text + "\r\n" + mainConsole.Text;
                this.log(text, null, 0);
            }

        }

        public delegate void logDelegate(string text, string user, int level);

        public void log(string text, string user, int level)
        {
            if (level > Program.logLevel)
            {
                return;
            }
            if (this.mainConsole.InvokeRequired)
            {
                logDelegate d = log;
                this.Invoke(d, new object[] { text, user, level });
            }
            else
            {
                String datetime = System.DateTime.Now.ToString();
                String username;
                if (user == null)
                {
                    username = "System";
                }
                else
                {
                    username = user;
                }
                mainConsole.Text = "[ " + datetime + " ] <" + username + ">[" + level + "]: " + text + "\r\n" + mainConsole.Text;
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

         public delegate void updateDelegate(List<string> userNameList);

        public void updateUserList(List<string> userNameList)
        {

            if (this.connectedUser.InvokeRequired)
            {
                updateDelegate d = updateUserList;
                this.Invoke(d, new object[] { userNameList });
            }
            else
            {
                connectedUser.Text = "";
                foreach (string s in userNameList)
                {
                    connectedUser.Text += s + "\r\n";
                }
            }

        }
    }
    
}
