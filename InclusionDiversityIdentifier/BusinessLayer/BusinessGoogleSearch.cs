using Google.Apis.CustomSearchAPI.v1.Data;
using InclusionDiversityIdentifier.Models;
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

        public async Task<BusinessDiverseInfo> GetBusinessUrls(Business business)
        {
            List<string> businessSearchLinks = new List<string>();
            BusinessDiverseInfo bus = new BusinessDiverseInfo();
            var searchResults = await _googleSearchService.PerformGoogleSearch(business.dunsName);
            if(searchResults != null)
            {
                foreach (Result result in searchResults.Items)
                {
                    businessSearchLinks.Add(result.Link);
                }
                MapBusinessFields(bus, business,businessSearchLinks);
            }
            return bus;
        }

        private void MapBusinessFields(BusinessDiverseInfo bus, Business business, List<string> links)
        {
            bus.businessName = business.dunsName;
            bus.dunsNumId = business.dunsNumId;
            bus.urlLink = links;
        }
    }
}
