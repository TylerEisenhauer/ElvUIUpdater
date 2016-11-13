using System;
using System.IO;
using System.IO.Compression;
using HtmlAgilityPack;
using ElvUI_Updater.Interfaces;

namespace ElvUI_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            IWebHelper WebHelper = factory.GetWebHelper();
            IFileSystemHelper FileSystemHelper = factory.GetFileSystemHelper();
            HtmlDocument doc;
            string version, directory;

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
                doc = WebHelper.GetHTMLDocument("http://www.tukui.org/dl.php");

                Console.WriteLine("Determining Version");
                version = WebHelper.GetElvUIVersion(doc);

                Console.WriteLine("Downloading File");
                byte[] b = WebHelper.DownloadFile("http://www.tukui.org/downloads/elvui-" + version + ".zip");

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
