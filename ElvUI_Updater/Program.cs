using HtmlAgilityPack;
using System;
using System.IO;
using System.IO.Compression;

namespace ElvUI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHelper WebHelper = new WebHelper();
            FileSystemHelper FileSystemHelper = new FileSystemHelper();
            HtmlDocument doc;
            string downloadLink, directory;

            try
            {
                Console.WriteLine("Searching for World of Warcraft Installation");
                directory = FileSystemHelper.LocateWorldOfWarcraftInstallation();

                if (string.IsNullOrEmpty(directory))
                {
                    Console.WriteLine("World of Warcraft Installation not found, press any key to exit.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Downloading HTML Document");
                doc = WebHelper.GetHTMLDocument("https://www.tukui.org/download.php?ui=elvui");

                Console.WriteLine("Determining Version");
                downloadLink = WebHelper.GetDownloadLink(doc);

                Console.WriteLine("Downloading File");
                byte[] b = WebHelper.DownloadFile("http://www.tukui.org/" + downloadLink);

                Console.WriteLine("Updating Local Files");
                MemoryStream stream = new MemoryStream(b);
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

                Console.WriteLine("Success! Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("Please give this to the developer.");
                Console.ReadKey();
            }
        }
    }
}
