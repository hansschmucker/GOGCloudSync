namespace GOGCloud
{
    partial class FormMergeConflict
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
            this.buttonCloudSelectAll = new System.Windows.Forms.Button();
            this.buttonKeepNewest = new System.Windows.Forms.Button();
            this.buttonLocalSelectAll = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonResolveConflictsOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCloudSelectAll
            // 
            this.buttonCloudSelectAll.Location = new System.Drawing.Point(12, 12);
            this.buttonCloudSelectAll.Name = "buttonCloudSelectAll";
            this.buttonCloudSelectAll.Size = new System.Drawing.Size(95, 23);
            this.buttonCloudSelectAll.TabIndex = 0;
            this.buttonCloudSelectAll.Text = "Keep Cloud";
            this.buttonCloudSelectAll.UseVisualStyleBackColor = true;
            this.buttonCloudSelectAll.Click += new System.EventHandler(this.cloudSelectAll_Click);
            // 
            // buttonKeepNewest
            // 
            this.buttonKeepNewest.Location = new System.Drawing.Point(113, 12);
            this.buttonKeepNewest.Name = "buttonKeepNewest";
            this.buttonKeepNewest.Size = new System.Drawing.Size(95, 23);
            this.buttonKeepNewest.TabIndex = 1;
            this.buttonKeepNewest.Text = "Keep Newest";
            this.buttonKeepNewest.UseVisualStyleBackColor = true;
            this.buttonKeepNewest.Click += new System.EventHandler(this.buttonKeepNewest_Click);
            // 
            // buttonLocalSelectAll
            // 
            this.buttonLocalSelectAll.Location = new System.Drawing.Point(214, 12);
            this.buttonLocalSelectAll.Name = "buttonLocalSelectAll";
            this.buttonLocalSelectAll.Size = new System.Drawing.Size(95, 23);
            this.buttonLocalSelectAll.TabIndex = 2;
            this.buttonLocalSelectAll.Text = "Keep Local";
            this.buttonLocalSelectAll.UseVisualStyleBackColor = true;
            this.buttonLocalSelectAll.Click += new System.EventHandler(this.buttonLocalSelectAll_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(13, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(825, 424);
            this.panel1.TabIndex = 3;
            this.panel1.Layout += new System.Windows.Forms.LayoutEventHandler(this.onPanelLayout);
            // 
            // buttonResolveConflictsOK
            // 
            this.buttonResolveConflictsOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResolveConflictsOK.Location = new System.Drawing.Point(763, 472);
            this.buttonResolveConflictsOK.Name = "buttonResolveConflictsOK";
            this.buttonResolveConflictsOK.Size = new System.Drawing.Size(75, 23);
            this.buttonResolveConflictsOK.TabIndex = 5;
            this.buttonResolveConflictsOK.Text = "OK";
            this.buttonResolveConflictsOK.UseVisualStyleBackColor = true;
            this.buttonResolveConflictsOK.Click += new System.EventHandler(this.onClickOK);
            // 
            // FormMergeConflict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 507);
            this.Controls.Add(this.buttonResolveConflictsOK);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonLocalSelectAll);
            this.Controls.Add(this.buttonKeepNewest);
            this.Controls.Add(this.buttonCloudSelectAll);
            this.Name = "FormMergeConflict";
            this.Text = "Potential Conflict";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCloudSelectAll;
        private System.Windows.Forms.Button buttonKeepNewest;
        private System.Windows.Forms.Button buttonLocalSelectAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonResolveConflictsOK;
    }
}