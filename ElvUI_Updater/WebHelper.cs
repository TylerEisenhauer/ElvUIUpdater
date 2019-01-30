using System.Net;
using HtmlAgilityPack;

namespace ElvUI_Updater
{
    public class WebHelper
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
        public string GetDownloadLink(HtmlDocument doc)
        {
            string downloadLink;

            HtmlNode href = doc.DocumentNode.SelectSingleNode("//*[@id=\"download\"]/div/div/a");
            downloadLink = href.Attributes["href"].Value;

            return downloadLink;
        }
        public byte[] DownloadFile(string url)
        {
            byte[] file;

            file = webClient.DownloadData(url);

            return file;
        }

    }
}
