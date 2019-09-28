using System.IO;
using System.IO.Compression;

namespace ElvUI_Updater
{
    public class FileSystemHelper
    {
        public string LocateWorldOfWarcraftInstallation(GameVersion version)
        {
            string directory = string.Empty;

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {

                directory = $"{drive.Name}Program Files (x86)/World of Warcraft/{version.ToString("g")}/Interface/AddOns";
                bool found = Directory.Exists(directory);
                if (found) { break; }
                directory = $"{drive.Name}World of Warcraft/{version.ToString("g")}/Interface/AddOns";
                found = Directory.Exists(directory);
                if (found) { break; }
            }

            return directory;
        }

        public void ExtractZipFile(byte[] zipFile, string directory)
        {
            MemoryStream stream = new MemoryStream(zipFile);
            using (ZipArchive archive = new ZipArchive(stream))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (string.IsNullOrWhiteSpace(entry.Name))
                    {
                        //Required incase the user does not already have an ElvUI directory
                        Directory.CreateDirectory(Path.Combine(directory, entry.FullName));
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
