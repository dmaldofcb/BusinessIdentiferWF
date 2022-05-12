using InclusionDiversityIdentifier.Database;
using InclusionDiversityIdentifier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext ctx)
        {
            _logger = logger;
            _context = ctx;
        }

        public IActionResult Index()
        {
            var query = _context.Businesses;
            return View(query.ToList());
        }
        //[HttpGet]
        //public ActionResult GetBusiness()
        //{
            

        //}
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
