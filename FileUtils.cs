using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
namespace GOGCloud
{

    public static class FileUtils
    {
        /**
         * Creates a symbolic link for a file if dwFlags is 0 or a directory if dwFlags is 1
         */
        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        /**
         * Returns a list containing all FILES inside the given directory as relative paths (without backslash at the beginning
         */
        public static List<string> collectRelativeFilePaths(string directory)
        {
            var self = StringUtils.stripTrailingBackslash(directory).ToLower() + "\\";
            var files = collectRelativeFilePaths(directory, null, self.Length);
            files.Sort((a, b) => (a.CompareTo(b)));
            return files;
        }

        /**
         * Fills a list containing all FILES inside the given directory as relative paths (without backslash at the beginning
         * If paths is not null, all results will be added to it, otherwise a new list is created.
         * All results have their first relOffset characters removed to produce a relative path.
         * Returns paths or a new list if paths is null.
         */
        public static List<string> collectRelativeFilePaths(string directory, List<string> paths, int relOffset)
        {
            if (paths == null)
                paths = new List<string>();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var files = Directory.GetFiles(directory);
            var dirs = Directory.GetDirectories(directory);

            for (var i = 0; i < files.Length; i++)
            {
                paths.Add(files[i].Substring(relOffset).ToLower());
            }

            for (var i = 0; i < dirs.Length; i++)
            {
                collectRelativeFilePaths(dirs[i], paths, relOffset);
            }

            return paths;
        }

        /**
         * True if two files have the same size and the same writeTime
         */
        public static bool areFilesEqual(string file1, string file2)
        {

            var f1 = new FileInfo(file1);
            var f2 = new FileInfo(file2);

            if (f1.LastWriteTime != f2.LastWriteTime)
                return false;

            if (f1.Length != f2.Length)
                return false;

            return true;
        }

        /*
         * True if at least one file doesn't exist or they are identical
         */
        public static bool areFilesMergable(string file1, string file2)
        {
            if (!File.Exists(file1) || !File.Exists(file2))
                return true;

            if (FileUtils.areFilesEqual(file1, file2))
                return true;

            return false;
        }

        /*
         *  True if all FILES are identical and exist in both directories.
         */
        public static bool areDirectoriesEqual(string directory1, string directory2)
        {

            var dir1Base = StringUtils.stripTrailingBackslash(directory1).ToLower() + "\\";
            var dir2Base = StringUtils.stripTrailingBackslash(directory2).ToLower() + "\\";

            var dir1 = FileUtils.collectRelativeFilePaths(directory1);
            var dir2 = FileUtils.collectRelativeFilePaths(directory2);

            if (dir1.Count != dir2.Count)
                return false;

            for (var i = 0; i < dir1.Count; i++)
                if (dir1[i] != dir2[i] || !FileUtils.areFilesEqual(dir1Base + dir1[i], dir2Base + dir2[i]))
                    return false;

            return true;
        }


        /*
         * True if all files that exist in both directories are equal
         */
        public static bool areDirectoriesMergable(string directory1, string directory2)
        {
            var dir1Base = StringUtils.stripTrailingBackslash(directory1).ToLower() + "\\";
            var dir2Base = StringUtils.stripTrailingBackslash(directory2).ToLower() + "\\";

            if (!Directory.Exists(directory1) || !Directory.Exists(directory2))
                return true;
            var dir1 = FileUtils.collectRelativeFilePaths(directory1);
            var dir2 = FileUtils.collectRelativeFilePaths(directory2);

            if (dir1.Count == 0 || dir2.Count == 0)
                return true;

            var all = StringUtils.mergeList(dir1, dir2);

            for (var i = 0; i < all.Count; i++)
                if (!FileUtils.areFilesMergable(dir1Base + all[i], dir2Base + all[i]))
                    return false;

            return true;
        }

