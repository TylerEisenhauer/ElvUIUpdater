using ElvUI_Updater.ApiClients.ElvUI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElvUI_Updater.ApiClients.ElvUI
{
    public class ElvUIClient
    {
        private readonly RestClient _client;

        public ElvUIClient()
        {
            _client = new RestClient("https://api.tukui.org");
        }

        public GetAddonResponse GetAddonData(string addon)
        {
            var req = new RestRequest($"/v1/addon/{addon}", Method.Get);

            var response = _client.Execute<GetAddonResponse>(req);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new Exception($"Could not get addon data Status: {response.StatusCode}, Content: {response.Content}");
        }
    }
}
