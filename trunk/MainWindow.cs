using System;
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

            //connect to database
            try
            {
                if (Program.ha.connect())
                {
                    log("Socket Open", null, 0);
                }
                if (Program.userDatabase.connect())
                {
                    log("Database Connected", null, 0);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error opening socket, " + e);
            }

            //cache alliances
            if (Alliance.loadAlliances())
            {
                log("Alliances Cached", null, 0);
                Alliance.updateTable(alliancetable);
            }
            else
            {
                log("No Alliances Cached", null, 0);
            }

            //cache corps
            if (Corperation.loadCorps())
            {
                log("Corps Cached", null, 0);
            }
            else
            {
                log("No Corps Cached", null, 0);
            }

            if (User.loadUsers())
            {
                log("Users Cached", null, 0);
                User.updateTable(userdatagrid);
            }
            else
            {
                log("No Users Cached", null, 0);
            }
           
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        //called when form is closed
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.cleanup();
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

        private void mainConsole_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string allianceID = alliancetable.SelectedRows[0].Cells[0].Value.ToString();
           // Console.WriteLine(allianceID);
            Corperation.updateTableByID(corptable, int.Parse(allianceID));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void removecorpbutton_Click(object sender, EventArgs e)
        {

        }

        private void addcorpbutton_Click(object sender, EventArgs e)
        {

        }

        private void addalliancebutton_Click(object sender, EventArgs e)
        {

        }

        private void userremovebutton_Click(object sender, EventArgs e)
        {

        }

        private void useraddbutton_Click(object sender, EventArgs e)
        {
            UserAddForm newform = new UserAddForm();
            newform.ShowDialog();
            
        }

    }
    
}