        /*
          * Returns a list of conflicted files
          */
        public static List<RelativeFilePair> getConflictedFiles(string cloudDirectory, string localDirectory)
        {
            var cloudDirectoryBase = StringUtils.stripTrailingBackslash(cloudDirectory).ToLower() + "\\";
            var localDirectoryBase = StringUtils.stripTrailingBackslash(localDirectory).ToLower() + "\\";

            if (!Directory.Exists(cloudDirectory) || !Directory.Exists(localDirectory))
                return new List<RelativeFilePair>();
            var dir1 = FileUtils.collectRelativeFilePaths(cloudDirectory);
            var dir2 = FileUtils.collectRelativeFilePaths(localDirectory);

            if (dir1.Count == 0 || dir2.Count == 0)
                return new List<RelativeFilePair>();

            var all = StringUtils.mergeList(dir1, dir2);

            var list = new List<RelativeFilePair>();
            for (var i = 0; i < all.Count; i++)
                if (!FileUtils.areFilesMergable(cloudDirectoryBase + all[i], localDirectoryBase + all[i]))
                {
                    var cloudFileInfo = new FileInfo(cloudDirectoryBase + all[i]);
                    var localFileInfo = new FileInfo(localDirectoryBase + all[i]);

                    list.Add(new RelativeFilePair() {
                        relPath = all[i],
                        localSize=localFileInfo.Length,
                        cloudSize=cloudFileInfo.Length,
                        localTime=localFileInfo.LastWriteTime,
                        cloudTime=cloudFileInfo.LastWriteTime
                    });
                }

            return list;
        }

        /**
         * Recreates directory-structure of src in target (does not delete)
         */
        public static void mirrorDirectories(string target, string src)
        {
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            var subdirs = Directory.GetDirectories(src);

            src = StringUtils.stripTrailingBackslash(src).ToLower() + "\\";
            target = StringUtils.stripTrailingBackslash(target).ToLower() + "\\";
            for (var i = 0; i < subdirs.Length; i++)
            {
                var relDir = subdirs[i].Substring(src.Length);
                FileUtils.mirrorDirectories(target + relDir, src + relDir);
            }
        }

        /**
         * Moves the content of src to target if there is no conflict, otherwise does nothing
         * Returns false on conflict, true on success
         */
        public static bool mergeDirectories(string target, string src)
        {
            src = StringUtils.stripTrailingBackslash(src).ToLower() + "\\";
            target = StringUtils.stripTrailingBackslash(target).ToLower() + "\\";

            if (!areDirectoriesMergable(target, src))
                return false;

            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            if (!Directory.Exists(src))
            {
                Directory.CreateDirectory(src);
                Directory.Delete(src);
                return true;
            }

            var srcFiles = FileUtils.collectRelativeFilePaths(src);

            FileUtils.mirrorDirectories(target, src);

            for (var i = 0; i < srcFiles.Count; i++)
            {
                if (FileUtils.areFilesEqual(src + srcFiles[i], target + srcFiles[i]))
                {
                    File.Delete(src + srcFiles[i]);
                }
                else if (!File.Exists(target + srcFiles[i]))
                {
                    File.Move(src + srcFiles[i], target + srcFiles[i]);
                }
                else
                {
                    throw new Exception("Unequal files in source and destination... this shouldn't happen");
                }
            }

            Directory.Delete(src, true);
            return true;
        }

        /*
         * Copies all files and directories from srcDir to dstDir, overwriting any existing files. Does not delete. Sets modified time of copies to original time.
         */
        public static void copyDir(string srcDir, string dstDir)
        {
            if (!Directory.Exists(srcDir))
                return;

            if (!Directory.Exists(dstDir))
                Directory.CreateDirectory(dstDir);

            srcDir = StringUtils.stripTrailingBackslash(srcDir).ToLower() + "\\";
            dstDir = StringUtils.stripTrailingBackslash(dstDir).ToLower() + "\\";

            var dirs = Directory.GetDirectories(srcDir);
            var files = Directory.GetFiles(srcDir);

            for (var i = 0; i < dirs.Length; i++)
            {
                var rel = dirs[i].Substring(srcDir.Length);
                copyDir(srcDir + rel, dstDir + rel);
            }

            for (var i = 0; i < files.Length; i++)
            {
                var rel = files[i].Substring(srcDir.Length);
                File.Copy(srcDir + rel, dstDir + rel, true);
                File.SetLastWriteTime(dstDir + rel, File.GetLastWriteTime(srcDir + rel));
            }

        }

    }

    public class RelativeFilePair
    {
        public string relPath = "";
        public DateTime localTime;
        public DateTime cloudTime;
        public long localSize;
        public long cloudSize;
        public bool keepLocal = false;
    }

    public class FilePairList
    {
        public List<RelativeFilePair> files = new List<RelativeFilePair>();
        public string localBasePath = "";
        public string cloudBasePath = "";
    }

}