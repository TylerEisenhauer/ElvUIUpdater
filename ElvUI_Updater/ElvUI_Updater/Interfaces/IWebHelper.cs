using HtmlAgilityPack;

namespace ElvUI_Updater.Interfaces
{
    interface IWebHelper
    {
        HtmlDocument GetHTMLDocument(string url);
        string GetElvUIVersion(HtmlDocument doc);
        byte[] DownloadFile(string url);
    }
}
