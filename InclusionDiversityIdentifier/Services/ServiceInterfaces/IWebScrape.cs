using InclusionDiversityIdentifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Services.ServiceInterfaces
{
    public interface IWebScrape
    {
        Task<WebScrapeResponse> WebExtractLinkPage(string searchUrl);
        Task<WebScrapeResponse> WebExtractHtmlPageAsync(string searchUrl);

    }
}
