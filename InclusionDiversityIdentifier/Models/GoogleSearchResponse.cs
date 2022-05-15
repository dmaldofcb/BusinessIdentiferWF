using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace InclusionDiversityIdentifier.Models
{
    public class GoogleSearchResponse
    {

        [JsonPropertyName("items")]
        public List<SearchItems> searchItems { get; set; }

    }

    public class SearchItems
    {
        [JsonPropertyName("title")]
        public string searchTitle { get; set; }
        [JsonPropertyName("link")]
        public string searchLink { get; set; }
        [JsonPropertyName("snippet")]
        public string searchDescription { get; set; }
    }
}
