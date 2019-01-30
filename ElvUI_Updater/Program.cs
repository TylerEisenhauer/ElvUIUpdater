using System;
using System.IO;
using System.IO.Compression;
using HtmlAgilityPack;

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

                File.WriteAllBytes(@"C:\temp.zip", b);
                Console.WriteLine("Copying Files");
                ZipFile.ExtractToDirectory("C:/temp.zip", "c:/tempelvuifile");
                
                FileSystemHelper.DirectoryCopy("C:/tempelvuifile/ElvUI", directory + "/ElvUI");
                FileSystemHelper.DirectoryCopy("C:/tempelvuifile/ElvUI_Config", directory + "/ElvUI_Config");

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
            finally
            {
                File.Delete(@"C:\temp.zip");
                Directory.Delete(@"C:\tempelvuifile", true);
            }
        }
    }
}
