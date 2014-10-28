using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace GOGCloud
{

    public class Game
    {
        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);


        public Game(RegistryKey subKey)
        {
            name = (string)subKey.GetValue("GAMENAME", "");
            code = (string)subKey.GetValue("CODE", "");
            installPath = (string)subKey.GetValue("PATH", "");
            savePath = (string)subKey.GetValue("SAVEGAMEFOLDER", "");
            launchCmd = (string)subKey.GetValue("EXE", "");
            launchArgs = (string)subKey.GetValue("LAUNCHPARAM", "");
            launchDir = (string)subKey.GetValue("WORKINGDIR", "");
            if (name == "")
                throw new Exception("Game is nameless");
        }

        public bool isCloudInUse()
        {
            return isCloudExistent() && Directory.GetFileSystemEntries(getCloudDir()).Length > 0;
        }

        public bool isLocalInUse()
        {
            return isLocalExistent() && Directory.GetFileSystemEntries(savePath).Length > 0;
        }

        public bool isCloudExistent()
        {
            return Directory.Exists(getCloudDir());
        }

        public bool isLocalExistent()
        {
            return Directory.Exists(savePath);
        }

        public bool isCloudEqual()
        {

            var modDict = new Dictionary<string, DateTime[]>();
            return false;
        }

        public bool isCloudConflicted()
        {
            return false;
        }

        public bool enableCloud()
        {
            if (isCloudEnabled())
                return false;
            var moveOK = FileUtils.mergeDirectories(getCloudDir(), savePath);
            if (moveOK)
            {
                FileUtils.CreateSymbolicLink(savePath, getCloudDir(), 1);
            }

            return true;
        }

        public bool disableCloud()
        {
            if (!isCloudEnabled())
                return false;

            Directory.Delete(savePath);
            Directory.CreateDirectory(savePath);
            FileUtils.copyDir(getCloudDir(), savePath);

            return true;

        }

        public string name;
        public string code;
        public string keyName;
        public int keyRoot;
        public string installPath;
        public string savePath;
        public string launchCmd;
        public string launchArgs;
        public string launchDir;
        public string getCloudDir()
        {
            return StringUtils.stripTrailingBackslash(GOGCloud.Properties.Settings.Default.CloudPath) + "\\" + code;
        }

        public void launch()
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(launchCmd);
            p.StartInfo.WorkingDirectory = launchDir == "" ? launchCmd.Substring(0, launchCmd.LastIndexOf("\\")) : launchDir;
            p.StartInfo.Arguments = launchArgs;
            p.Start();
        }

        public bool isCloudEnabled()
        {
            if (isLocalExistent())
            {
                var info = new FileInfo(savePath);
                return info.Attributes.HasFlag(FileAttributes.ReparsePoint);
            }
            return false;
        }

        public bool hasDedicatedSaveFolder()
        {
            return StringUtils.stripTrailingBackslash(installPath).ToLower() != StringUtils.stripTrailingBackslash(savePath).ToLower();
        }

        override public string ToString()
        {
            return name;
        }
    }

}