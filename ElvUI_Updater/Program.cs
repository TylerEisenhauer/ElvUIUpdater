using HtmlAgilityPack;
using System;

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
                directory = FileSystemHelper.LocateWorldOfWarcraftInstallation(GameVersion._retail_);

                if (string.IsNullOrEmpty(directory))
                {
                    Console.WriteLine("World of Warcraft Installation not found, skipping.");
                }
                else
                {
                    Console.WriteLine("Downloading HTML Document");
                    doc = WebHelper.GetHTMLDocument("https://www.tukui.org/download.php?ui=elvui");

                    Console.WriteLine("Determining Version");
                    downloadLink = WebHelper.GetDownloadLink(doc);

                    Console.WriteLine("Downloading File");
                    byte[] b = WebHelper.DownloadFile("http://www.tukui.org/" + downloadLink);

                    Console.WriteLine("Updating Local Files");
                    FileSystemHelper.ExtractZipFile(b, directory);
                }

                directory = string.Empty;

                Console.WriteLine("Searching for World of Warcraft Classic Installation");
                directory = FileSystemHelper.LocateWorldOfWarcraftInstallation(GameVersion._classic_era_);

                if (string.IsNullOrEmpty(directory))
                {
                    Console.WriteLine("World of Warcraft Classic Installation not found, skipping.");
                }
                else
                {
                    Console.WriteLine("Downloading File");
                    byte[] b = WebHelper.DownloadFile("http://www.tukui.org/classic-addons.php?download=2");

                    Console.WriteLine("Updating Local Files");
                    FileSystemHelper.ExtractZipFile(b, directory);
                }

                Console.WriteLine("Searching for World of Warcraft Burning Crusade Classic Installation");
                directory = FileSystemHelper.LocateWorldOfWarcraftInstallation(GameVersion._classic_);

                if (string.IsNullOrEmpty(directory))
                {
                    Console.WriteLine("World of Warcraft Burning Crusade Classic Installation not found, skipping.");
                }
                else
                {
                    Console.WriteLine("Downloading File");
                    byte[] b = WebHelper.DownloadFile("https://www.tukui.org/classic-tbc-addons.php?download=2");

                    Console.WriteLine("Updating Local Files");
                    FileSystemHelper.ExtractZipFile(b, directory);
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
