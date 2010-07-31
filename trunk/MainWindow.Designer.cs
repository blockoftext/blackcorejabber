using System.Windows.Forms;
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
            this.connectedUser = new System.Windows.Forms.TextBox();
            this.mainConsole = new System.Windows.Forms.TextBox();
            this.userTab = new System.Windows.Forms.TabPage();
            this.caadmintab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.addalliancebutton = new System.Windows.Forms.Button();
            this.removealliancebutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.alliancetable = new System.Windows.Forms.DataGridView();
            this.allianceid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alliancename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alliancetickercolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removecorpbutton = new System.Windows.Forms.Button();
            this.addcorpbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.corptable = new System.Windows.Forms.DataGridView();
            this.userdatagrid = new System.Windows.Forms.DataGridView();
            this.corpidcolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corpnamecolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corpticker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corpalliancecolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useridcolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usernamecolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usercorpnamecolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useralliancenamecolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userapiidcolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userapikeycolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useraddbutton = new System.Windows.Forms.Button();
            this.userremovebutton = new System.Windows.Forms.Button();
            this.userstatuscheckbox = new System.Windows.Forms.CheckBox();
            this.fromalliancecombo = new System.Windows.Forms.ComboBox();
            this.fromcorpcombo = new System.Windows.Forms.ComboBox();
            this.mainTab1.SuspendLayout();
            this.statusTab.SuspendLayout();
            this.userTab.SuspendLayout();
            this.caadmintab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alliancetable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corptable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userdatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTab1
            // 
            this.mainTab1.Controls.Add(this.statusTab);
            this.mainTab1.Controls.Add(this.userTab);
            this.mainTab1.Controls.Add(this.caadmintab);
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
            // connectedUser
            // 
            this.connectedUser.Location = new System.Drawing.Point(522, 6);
            this.connectedUser.Multiline = true;
            this.connectedUser.Name = "connectedUser";
            this.connectedUser.Size = new System.Drawing.Size(185, 374);
            this.connectedUser.TabIndex = 1;
            // 
            // mainConsole
            // 
            this.mainConsole.Location = new System.Drawing.Point(3, 6);
            this.mainConsole.Multiline = true;
            this.mainConsole.Name = "mainConsole";
            this.mainConsole.Size = new System.Drawing.Size(513, 374);
            this.mainConsole.TabIndex = 0;
            this.mainConsole.TextChanged += new System.EventHandler(this.mainConsole_TextChanged);
            // 
            // userTab
            // 
            this.userTab.Controls.Add(this.fromcorpcombo);
            this.userTab.Controls.Add(this.fromalliancecombo);
            this.userTab.Controls.Add(this.userstatuscheckbox);
            this.userTab.Controls.Add(this.userremovebutton);
            this.userTab.Controls.Add(this.useraddbutton);
            this.userTab.Controls.Add(this.userdatagrid);
            this.userTab.Location = new System.Drawing.Point(4, 22);
            this.userTab.Name = "userTab";
            this.userTab.Padding = new System.Windows.Forms.Padding(3);
            this.userTab.Size = new System.Drawing.Size(713, 386);
            this.userTab.TabIndex = 1;
            this.userTab.Text = "Users";
            this.userTab.UseVisualStyleBackColor = true;
            // 
            // caadmintab
            // 
            this.caadmintab.Controls.Add(this.splitContainer1);
            this.caadmintab.Location = new System.Drawing.Point(4, 22);
            this.caadmintab.Name = "caadmintab";
            this.caadmintab.Size = new System.Drawing.Size(713, 386);
            this.caadmintab.TabIndex = 2;
            this.caadmintab.Text = "Corp and Alliance";
            this.caadmintab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addalliancebutton);
            this.splitContainer1.Panel1.Controls.Add(this.removealliancebutton);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.alliancetable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.removecorpbutton);
            this.splitContainer1.Panel2.Controls.Add(this.addcorpbutton);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.corptable);
            this.splitContainer1.Size = new System.Drawing.Size(713, 386);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 0;
            // 
            // addalliancebutton
            // 
            this.addalliancebutton.Location = new System.Drawing.Point(7, 355);
            this.addalliancebutton.Name = "addalliancebutton";
            this.addalliancebutton.Size = new System.Drawing.Size(27, 23);
            this.addalliancebutton.TabIndex = 2;
            this.addalliancebutton.Text = "+";
            this.addalliancebutton.UseVisualStyleBackColor = true;
            this.addalliancebutton.Click += new System.EventHandler(this.addalliancebutton_Click);
            // 
            // removealliancebutton
            // 
            this.removealliancebutton.Location = new System.Drawing.Point(40, 355);
            this.removealliancebutton.Name = "removealliancebutton";
            this.removealliancebutton.Size = new System.Drawing.Size(28, 23);
            this.removealliancebutton.TabIndex = 1;
            this.removealliancebutton.Text = "-";
            this.removealliancebutton.UseVisualStyleBackColor = true;
            this.removealliancebutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Allowed Alliances";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // alliancetable
            // 
            this.alliancetable.AllowUserToAddRows = false;
            this.alliancetable.AllowUserToDeleteRows = false;
            this.alliancetable.AllowUserToOrderColumns = true;
            this.alliancetable.AllowUserToResizeColumns = false;
            this.alliancetable.AllowUserToResizeRows = false;
            this.alliancetable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.alliancetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.alliancetable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.allianceid,
            this.alliancename,
            this.alliancetickercolumn});
            this.alliancetable.Location = new System.Drawing.Point(1, 27);
            this.alliancetable.Name = "alliancetable";
            this.alliancetable.ReadOnly = true;
            this.alliancetable.RowHeadersVisible = false;
            this.alliancetable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.alliancetable.Size = new System.Drawing.Size(243, 322);
            this.alliancetable.TabIndex = 0;
            this.alliancetable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // allianceid
            // 
            this.allianceid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.allianceid.FillWeight = 25F;
            this.allianceid.HeaderText = "ID";
            this.allianceid.Name = "allianceid";
            this.allianceid.ReadOnly = true;
            this.allianceid.Width = 43;
            // 
            // alliancename
            // 
            this.alliancename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.alliancename.HeaderText = "Alliance Name";
            this.alliancename.Name = "alliancename";
            this.alliancename.ReadOnly = true;
            // 
            // alliancetickercolumn
            // 
            this.alliancetickercolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.alliancetickercolumn.FillWeight = 50F;
            this.alliancetickercolumn.HeaderText = "Ticker";
            this.alliancetickercolumn.Name = "alliancetickercolumn";
            this.alliancetickercolumn.ReadOnly = true;
            this.alliancetickercolumn.Width = 62;
            // 
            // removecorpbutton
            // 
            this.removecorpbutton.Location = new System.Drawing.Point(34, 355);
            this.removecorpbutton.Name = "removecorpbutton";
            this.removecorpbutton.Size = new System.Drawing.Size(26, 23);
            this.removecorpbutton.TabIndex = 3;
            this.removecorpbutton.Text = "-";
            this.removecorpbutton.UseVisualStyleBackColor = true;
            this.removecorpbutton.Click += new System.EventHandler(this.removecorpbutton_Click);
            // 
            // addcorpbutton
            // 
            this.addcorpbutton.Location = new System.Drawing.Point(3, 355);
            this.addcorpbutton.Name = "addcorpbutton";
            this.addcorpbutton.Size = new System.Drawing.Size(25, 23);
            this.addcorpbutton.TabIndex = 2;
            this.addcorpbutton.Text = "+";
            this.addcorpbutton.UseVisualStyleBackColor = true;
            this.addcorpbutton.Click += new System.EventHandler(this.addcorpbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Allowed Corperations";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // corptable
            // 
            this.corptable.AllowUserToAddRows = false;
            this.corptable.AllowUserToDeleteRows = false;
            this.corptable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.corptable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.corpidcolumn,
            this.corpnamecolumn,
            this.corpticker,
            this.corpalliancecolumn});
            this.corptable.Location = new System.Drawing.Point(0, 27);
            this.corptable.Name = "corptable";
            this.corptable.ReadOnly = true;
            this.corptable.RowHeadersVisible = false;
            this.corptable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.corptable.Size = new System.Drawing.Size(460, 322);
            this.corptable.TabIndex = 0;
            // 
            // userdatagrid
            // 
            this.userdatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userdatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.useridcolumn,
            this.usernamecolumn,
            this.usercorpnamecolumn,
            this.useralliancenamecolumn,
            this.userapiidcolumn,
            this.userapikeycolumn});
            this.userdatagrid.Location = new System.Drawing.Point(3, 32);
            this.userdatagrid.Name = "userdatagrid";
            this.userdatagrid.RowHeadersVisible = false;
            this.userdatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userdatagrid.Size = new System.Drawing.Size(714, 193);
            this.userdatagrid.TabIndex = 0;
            // 
            // corpidcolumn
            // 
            this.corpidcolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.corpidcolumn.FillWeight = 25F;
            this.corpidcolumn.HeaderText = "ID";
            this.corpidcolumn.Name = "corpidcolumn";
            this.corpidcolumn.ReadOnly = true;
            this.corpidcolumn.Width = 43;
            // 
            // corpnamecolumn
            // 
            this.corpnamecolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.corpnamecolumn.HeaderText = "Corp Name";
            this.corpnamecolumn.Name = "corpnamecolumn";
            this.corpnamecolumn.ReadOnly = true;
            // 
            // corpticker
            // 
            this.corpticker.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.corpticker.HeaderText = "Ticker";
            this.corpticker.Name = "corpticker";
            this.corpticker.ReadOnly = true;
            this.corpticker.Width = 62;
            // 
            // corpalliancecolumn
            // 
            this.corpalliancecolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.corpalliancecolumn.HeaderText = "Alliance";
            this.corpalliancecolumn.Name = "corpalliancecolumn";
            this.corpalliancecolumn.ReadOnly = true;
            // 
            // useridcolumn
            // 
            this.useridcolumn.HeaderText = "ID";
            this.useridcolumn.Name = "useridcolumn";
            // 
            // usernamecolumn
            // 
            this.usernamecolumn.HeaderText = "Username";
            this.usernamecolumn.Name = "usernamecolumn";
            // 
            // usercorpnamecolumn
            // 
            this.usercorpnamecolumn.HeaderText = "Corp";
            this.usercorpnamecolumn.Name = "usercorpnamecolumn";
            // 
            // useralliancenamecolumn
            // 
            this.useralliancenamecolumn.HeaderText = "Alliance";
            this.useralliancenamecolumn.Name = "useralliancenamecolumn";
            // 
            // userapiidcolumn
            // 
            this.userapiidcolumn.HeaderText = "API ID";
            this.userapiidcolumn.Name = "userapiidcolumn";
            // 
            // userapikeycolumn
            // 
            this.userapikeycolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.userapikeycolumn.HeaderText = "API Key";
            this.userapikeycolumn.Name = "userapikeycolumn";
            // 
            // useraddbutton
            // 
            this.useraddbutton.Location = new System.Drawing.Point(6, 231);
            this.useraddbutton.Name = "useraddbutton";
            this.useraddbutton.Size = new System.Drawing.Size(23, 23);
            this.useraddbutton.TabIndex = 1;
            this.useraddbutton.Text = "+";
            this.useraddbutton.UseVisualStyleBackColor = true;
            this.useraddbutton.Click += new System.EventHandler(this.useraddbutton_Click);
            // 
            // userremovebutton
            // 
            this.userremovebutton.Location = new System.Drawing.Point(35, 231);
            this.userremovebutton.Name = "userremovebutton";
            this.userremovebutton.Size = new System.Drawing.Size(26, 23);
            this.userremovebutton.TabIndex = 2;
            this.userremovebutton.Text = "-";
            this.userremovebutton.UseVisualStyleBackColor = true;
            this.userremovebutton.Click += new System.EventHandler(this.userremovebutton_Click);
            // 
            // userstatuscheckbox
            // 
            this.userstatuscheckbox.AutoSize = true;
            this.userstatuscheckbox.Location = new System.Drawing.Point(67, 237);
            this.userstatuscheckbox.Name = "userstatuscheckbox";
            this.userstatuscheckbox.Size = new System.Drawing.Size(106, 17);
            this.userstatuscheckbox.TabIndex = 3;
            this.userstatuscheckbox.Text = "Online users only";
            this.userstatuscheckbox.UseVisualStyleBackColor = true;
            // 
            // fromalliancecombo
            // 
            this.fromalliancecombo.FormattingEnabled = true;
            this.fromalliancecombo.Location = new System.Drawing.Point(179, 231);
            this.fromalliancecombo.Name = "fromalliancecombo";
            this.fromalliancecombo.Size = new System.Drawing.Size(121, 21);
            this.fromalliancecombo.TabIndex = 4;
            // 
            // fromcorpcombo
            // 
            this.fromcorpcombo.FormattingEnabled = true;
            this.fromcorpcombo.Location = new System.Drawing.Point(306, 231);
            this.fromcorpcombo.Name = "fromcorpcombo";
            this.fromcorpcombo.Size = new System.Drawing.Size(121, 21);
            this.fromcorpcombo.TabIndex = 5;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 436);
            this.Controls.Add(this.mainTab1);
            this.Name = "MainWindow";
            this.Text = "Black Core Jabber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainTab1.ResumeLayout(false);
            this.statusTab.ResumeLayout(false);
            this.statusTab.PerformLayout();
            this.userTab.ResumeLayout(false);
            this.userTab.PerformLayout();
            this.caadmintab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.alliancetable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corptable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userdatagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTab1;
        private System.Windows.Forms.TabPage statusTab;
        private System.Windows.Forms.TabPage userTab;
        private System.Windows.Forms.TextBox connectedUser;
        private System.Windows.Forms.TextBox mainConsole;
        private TabPage caadmintab;
        private SplitContainer splitContainer1;
        public DataGridView alliancetable;
        private DataGridViewTextBoxColumn allianceid;
        private DataGridViewTextBoxColumn alliancename;
        private DataGridViewTextBoxColumn alliancetickercolumn;
        private DataGridView corptable;
        private Label label1;
        private Label label2;
        private Button removealliancebutton;
        private Button addalliancebutton;
        private Button removecorpbutton;
        private Button addcorpbutton;
        public DataGridView userdatagrid;
        private DataGridViewTextBoxColumn useridcolumn;
        private DataGridViewTextBoxColumn usernamecolumn;
        private DataGridViewTextBoxColumn usercorpnamecolumn;
        private DataGridViewTextBoxColumn useralliancenamecolumn;
        private DataGridViewTextBoxColumn userapiidcolumn;
        private DataGridViewTextBoxColumn userapikeycolumn;
        private DataGridViewTextBoxColumn corpidcolumn;
        private DataGridViewTextBoxColumn corpnamecolumn;
        private DataGridViewTextBoxColumn corpticker;
        private DataGridViewTextBoxColumn corpalliancecolumn;
        private ComboBox fromcorpcombo;
        private ComboBox fromalliancecombo;
        private CheckBox userstatuscheckbox;
        private Button userremovebutton;
        private Button useraddbutton;
    }
}

