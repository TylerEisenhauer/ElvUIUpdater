using System.IO;

namespace ElvUI_Updater
{
    public class FileSystemHelper
    {
        public void DirectoryCopy(string source, string destination)
        {
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + source);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destination, file.Name);
                file.CopyTo(temppath, true);
            }


            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destination, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }

        public string LocateWorldOfWarcraftInstallation()
        {
            string directory = "";

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                directory = drive.Name + "Program Files (x86)/World of Warcraft/_retail_/Interface/AddOns";
                bool found = Directory.Exists(directory);
                if (found) { break; }
                directory = drive.Name + "World of Warcraft/_retail_/Interface/AddOns";
                found = Directory.Exists(directory);
                if (found) { break; }
            }

            return directory;
        }
    }
}
