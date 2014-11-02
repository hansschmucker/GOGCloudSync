namespace GOGCloud
{
    partial class FormGameList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameList));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gameList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCloudFolder = new System.Windows.Forms.Button();
            this.buttonSaveFolder = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonCloud = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.browseCloudFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.gameList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(914, 478);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gameList
            // 
            this.gameList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameList.FormattingEnabled = true;
            this.gameList.Location = new System.Drawing.Point(3, 35);
            this.gameList.Name = "gameList";
            this.gameList.Size = new System.Drawing.Size(222, 440);
            this.gameList.TabIndex = 0;
            this.gameList.SelectedIndexChanged += new System.EventHandler(this.onGameSelection);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCloudFolder);
            this.panel1.Controls.Add(this.buttonSaveFolder);
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Controls.Add(this.buttonCloud);
            this.panel1.Controls.Add(this.buttonPlay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(231, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 440);
            this.panel1.TabIndex = 1;
            // 
            // buttonCloudFolder
            // 
            this.buttonCloudFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCloudFolder.Location = new System.Drawing.Point(453, 414);
            this.buttonCloudFolder.Name = "buttonCloudFolder";
            this.buttonCloudFolder.Size = new System.Drawing.Size(144, 23);
            this.buttonCloudFolder.TabIndex = 5;
            this.buttonCloudFolder.Text = "Cloud Folder";
            this.buttonCloudFolder.UseVisualStyleBackColor = true;
            this.buttonCloudFolder.Click += new System.EventHandler(this.buttonCloudFolder_Click);
            // 
            // buttonSaveFolder
            // 
            this.buttonSaveFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveFolder.Enabled = false;
            this.buttonSaveFolder.Location = new System.Drawing.Point(303, 414);
            this.buttonSaveFolder.Name = "buttonSaveFolder";
            this.buttonSaveFolder.Size = new System.Drawing.Size(144, 23);
            this.buttonSaveFolder.TabIndex = 4;
            this.buttonSaveFolder.Text = "Open SaveFolder";
            this.buttonSaveFolder.UseVisualStyleBackColor = true;
            this.buttonSaveFolder.Click += new System.EventHandler(this.onRevealDir);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(668, 408);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.Url = new System.Uri("http://www.gog.com", System.UriKind.Absolute);
            // 
            // buttonCloud
            // 
            this.buttonCloud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCloud.Enabled = false;
            this.buttonCloud.Location = new System.Drawing.Point(153, 414);
            this.buttonCloud.Name = "buttonCloud";
            this.buttonCloud.Size = new System.Drawing.Size(144, 23);
            this.buttonCloud.TabIndex = 2;
            this.buttonCloud.Text = "Cloud";
            this.buttonCloud.UseVisualStyleBackColor = true;
            this.buttonCloud.Click += new System.EventHandler(this.onCloudClick);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Location = new System.Drawing.Point(3, 414);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(144, 23);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.onPlayClick);
            // 
            // FormGameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 478);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGameList";
            this.Text = "GOGCloudSync";
            this.Load += new System.EventHandler(this.onLoad);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox gameList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonCloud;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button buttonSaveFolder;
        private System.Windows.Forms.Button buttonCloudFolder;
        private System.Windows.Forms.FolderBrowserDialog browseCloudFolder;
    }
}

