using InclusionDiversityIdentifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Services.ServiceInterfaces
{
    public interface IWebScrape
    {
        WebScrapeResponse WebExtractLinkPage(string searchUrl);
        WebScrapeResponse WebExtractHtmlPage(string searchUrl);

    }
}
