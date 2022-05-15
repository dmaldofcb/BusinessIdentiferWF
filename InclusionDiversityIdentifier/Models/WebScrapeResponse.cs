using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace InclusionDiversityIdentifier.Models
{
    public class WebScrapeResponse
    {
        [JsonPropertyName("text")]
        public string extractedHtmlText { get; set; }

        [JsonPropertyName("all_links")]
        public string extractedHtmlLinks { get; set; }

    }

    public class htmlLink
    {
        [JsonPropertyName("anchor")]
        public string anchorTag { get; set; }
        [JsonPropertyName("href")]
        public string link { get; set; }
    }
}
