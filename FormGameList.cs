using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GOGCloud
{

    public partial class FormGameList : Form
    {

        public FormGameList()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            var keyGOGCom=Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("GOG.com");
            if (keyGOGCom == null)
                return;

            var games = new List<Game>();

            var directSubKeys = keyGOGCom.GetSubKeyNames();

            if(directSubKeys!=null){

                for (var i = 0; i < directSubKeys.Length; i++)
                {
                    if (directSubKeys[i].Substring(0, 3) != "GOG")
                        continue;

                    try
                    {
                        var game = new Game(keyGOGCom.OpenSubKey(directSubKeys[i]));
                        game.keyRoot = 0;
                        game.keyName = directSubKeys[i];
                        games.Add(game);
                    }
                    catch (Exception) { }
                }


                var keyGOGComGames = keyGOGCom.OpenSubKey("Games");

                if (keyGOGComGames != null)
                {
                    var gamesSubKeys = keyGOGComGames.GetSubKeyNames();

                    for (var i = 0; i < gamesSubKeys.Length; i++)
                    {
                        try
                        {
                            var game = new Game(keyGOGComGames.OpenSubKey(gamesSubKeys[i]));
                            game.keyRoot = 1;
                            game.keyName = gamesSubKeys[i];
                            games.Add(game);
                        }
                        catch (Exception) { }
                    }
                }

            }

            

            



            games.Sort((a, b) => (a.name.CompareTo(b.name)));

            for (var i = 0; i < games.Count; i++)
                gameList.Items.Add(games[i]);
        }

        private string syncAction = "NONE";


        private void updateGameView()
        {
            buttonSaveFolder.Enabled = true;
            buttonPlay.Enabled = true;

            var game = (Game)(gameList.SelectedItem);
            if (game.hasDedicatedSaveFolder())
            {
                buttonCloud.Enabled = true;
                if (game.isCloudEnabled())
                {
                    syncAction = "OFF";
                    buttonCloud.Text = "Disable Sync";
                }
                else
                {
                    syncAction = "ON";
                    buttonCloud.Text = "Enable Sync";
                }

            }
            else
            {
                syncAction = "NONE";
                buttonCloud.Enabled = false;
                buttonCloud.Text = "Sync not available";
            }
        }

        private void onGameSelection(object sender, EventArgs e)
        {
            updateGameView();
        }

        private void onPlayClick(object sender, EventArgs e)
        {
            ((Game)gameList.SelectedItem).launch();
        }

        private void onCloudClick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CloudPath == "")
                buttonCloudFolder_Click(null, null);

            if (Properties.Settings.Default.CloudPath == "" || !Directory.Exists(Properties.Settings.Default.CloudPath))
                return;

            switch (syncAction)
            {
                case "ON":
                    ((Game)gameList.SelectedItem).enableCloud();
                    updateGameView();
                    break;
                case "OFF":
                    ((Game)gameList.SelectedItem).disableCloud();
                    updateGameView();
                    break;
            }
        }

        private void onSyncToggleClick(object sender, EventArgs e)
        {
            var game=(Game)gameList.SelectedItem;
            if (game.hasDedicatedSaveFolder() && game.isCloudEnabled() && Directory.Exists(Properties.Settings.Default.CloudPath))
            {
                Process.Start(game.getCloudDir());
            }
            else if(Directory.Exists(game.savePath))
            {
                Process.Start(game.savePath);
            }
        }

        private void buttonCloudFolder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In order to sync any data, GOGCloudSync needs a sync provider like Dropbox, Box, GoogleDrive or OneDrive. Please select a folder inside the sync folder of one of these applications. " + (Properties.Settings.Default.CloudPath != "" ? "The current folder is " + Properties.Settings.Default.CloudPath : ""), "GOGCloudSync");
            if (Properties.Settings.Default.CloudPath != "")
            {
                
                browseCloudFolder.SelectedPath = Properties.Settings.Default.CloudPath;
            }
            browseCloudFolder.ShowDialog();
            if (browseCloudFolder.SelectedPath != "")
            {
                Properties.Settings.Default.CloudPath = browseCloudFolder.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }



}
