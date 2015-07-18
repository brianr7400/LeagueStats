namespace LeagueStats
{
    partial class LeagueStats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeagueStats));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchButton = new System.Windows.Forms.Button();
            this.regionBox = new System.Windows.Forms.ComboBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AppName = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.winlossLabel = new System.Windows.Forms.Label();
            this.rankLabel = new System.Windows.Forms.Label();
            this.lpLabel = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.overviewTab = new System.Windows.Forms.TabPage();
            this.matchhistoryTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.towerLabel1 = new System.Windows.Forms.Label();
            this.visionwardLabel1 = new System.Windows.Forms.Label();
            this.wardLabel1 = new System.Windows.Forms.Label();
            this.csLabel1 = new System.Windows.Forms.Label();
            this.goldLabel1 = new System.Windows.Forms.Label();
            this.levelLabel1 = new System.Windows.Forms.Label();
            this.kdaLabel1 = new System.Windows.Forms.Label();
            this.jobLabel1 = new System.Windows.Forms.Label();
            this.winLabel1 = new System.Windows.Forms.Label();
            this.champPic1 = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.overviewTab.SuspendLayout();
            this.matchhistoryTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.champPic1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(944, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meToolStripMenuItem,
            this.updatesToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // meToolStripMenuItem
            // 
            this.meToolStripMenuItem.Name = "meToolStripMenuItem";
            this.meToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.meToolStripMenuItem.Text = "Me";
            this.meToolStripMenuItem.Click += new System.EventHandler(this.meToolStripMenuItem_Click);
            // 
            // updatesToolStripMenuItem
            // 
            this.updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
            this.updatesToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.updatesToolStripMenuItem.Text = "Updates";
            this.updatesToolStripMenuItem.Click += new System.EventHandler(this.updatesToolStripMenuItem_Click);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.searchButton.FlatAppearance.BorderSize = 3;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(857, 71);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 37);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // regionBox
            // 
            this.regionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regionBox.ForeColor = System.Drawing.Color.White;
            this.regionBox.FormattingEnabled = true;
            this.regionBox.Items.AddRange(new object[] {
            "NA"});
            this.regionBox.Location = new System.Drawing.Point(85, 81);
            this.regionBox.Name = "regionBox";
            this.regionBox.Size = new System.Drawing.Size(61, 21);
            this.regionBox.TabIndex = 4;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Lucida Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(159, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(430, 55);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "SummonerName";
            this.nameLabel.UseMnemonic = false;
            this.nameLabel.Visible = false;
            // 
            // iconBox
            // 
            this.iconBox.ImageLocation = "";
            this.iconBox.Location = new System.Drawing.Point(3, 3);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(150, 150);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconBox.TabIndex = 6;
            this.iconBox.TabStop = false;
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.ForeColor = System.Drawing.Color.White;
            this.searchBox.Location = new System.Drawing.Point(152, 81);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(699, 26);
            this.searchBox.TabIndex = 7;
            this.searchBox.Text = "Summoner Name";
            this.searchBox.Click += new System.EventHandler(this.searchBox_Click);
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // AppName
            // 
            this.AppName.Font = new System.Drawing.Font("Lucida Sans", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppName.ForeColor = System.Drawing.Color.White;
            this.AppName.Location = new System.Drawing.Point(369, 39);
            this.AppName.Name = "AppName";
            this.AppName.Size = new System.Drawing.Size(222, 39);
            this.AppName.TabIndex = 9;
            this.AppName.Text = "LeagueStats";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel.ForeColor = System.Drawing.Color.White;
            this.levelLabel.Location = new System.Drawing.Point(162, 64);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(147, 39);
            this.levelLabel.TabIndex = 10;
            this.levelLabel.Text = "Level 30";
            this.levelLabel.Visible = false;
            // 
            // winlossLabel
            // 
            this.winlossLabel.AutoSize = true;
            this.winlossLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winlossLabel.ForeColor = System.Drawing.Color.White;
            this.winlossLabel.Location = new System.Drawing.Point(165, 103);
            this.winlossLabel.Name = "winlossLabel";
            this.winlossLabel.Size = new System.Drawing.Size(104, 31);
            this.winlossLabel.TabIndex = 11;
            this.winlossLabel.Text = "Wins: 5";
            this.winlossLabel.Visible = false;
            // 
            // rankLabel
            // 
            this.rankLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rankLabel.AutoSize = true;
            this.rankLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rankLabel.ForeColor = System.Drawing.Color.White;
            this.rankLabel.Location = new System.Drawing.Point(633, 13);
            this.rankLabel.Name = "rankLabel";
            this.rankLabel.Size = new System.Drawing.Size(271, 51);
            this.rankLabel.TabIndex = 12;
            this.rankLabel.Text = "Challenger V";
            this.rankLabel.Visible = false;
            // 
            // lpLabel
            // 
            this.lpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lpLabel.AutoSize = true;
            this.lpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpLabel.ForeColor = System.Drawing.Color.White;
            this.lpLabel.Location = new System.Drawing.Point(776, 64);
            this.lpLabel.Name = "lpLabel";
            this.lpLabel.Size = new System.Drawing.Size(106, 39);
            this.lpLabel.TabIndex = 13;
            this.lpLabel.Text = "50 LP";
            this.lpLabel.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.overviewTab);
            this.tabControl.Controls.Add(this.matchhistoryTab);
            this.tabControl.Location = new System.Drawing.Point(0, 108);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(944, 392);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 14;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // overviewTab
            // 
            this.overviewTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(98)))), ((int)(((byte)(162)))));
            this.overviewTab.Controls.Add(this.iconBox);
            this.overviewTab.Controls.Add(this.lpLabel);
            this.overviewTab.Controls.Add(this.nameLabel);
            this.overviewTab.Controls.Add(this.rankLabel);
            this.overviewTab.Controls.Add(this.levelLabel);
            this.overviewTab.Controls.Add(this.winlossLabel);
            this.overviewTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(98)))), ((int)(((byte)(162)))));
            this.overviewTab.Location = new System.Drawing.Point(4, 22);
            this.overviewTab.Name = "overviewTab";
            this.overviewTab.Padding = new System.Windows.Forms.Padding(3);
            this.overviewTab.Size = new System.Drawing.Size(936, 366);
            this.overviewTab.TabIndex = 0;
            this.overviewTab.Text = "Overview";
            // 
            // matchhistoryTab
            // 
            this.matchhistoryTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(98)))), ((int)(((byte)(162)))));
            this.matchhistoryTab.Controls.Add(this.groupBox1);
            this.matchhistoryTab.Location = new System.Drawing.Point(4, 22);
            this.matchhistoryTab.Name = "matchhistoryTab";
            this.matchhistoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.matchhistoryTab.Size = new System.Drawing.Size(936, 366);
            this.matchhistoryTab.TabIndex = 1;
            this.matchhistoryTab.Text = "Match History";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.towerLabel1);
            this.groupBox1.Controls.Add(this.visionwardLabel1);
            this.groupBox1.Controls.Add(this.wardLabel1);
            this.groupBox1.Controls.Add(this.csLabel1);
            this.groupBox1.Controls.Add(this.goldLabel1);
            this.groupBox1.Controls.Add(this.levelLabel1);
            this.groupBox1.Controls.Add(this.kdaLabel1);
            this.groupBox1.Controls.Add(this.jobLabel1);
            this.groupBox1.Controls.Add(this.winLabel1);
            this.groupBox1.Controls.Add(this.champPic1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // towerLabel1
            // 
            this.towerLabel1.AutoSize = true;
            this.towerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.towerLabel1.ForeColor = System.Drawing.Color.White;
            this.towerLabel1.Location = new System.Drawing.Point(589, 16);
            this.towerLabel1.Name = "towerLabel1";
            this.towerLabel1.Size = new System.Drawing.Size(236, 29);
            this.towerLabel1.TabIndex = 9;
            this.towerLabel1.Text = "Towers Destroyed: 2";
            // 
            // visionwardLabel1
            // 
            this.visionwardLabel1.AutoSize = true;
            this.visionwardLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visionwardLabel1.ForeColor = System.Drawing.Color.White;
            this.visionwardLabel1.Location = new System.Drawing.Point(589, 71);
            this.visionwardLabel1.Name = "visionwardLabel1";
            this.visionwardLabel1.Size = new System.Drawing.Size(241, 29);
            this.visionwardLabel1.TabIndex = 8;
            this.visionwardLabel1.Text = "Pink Wards Placed: 1";
            // 
            // wardLabel1
            // 
            this.wardLabel1.AutoSize = true;
            this.wardLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wardLabel1.ForeColor = System.Drawing.Color.White;
            this.wardLabel1.Location = new System.Drawing.Point(589, 45);
            this.wardLabel1.Name = "wardLabel1";
            this.wardLabel1.Size = new System.Drawing.Size(188, 29);
            this.wardLabel1.TabIndex = 7;
            this.wardLabel1.Text = "Wards Placed: 9";
            // 
            // csLabel1
            // 
            this.csLabel1.AutoSize = true;
            this.csLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csLabel1.ForeColor = System.Drawing.Color.White;
            this.csLabel1.Location = new System.Drawing.Point(344, 74);
            this.csLabel1.Name = "csLabel1";
            this.csLabel1.Size = new System.Drawing.Size(216, 29);
            this.csLabel1.TabIndex = 6;
            this.csLabel1.Text = "Minions Killed: 233";
            // 
            // goldLabel1
            // 
            this.goldLabel1.AutoSize = true;
            this.goldLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goldLabel1.ForeColor = System.Drawing.Color.White;
            this.goldLabel1.Location = new System.Drawing.Point(344, 45);
            this.goldLabel1.Name = "goldLabel1";
            this.goldLabel1.Size = new System.Drawing.Size(226, 29);
            this.goldLabel1.TabIndex = 5;
            this.goldLabel1.Text = "Gold Earned: 13955";
            // 
            // levelLabel1
            // 
            this.levelLabel1.AutoSize = true;
            this.levelLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel1.ForeColor = System.Drawing.Color.White;
            this.levelLabel1.Location = new System.Drawing.Point(344, 16);
            this.levelLabel1.Name = "levelLabel1";
            this.levelLabel1.Size = new System.Drawing.Size(109, 29);
            this.levelLabel1.TabIndex = 4;
            this.levelLabel1.Text = "Level: 17";
            // 
            // kdaLabel1
            // 
            this.kdaLabel1.AutoSize = true;
            this.kdaLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdaLabel1.ForeColor = System.Drawing.Color.White;
            this.kdaLabel1.Location = new System.Drawing.Point(204, 71);
            this.kdaLabel1.Name = "kdaLabel1";
            this.kdaLabel1.Size = new System.Drawing.Size(79, 29);
            this.kdaLabel1.TabIndex = 3;
            this.kdaLabel1.Text = "12/6/8";
            // 
            // jobLabel1
            // 
            this.jobLabel1.AutoSize = true;
            this.jobLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobLabel1.ForeColor = System.Drawing.Color.White;
            this.jobLabel1.Location = new System.Drawing.Point(113, 71);
            this.jobLabel1.Name = "jobLabel1";
            this.jobLabel1.Size = new System.Drawing.Size(85, 29);
            this.jobLabel1.TabIndex = 2;
            this.jobLabel1.Text = "Jungle";
            // 
            // winLabel1
            // 
            this.winLabel1.AutoSize = true;
            this.winLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winLabel1.ForeColor = System.Drawing.Color.White;
            this.winLabel1.Location = new System.Drawing.Point(108, 16);
            this.winLabel1.Name = "winLabel1";
            this.winLabel1.Size = new System.Drawing.Size(182, 55);
            this.winLabel1.TabIndex = 1;
            this.winLabel1.Text = "Winner";
            // 
            // champPic1
            // 
            this.champPic1.Location = new System.Drawing.Point(7, 11);
            this.champPic1.Name = "champPic1";
            this.champPic1.Size = new System.Drawing.Size(95, 95);
            this.champPic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.champPic1.TabIndex = 0;
            this.champPic1.TabStop = false;
            // 
            // LeagueStats
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.AppName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.regionBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "LeagueStats";
            this.Text = "LeagueStats";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.overviewTab.ResumeLayout(false);
            this.overviewTab.PerformLayout();
            this.matchhistoryTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.champPic1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox regionBox;
        public System.Windows.Forms.PictureBox iconBox;
        public System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label AppName;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.ToolStripMenuItem meToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatesToolStripMenuItem;
        private System.Windows.Forms.Label winlossLabel;
        private System.Windows.Forms.Label rankLabel;
        private System.Windows.Forms.Label lpLabel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage overviewTab;
        private System.Windows.Forms.TabPage matchhistoryTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox champPic1;
        private System.Windows.Forms.Label winLabel1;
        private System.Windows.Forms.Label jobLabel1;
        private System.Windows.Forms.Label towerLabel1;
        private System.Windows.Forms.Label wardLabel1;
        private System.Windows.Forms.Label visionwardLabel1;
        private System.Windows.Forms.Label csLabel1;
        private System.Windows.Forms.Label goldLabel1;
        private System.Windows.Forms.Label levelLabel1;
        private System.Windows.Forms.Label kdaLabel1;
    }
}

