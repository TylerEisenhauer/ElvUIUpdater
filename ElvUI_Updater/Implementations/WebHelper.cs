using System.Net;
using HtmlAgilityPack;
using ElvUI_Updater.Interfaces;

namespace ElvUI_Updater.Implementations
{
    public class WebHelper : IWebHelper
    {
        private WebClient webClient;
        public WebHelper()
        {
            webClient = new WebClient();
        }
        public HtmlDocument GetHTMLDocument(string url)
        {
            HtmlDocument doc = new HtmlDocument();

            string html = webClient.DownloadString(url);
            doc.LoadHtml(html);

            return doc;
        }
        public string GetElvUIVersion(HtmlDocument doc)
        {
            string version;

            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes("//span[@class='VIP']");
            version = hrefs[1].InnerText;

            return version;
        }
        public byte[] DownloadFile(string url)
        {
            byte[] file;

            file = webClient.DownloadData(url);

            return file;
        }

    }
}
