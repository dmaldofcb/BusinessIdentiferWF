using InclusionDiversityIdentifier.Config;
using InclusionDiversityIdentifier.Models;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Google.Apis.Services;
using static Google.Apis.Services.BaseClientService;
using static Google.Apis.CustomSearchAPI.v1.CseResource;
using Microsoft.Extensions.Options;

namespace InclusionDiversityIdentifier.Services
{
    public class GoogleSearchService : IGoogleSearch
    {
        private DiversityIdentifierConfig _diversityConfig;

        public GoogleSearchService(IOptions<DiversityIdentifierConfig> opts)
        {
            _diversityConfig = opts.Value;
        }

        public async Task<Search> PerformGoogleSearch(string querySearch)
        {
            CustomSearchAPIService searchService = CustomSearchApiSetup();
            ListRequest searchRequest = SetupSearch(searchService, querySearch);
            try
            {
                Search searchResult = await searchRequest.ExecuteAsync();

                return searchResult;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        private ListRequest SetupSearch(CustomSearchAPIService searchService, string searchQ)
        {
            //metadata of our specific search engine
            ListRequest listRequest = searchService.Cse.List();
            listRequest.Cx = _diversityConfig.GoogleSearchCxKey;
            listRequest.Q = searchQ;

            return listRequest;
        }

        private CustomSearchAPIService CustomSearchApiSetup()
        {
            //initialize client service
            Initializer initial = new Initializer();
            initial.ApiKey = _diversityConfig.GoogleSearchApiKey;

            CustomSearchAPIService customeSearchService = new CustomSearchAPIService(initial);

            return customeSearchService;
        }
    }
}
