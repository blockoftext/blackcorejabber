namespace BlackCoreJabber
{
    partial class UserAddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernametextbox = new System.Windows.Forms.TextBox();
            this.passwordtextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.corpcombo = new System.Windows.Forms.ComboBox();
            this.alliancecombo = new System.Windows.Forms.ComboBox();
            this.label23423 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.apikeytextbox = new System.Windows.Forms.TextBox();
            this.apiidtext = new System.Windows.Forms.TextBox();
            this.useraddcancel = new System.Windows.Forms.Button();
            this.useraddok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernametextbox
            // 
            this.usernametextbox.Location = new System.Drawing.Point(160, 2);
            this.usernametextbox.Name = "usernametextbox";
            this.usernametextbox.Size = new System.Drawing.Size(100, 20);
            this.usernametextbox.TabIndex = 0;
            // 
            // passwordtextbox
            // 
            this.passwordtextbox.Location = new System.Drawing.Point(160, 28);
            this.passwordtextbox.Name = "passwordtextbox";
            this.passwordtextbox.Size = new System.Drawing.Size(100, 20);
            this.passwordtextbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // corpcombo
            // 
            this.corpcombo.FormattingEnabled = true;
            this.corpcombo.Location = new System.Drawing.Point(147, 81);
            this.corpcombo.Name = "corpcombo";
            this.corpcombo.Size = new System.Drawing.Size(121, 21);
            this.corpcombo.TabIndex = 4;
            this.corpcombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // alliancecombo
            // 
            this.alliancecombo.FormattingEnabled = true;
            this.alliancecombo.Location = new System.Drawing.Point(147, 54);
            this.alliancecombo.Name = "alliancecombo";
            this.alliancecombo.Size = new System.Drawing.Size(121, 21);
            this.alliancecombo.TabIndex = 5;
            this.alliancecombo.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label23423
            // 
            this.label23423.AutoSize = true;
            this.label23423.Location = new System.Drawing.Point(12, 62);
            this.label23423.Name = "label23423";
            this.label23423.Size = new System.Drawing.Size(44, 13);
            this.label23423.TabIndex = 6;
            this.label23423.Text = "Alliance";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Corp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "API ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "API Key";
            // 
            // apikeytextbox
            // 
            this.apikeytextbox.Location = new System.Drawing.Point(160, 131);
            this.apikeytextbox.Name = "apikeytextbox";
            this.apikeytextbox.Size = new System.Drawing.Size(100, 20);
            this.apikeytextbox.TabIndex = 10;
            // 
            // apiidtext
            // 
            this.apiidtext.Location = new System.Drawing.Point(160, 105);
            this.apiidtext.Name = "apiidtext";
            this.apiidtext.Size = new System.Drawing.Size(100, 20);
            this.apiidtext.TabIndex = 11;
            // 
            // useraddcancel
            // 
            this.useraddcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.useraddcancel.Location = new System.Drawing.Point(193, 154);
            this.useraddcancel.Name = "useraddcancel";
            this.useraddcancel.Size = new System.Drawing.Size(75, 23);
            this.useraddcancel.TabIndex = 12;
            this.useraddcancel.Text = "Cancel";
            this.useraddcancel.UseVisualStyleBackColor = true;
            this.useraddcancel.Click += new System.EventHandler(this.useraddcancel_Click);
            // 
            // useraddok
            // 
            this.useraddok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.useraddok.Location = new System.Drawing.Point(0, 154);
            this.useraddok.Name = "useraddok";
            this.useraddok.Size = new System.Drawing.Size(75, 23);
            this.useraddok.TabIndex = 13;
            this.useraddok.Text = "OK";
            this.useraddok.UseVisualStyleBackColor = true;
            this.useraddok.Click += new System.EventHandler(this.useraddok_Click);
            // 
            // UserAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(280, 182);
            this.Controls.Add(this.useraddok);
            this.Controls.Add(this.useraddcancel);
            this.Controls.Add(this.apiidtext);
            this.Controls.Add(this.apikeytextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label23423);
            this.Controls.Add(this.alliancecombo);
            this.Controls.Add(this.corpcombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordtextbox);
            this.Controls.Add(this.usernametextbox);
            this.Name = "UserAddForm";
            this.Text = "Add New User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernametextbox;
        private System.Windows.Forms.TextBox passwordtextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox corpcombo;
        private System.Windows.Forms.ComboBox alliancecombo;
        private System.Windows.Forms.Label label23423;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox apikeytextbox;
        private System.Windows.Forms.TextBox apiidtext;
        private System.Windows.Forms.Button useraddcancel;
        private System.Windows.Forms.Button useraddok;
    }
}