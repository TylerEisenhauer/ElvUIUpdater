using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ElvUI_Updater.ApiClients.ElvUI.Models
{
    public class GetAddonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("changelog_url")]
        public string ChangelogUrl { get; set; }

        [JsonPropertyName("ticket_url")]
        public string TicketUrl { get; set; }

        [JsonPropertyName("git_url")]
        public string GitUrl { get; set; }

        [JsonPropertyName("patch")]
        public IList<string> Patch { get; set; }

        [JsonPropertyName("last_update")]
        public string LastUpdate { get; set; }

        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; }

        [JsonPropertyName("donate_url")]
        public string DonateUrl { get; set; }

        [JsonPropertyName("small_desk")]
        public string SmallDescripion { get; set; }

        [JsonPropertyName("screenshot_url")]
        public string ScreenshotUrl { get; set; }

        [JsonPropertyName("directories")]
        public IList<string> Directories { get; set; }
    }
}
