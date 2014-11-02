using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOGCloud
{
    public partial class FormMergeConflict : Form
    {
        public FormMergeConflict()
        {
            InitializeComponent();
            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;

        }

        public List<Panel> hostPanels = new List<Panel>();

        public FilePairList fileList = null;

        public void addPanel(RelativeFilePair file)
        {   
            var hostPanel = new Panel();
            var cloudRadio = new RadioButton();
            var localRadio = new RadioButton();
            var descriptionLabel = new Label();
            var cloudSizeLabel = new Label();
            var localSizeLabel = new Label();

            hostPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            hostPanel.Controls.Add(descriptionLabel);
            hostPanel.Controls.Add(cloudRadio);
            hostPanel.Controls.Add(cloudSizeLabel);
            hostPanel.Controls.Add(localRadio);
            hostPanel.Controls.Add(localSizeLabel);
            hostPanel.Location = new System.Drawing.Point(3, 3 + 103 * hostPanels.Count);
            var choicePanel = new FileChoicePanel()
            {
                hostPanel = hostPanel,
                cloudRadio = cloudRadio,
                localRadio = localRadio,
                descriptionLabel = descriptionLabel,
                cloudSizeLabel = cloudSizeLabel,
                localSizeLabel = localSizeLabel,
                filePair = file
            };

            hostPanel.Tag = choicePanel;

            hostPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.onItemLayout);

            cloudRadio.AutoSize = true;
            cloudRadio.Location = new System.Drawing.Point(7, 24);
            cloudRadio.Size = new System.Drawing.Size(165, 17);
            cloudRadio.Text = "Keep cloud file";
            cloudRadio.UseVisualStyleBackColor = true;

            cloudRadio.Tag = choicePanel;
            cloudRadio.CheckedChanged += new EventHandler(this.onRadioChecked);

            localRadio.AutoSize = true;
            localRadio.Checked = true;
            localRadio.Location = new System.Drawing.Point(409, 24);
            localRadio.Size = new System.Drawing.Size(195, 17);
            localRadio.Text = "Keep local file";
            localRadio.UseVisualStyleBackColor = true;
            localRadio.Tag = choicePanel;
            localRadio.CheckedChanged += new EventHandler(this.onLocalChecked);

            cloudSizeLabel.AutoSize = true;
            cloudSizeLabel.Location = new System.Drawing.Point(4, 45);
            cloudSizeLabel.Size = new System.Drawing.Size(225, 13);
            cloudSizeLabel.Text = "Size " + StringUtils.formatSize(file.cloudSize) + " ; Last modified " + file.cloudTime.ToString();

            localSizeLabel.AutoSize = true;
            localSizeLabel.Location = new System.Drawing.Point(406, 45);
            localSizeLabel.Size = new System.Drawing.Size(225, 13);
            localSizeLabel.Text = "Size " + StringUtils.formatSize(file.localSize) + " ; Last modified " + file.localTime.ToString() ;



            descriptionLabel.AutoSize = true;
            descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            descriptionLabel.Location = new System.Drawing.Point(4, 8);
            descriptionLabel.Size = new System.Drawing.Size(95, 13);
            descriptionLabel.Text = file.relPath;

            cloudRadio.Checked = true;
            if (file.localTime > file.cloudTime || (file.localTime == file.cloudTime && file.localSize > file.cloudSize))
            {
                localRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                localRadio.Checked = true;
                choicePanel.filePair.keepLocal = true;
            }
            else if (file.localTime < file.cloudTime || (file.localTime == file.cloudTime && file.localSize < file.cloudSize))
            {
                cloudRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

            panel1.Controls.Add(hostPanel);
            hostPanels.Add(hostPanel);
        }

        private void onPanelLayout(object sender, LayoutEventArgs e)
        {
            for (var i = 0; i < hostPanels.Count; i++)
            {
                hostPanels[i].Width = panel1.Width - panel1.Padding.Horizontal - hostPanels[i].Margin.Horizontal - (panel1.VerticalScroll.Visible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0);
            }
        }

        private void onItemLayout(object sender, LayoutEventArgs e)
        {
            FileChoicePanel c = (FileChoicePanel)((sender as Control).Tag);

            c.localRadio.Left = c.hostPanel.Width / 2;
            c.localSizeLabel.Left = c.hostPanel.Width / 2;
        }

        private void onLocalChecked(object sender, EventArgs e)
        {
            FileChoicePanel c = (FileChoicePanel)((sender as Control).Tag);

            c.filePair.keepLocal = true;
        }

        private void onRadioChecked(object sender, EventArgs e)
        {
            FileChoicePanel c = (FileChoicePanel)((sender as Control).Tag);

            c.filePair.keepLocal = false;
        }

        private void onClickAbort(object sender, EventArgs e)
        {
            ok = false;
            Close();
        }

        public bool ok = false;

        private void onClickOK(object sender, EventArgs e)
        {
            ok = true;
            Close();
        }

        private void cloudSelectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < hostPanels.Count; i++)
            {
                FileChoicePanel c = (FileChoicePanel)(hostPanels[i].Tag);
                c.cloudRadio.Checked = true;
            }
        }

        private void buttonLocalSelectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < hostPanels.Count; i++)
            {
                FileChoicePanel c = (FileChoicePanel)(hostPanels[i].Tag);
                c.localRadio.Checked = true;
            }
        }

        private void buttonKeepNewest_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < hostPanels.Count; i++)
            {
                FileChoicePanel c = (FileChoicePanel)(hostPanels[i].Tag);
                if (c.filePair.localTime > c.filePair.cloudTime)
                    c.localRadio.Checked = true;
                else
                    c.cloudRadio.Checked = true;
            }
        }

        public void setToFileList(FilePairList list)
        {
            fileList = list;
            for (var i = 0; i < fileList.files.Count; i++)
            {
                addPanel(fileList.files[i]);
            }
        }
    }

    public class FileChoicePanel{
        public Panel hostPanel;
        public RadioButton cloudRadio;
        public RadioButton localRadio;
        public Label descriptionLabel;
        public Label cloudSizeLabel;
        public Label localSizeLabel;


        public RelativeFilePair filePair = null;
    }
}
