namespace BlackCoreJabber

{
    partial class MainWindow
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
            this.mainTab1 = new System.Windows.Forms.TabControl();
            this.statusTab = new System.Windows.Forms.TabPage();
            this.userTab = new System.Windows.Forms.TabPage();
            this.mainConsole = new System.Windows.Forms.TextBox();
            this.connectedUser = new System.Windows.Forms.TextBox();
            this.mainTab1.SuspendLayout();
            this.statusTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTab1
            // 
            this.mainTab1.Controls.Add(this.statusTab);
            this.mainTab1.Controls.Add(this.userTab);
            this.mainTab1.Location = new System.Drawing.Point(12, 12);
            this.mainTab1.Name = "mainTab1";
            this.mainTab1.SelectedIndex = 0;
            this.mainTab1.Size = new System.Drawing.Size(721, 412);
            this.mainTab1.TabIndex = 0;
            // 
            // statusTab
            // 
            this.statusTab.Controls.Add(this.connectedUser);
            this.statusTab.Controls.Add(this.mainConsole);
            this.statusTab.Location = new System.Drawing.Point(4, 22);
            this.statusTab.Name = "statusTab";
            this.statusTab.Padding = new System.Windows.Forms.Padding(3);
            this.statusTab.Size = new System.Drawing.Size(713, 386);
            this.statusTab.TabIndex = 0;
            this.statusTab.Text = "Status";
            this.statusTab.UseVisualStyleBackColor = true;
            this.statusTab.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // userTab
            // 
            this.userTab.Location = new System.Drawing.Point(4, 22);
            this.userTab.Name = "userTab";
            this.userTab.Padding = new System.Windows.Forms.Padding(3);
            this.userTab.Size = new System.Drawing.Size(713, 386);
            this.userTab.TabIndex = 1;
            this.userTab.Text = "Users";
            this.userTab.UseVisualStyleBackColor = true;
            // 
            // mainConsole
            // 
            this.mainConsole.Location = new System.Drawing.Point(3, 6);
            this.mainConsole.Multiline = true;
            this.mainConsole.Name = "mainConsole";
            this.mainConsole.Size = new System.Drawing.Size(513, 374);
            this.mainConsole.TabIndex = 0;
            // 
            // connectedUser
            // 
            this.connectedUser.Location = new System.Drawing.Point(522, 6);
            this.connectedUser.Multiline = true;
            this.connectedUser.Name = "connectedUser";
            this.connectedUser.Size = new System.Drawing.Size(185, 374);
            this.connectedUser.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 436);
            this.Controls.Add(this.mainTab1);
            this.Name = "MainWindow";
            this.Text = "mainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainTab1.ResumeLayout(false);
            this.statusTab.ResumeLayout(false);
            this.statusTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTab1;
        private System.Windows.Forms.TabPage statusTab;
        private System.Windows.Forms.TabPage userTab;
        private System.Windows.Forms.TextBox connectedUser;
        private System.Windows.Forms.TextBox mainConsole;
    }
}

