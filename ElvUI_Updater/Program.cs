using ElvUI_Updater.ApiClients.ElvUI;
using HtmlAgilityPack;
using RestSharp;
using System;

namespace ElvUI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            ElvUIClient client = new ElvUIClient();
            WebHelper WebHelper = new WebHelper();

            try
            {
                Console.WriteLine("Getting download link");
                var data = client.GetAddonData("elvui");

                Console.WriteLine("Downloading File");
                byte[] b = WebHelper.DownloadFile(data.Url);

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
