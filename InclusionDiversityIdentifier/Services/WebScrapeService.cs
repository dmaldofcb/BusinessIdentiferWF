using InclusionDiversityIdentifier.Config;
using InclusionDiversityIdentifier.Models;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace InclusionDiversityIdentifier.Services
{
    public class WebScrapeService : IWebScrape
    {
        private DiversityIdentifierConfig _diversityConfig;
        private readonly string baseUrl = "https://app.scrapingbee.com/api/v1/";
        public WebScrapeService(IOptions<DiversityIdentifierConfig> opts)
        {
            _diversityConfig = opts.Value;
        }

        public WebScrapeResponse WebExtractHtmlPage(string searchUrl)
        {
            var client = new RestClient(baseUrl);

            RestRequest request = RequestExtractHtml(searchUrl, "false", "{ \"text\": \"body\"}");

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonSerializer.Deserialize<WebScrapeResponse>(response.Content);

                return content;
            }
            else
            {
                return null;
            }
        }

        public WebScrapeResponse WebExtractLinkPage(string searchUrl)
        {

            throw new NotImplementedException();
        }

        private RestRequest RequestExtractHtml(string searchUrl, string param1, string param2)
        {
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Timeout = -1;
            request.AddQueryParameter("api_key", _diversityConfig.ScrapingBeeApiKey);
            request.AddQueryParameter("url", searchUrl);
            request.AddQueryParameter("render_js", param1);
            request.AddQueryParameter("extract_rules", param2);

            return request;
        }
    }
}
