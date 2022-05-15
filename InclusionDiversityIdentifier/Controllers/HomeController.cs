using ExcelDataReader;
using InclusionDiversityIdentifier.Database;
using InclusionDiversityIdentifier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI;
using RestSharp;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InclusionDiversityIdentifier.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using InclusionDiversityIdentifier.BusinessLayer;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;

namespace InclusionDiversityIdentifier.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;
        private DiversityIdentifierConfig _diversityConfig;
        private readonly IConfiguration _config = null;
        private readonly BusinessGoogleSearch _businessSearch;
        private readonly SearchBusinessDiverse _searchBusinessDiverse;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext ctx, IOptions<DiversityIdentifierConfig> opts, IConfiguration configuration, BusinessGoogleSearch search, SearchBusinessDiverse diverseBusiness)
        {
            _logger = logger;
            _context = ctx;
            _diversityConfig = opts.Value;
            _config = configuration;
            _businessSearch = search;
            _searchBusinessDiverse = diverseBusiness;
        }

        public IActionResult Index()
        {
           
            //Get List of Business Names to Search on Google API
            var googleSearchStrings = GetBusinessNames();

            //Google search and retrive business URLs
            //var businessUrls = _businessSearch.GetBusinessUrls("CP&y, Inc.");

            //search Business diverse
            //var isWomenOwned = _searchBusinessDiverse.CheckBusinessIsWomenOwned(businessUrls.FirstOrDefault());

            //scrape html for text
            //var text = WebScrapeUrl(businessUrls.FirstOrDefault());

            //check for keywords(women,veterans,deversity ...)
            //not found scrape hmtl for page links check links (about, awards, culture)
            //google to link that have tag  (about, awards, culture)



            //var query2 = _context.Businesses;
            ViewBag.Secret1 = _config.GetConnectionString("DefaultConnection");
            ViewBag.Secret2 = _diversityConfig.GoogleSearchApiKey;
            ViewBag.Secret3 = _diversityConfig.GoogleSearchCxKey;
            ViewBag.Secret4 = _diversityConfig.ScrapingBeeApiKey;


            return View();
        }

        private List<string> GetBusinessNames()
        {
            var businessNames = new List<string>();
            var businessTable = _context.Businesses;
            List<Business> businessList = businessTable.ToList();
            foreach (var buisness in businessList)
            {
                businessNames.Add(buisness.dunsName);
            }

            return businessNames;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
