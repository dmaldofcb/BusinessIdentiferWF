using Google.Apis.CustomSearchAPI.v1.Data;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.BusinessLayer
{
    public class BusinessGoogleSearch
    {
        private readonly IGoogleSearch _googleSearchService;

        public BusinessGoogleSearch(IGoogleSearch googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        public List<string> GetBusinessUrls(string businessName)
        {
            List<string> businessSearchLinks = new List<string>();
            var searchResults = _googleSearchService.PerformGoogleSearch(businessName);
            if(searchResults != null)
            {
                foreach (Result result in searchResults.Items)
                {
                    businessSearchLinks.Add(result.Link);
                }
            }
            return businessSearchLinks;
        }
    }
}
