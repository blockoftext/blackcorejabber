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
    public partial class UserAddForm : Form
    {
        public UserAddForm()
        {
            InitializeComponent();
            foreach(Alliance a in Alliance.allianceCache){
                alliancecombo.Items.Add(a.name);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void useraddok_Click(object sender, EventArgs e)
        {
            if (corpcombo.SelectedItem == null || alliancecombo.SelectedItem == null || usernametextbox.Text.Equals("") || passwordtextbox.Text.Equals(""))
            {
                return;
            }
           string corp = corpcombo.SelectedItem.ToString();      
           int cid = Corperation.getCorpIdByName(corp);

           string alliance = alliancecombo.SelectedItem.ToString();      
           int aid = Alliance.getAllianceIdByName(alliance);

           User.addUserToDatabase(usernametextbox.Text, passwordtextbox.Text, cid, aid, int.Parse( apiidtext.Text), apikeytextbox.Text);
        }

        private void useraddcancel_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = alliancecombo.SelectedItem.ToString();
           int id = Alliance.getAllianceIdByName(selected);
          if (id != -1)
          {
              foreach (Corperation c in Corperation.getCorpsByAllianceID(id))
              {
                  corpcombo.Items.Add(c.name);
              }
          }
           // 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UserAddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
