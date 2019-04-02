using System.IO;

namespace ElvUI_Updater
{
    public class FileSystemHelper
    {
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
