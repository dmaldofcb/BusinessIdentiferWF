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
using InclusionDiversityIdentifier.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using InclusionDiversityIdentifier.BusinessLayer;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using InclusionDiversityIdentifier.Repository;
using ClosedXML.Excel;

namespace InclusionDiversityIdentifier.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusinessRepo _businessRepo;
        private readonly SearchBusinessDiverse _searchBusinessDiverse;
        public HomeController(ILogger<HomeController> logger, IBusinessRepo repo, SearchBusinessDiverse diverseBusiness)
        {
            _logger = logger;
            _businessRepo = repo;
            _searchBusinessDiverse = diverseBusiness;
        }

        public async Task<IActionResult> Index()
        {
            
            //Get List of Business Names to Search on Google API
            var businessList = await _businessRepo.GetAllBusiness();

            return View(businessList);
        }

        public async Task<IActionResult> Export()
        {
            DataTable dt = new DataTable("sheet1");
            dt.Columns.AddRange(new DataColumn[12] 
                                { 
                                    new DataColumn("dunsNum"),
                                    new DataColumn("dunsName"),
                                    new DataColumn("County"),
                                    new DataColumn("Street Address"),
                                    new DataColumn("City"),
                                    new DataColumn("State"),
                                    new DataColumn("Zip"),
                                    new DataColumn("Phone"),
                                    new DataColumn("Executive Contact 1"),
                                    new DataColumn("Executive Contact 2"),
                                    new DataColumn("Is Women Owned"),
                                    new DataColumn("Minority Owned Description"),
                                });

            var records = await _businessRepo.GetAllBusiness();

            foreach (var record in records)
            {
                dt.Rows.Add(record.dunsNumId, record.dunsName, record.county, record.streetAddress, record.city, record.state, record.zipCode, record.phoneNumber, record.executiveContact, record.executiveContact2,record.isWomanOwned,record.minorityOwnedDesc);
            }
            string excelName = $"DiversityBusiness-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
        }

        public async Task<IActionResult> ProcessBusinessRecords()
        {

            var businessList = await _businessRepo.GetAllBusiness();
            //Give List Business update diverse information
            await _searchBusinessDiverse.CheckBusinessIsDiverse(businessList);

            //retrieve updated record Information
            var updatedList = await _businessRepo.GetAllBusiness();
            return View("Index", updatedList);
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
