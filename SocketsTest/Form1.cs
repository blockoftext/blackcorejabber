using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {            
            InitializeComponent();
        }

        public delegate void textDelegate(string text);

        public void addText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                textDelegate d = addText;
                this.Invoke(d, new object[] { text });
            }
            else
            {
               // textBox1.Text = text + "\r\n" + textBox1.Text;
                this.log(text, null, 0);
            }
            
        }

        public delegate void logDelegate(string text, string user, int level);

        public void log(string text,  string user, int level)
        {
            if (level > Program.logLevel)
            {
                return;
            }
            if (this.textBox1.InvokeRequired)
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
                textBox1.Text = "[ " + datetime + " ] <" + username + ">[" + level + "]: " +text + "\r\n" + textBox1.Text;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            addText("Opening Connection");
        
            try
            {
                if (Program.ha.connect())
                {
                    addText("Listening");
                }
                else
                {
                    addText("Connection Exists");
                }
               // Program.ha.handleMessages();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                addText("Connection Error");
            }        

          
               
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.ha.disconnect())
                {
                    addText("Disconnected");
                }
                else
                {
                    addText("Error Disconnecting");
                }
                
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                addText("Connection Error");
            }  
           
        }
    }
}
