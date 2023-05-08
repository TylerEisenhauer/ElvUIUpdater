using HtmlAgilityPack;
using System;

namespace ElvUI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHelper WebHelper = new WebHelper();

            try
            {
                Console.WriteLine("Downloading HTML Document");
                HtmlDocument doc = WebHelper.GetHTMLDocument("https://www.tukui.org/download.php?ui=elvui");

                Console.WriteLine("Determining Version");
                string downloadLink = WebHelper.GetDownloadLink(doc);

                Console.WriteLine("Downloading File");
                byte[] b = WebHelper.DownloadFile("http://www.tukui.org/" + downloadLink);

                foreach (var item in (GameVersion[])Enum.GetValues(typeof(GameVersion)))
                {
                    Console.WriteLine($"Searching for World of Warcraft {item} Installation");
                    string directory = FileSystemHelper.LocateWorldOfWarcraftInstallation(item);

                    if (string.IsNullOrEmpty(directory))
                    {
                        Console.WriteLine($"World of Warcraft {item} Installation not found, skipping.");
                    }
                    else
                    {
                        Console.WriteLine("Updating Local Files");
                        FileSystemHelper.ExtractZipFile(b, directory);
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
