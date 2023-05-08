using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ElvUI_Updater
{
    public static class FileSystemHelper
    {
        public static string LocateWorldOfWarcraftInstallation(GameVersion version)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                string directory = $"{drive.Name}Program Files (x86)/World of Warcraft/{version:g}/Interface/AddOns";
                bool found = Directory.Exists(directory);
                if (found) { return directory; }
                directory = $"{drive.Name}World of Warcraft/{version:g}/Interface/AddOns";
                found = Directory.Exists(directory);
                if (found) { return directory; }
            }

            return string.Empty;
        }

        public static void ExtractZipFile(byte[] zipFile, string directory)
        {
            MemoryStream stream = new MemoryStream(zipFile);
            using (ZipArchive archive = new ZipArchive(stream))
            {
                //Create the directories
                archive.Entries.Where(x => string.IsNullOrWhiteSpace(x.Name)).ToList().ForEach(x => Directory.CreateDirectory(Path.Combine(directory, x.FullName)));

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (string.IsNullOrWhiteSpace(entry.Name))
                    {
                        //This skips past folders that were created above
                        continue;
                    }
                    else
                    {
                        entry.ExtractToFile(Path.Combine(directory, entry.FullName), true);
                    }
                }
            }
        }
    }
}
