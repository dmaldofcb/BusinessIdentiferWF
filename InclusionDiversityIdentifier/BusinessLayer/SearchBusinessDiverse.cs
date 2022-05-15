using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.BusinessLayer
{
    public class SearchBusinessDiverse
    {
        private readonly IWebScrape _scraperService;

        public SearchBusinessDiverse(IWebScrape scraperService)
        {
            _scraperService = scraperService;
        }

        public bool CheckBusinessIsWomenOwned(string businessUrl)
        {
            var html = _scraperService.WebExtractHtmlPage(businessUrl);
            return false;
        }

    }
}
