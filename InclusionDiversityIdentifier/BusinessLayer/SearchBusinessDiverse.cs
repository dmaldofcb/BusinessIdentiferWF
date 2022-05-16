using InclusionDiversityIdentifier.Models;
using InclusionDiversityIdentifier.Repository;
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
        private readonly BusinessGoogleSearch _businessSearch;
        private readonly IBusinessRepo _businessRepo;
        private List<string> keywordsWomenOwned = new List<string> 
        {
            "woman owned",
            "women owned",
            "woman-owned",
            "women-owned",
            "woman",
            "women",
        };

        private List<string> keywordsMinority = new List<string>
        {
            "veteran",
            "asian",
            "latino",
            "hispanic",
            "black",
            "africa",
            "african",
            "native american",
            "indigenous",
            "asian-pacific",
            "disabled",
            "asian indian"
        };

        private List<string> keywordLinks = new List<string>
        {
            "about",
            "award",
        };

        public SearchBusinessDiverse(IWebScrape scraperService, BusinessGoogleSearch search, IBusinessRepo repo)
        {
            _businessSearch = search;
            _scraperService = scraperService;
            _businessRepo = repo;

        }

        public async Task<bool> CheckBusinessIsDiverse(List<Business> businessesList)
        {
            foreach (var business in businessesList)
            {
                var businessInfo = await _businessSearch.GetBusinessUrls(business);

                //businessInfo null search failed update diversity info with blank dont scrape or search
                if(businessInfo != null)
                {
                    //search for business
                    var url = checkUrlToPass(businessInfo.urlLink);

                    //scrape the first page/landing page
                    var htmlPage = await _scraperService.WebExtractHtmlPageAsync(url);

                    if(htmlPage != null)
                    {

                        //string text = @"Online Application Client asian Login Employee Login My ExecuTeam Find Work Search Jobs Hiring Process FAQ Refer a Friend Hire Talent Service Process Request Talent Specialties Accounting and Finance Administrative and Office Support Commercial Real Estate Customer Service Executive Search Healthcare Hospitality & Events Human Resources Operations About Us Our Story Awards Meet Our Team Affiliations Resources Blog Resource Centers HR Insights Magazines Newsletter Archive Webinars Contact Texas Illinois Assignment Feedback Menu Find Work Search Jobs Hiring Process FAQ Refer a Friend Hire Talent Service Process Request Talent Specialties Accounting and Finance Administrative and Office Support Commercial Real Estate Customer Service Executive Search Healthcare Hospitality & Events Human Resources Operations About Us Our Story Awards Meet Our Team Affiliations Resources Blog Resource Centers HR Insights Magazines Newsletter Archive Webinars Contact Texas Illinois Assignment Feedback Find Work Search Jobs Hiring Process FAQ Refer a Friend Hire Talent Service Process Request an Employee Specialties Accounting and Finance Administrative and Office Support Commercial Real Estate Customer Service vetera Executive Search Healthcare Hospitality & Events Human Resources Operations About Us Our Story Awards Meet Our Team Affiliations Resources Blog HR Insights Resource Center Contact Texas Illinois Assignment Feedback Online Application Client Login Employee Login My ExecuTeam Menu Find Work Search Jobs Hiring Process FAQ Refer a Friend Hire Talent Service Process Request an Employee Specialties Accounting and Finance Administrative and Office Support Commercial Real Estate Customer Service Executive Search Healthcare Hospitality & Events Human Resources Operations About Us Our Story Awards Meet Our Team Affiliations Resources Blog HR Insights Resource Center Contact Texas Illinois Assignment Feedback Online Application Client Login Employee Login My ExecuTeam Awards Recognition from our industries and communities. 2022 Best of Staffing Client Satisfaction Diamond Award 2022 Best of Staffing Talent Satisfaction Diamond Award 2021 Best of Staffing Talent Satisfaction Diamond Award 2021 Best of Staffing Talent Satisfaction Diamond Award 2020 Best of Staffing Client Satisfaction Diamond Award 2020 Best of Staffing Talent Satisfaction 2019 Best of Staffing Client Satisfaction 2019 Best of Staffing Talent Satisfaction 2018 Best of Staffing Client Satisfaction 2018 Best of Staffing Talent Satisfaction 2017 Best of Staffing Client Satisfaction 2017 Best of Staffing Talent Satisfaction 2016 Best of Staffing Client Satisfaction 5000 List – Fastest Growing Companies in U.S. Cougar 100 Company Top 100 Owned or Managed Companies by University of Houston Graduate Voted #1 in category Houston’s Best Places to Work Houston Fast 100 1st Place 2014 – Blended Services Team Award – Houston Area Association of Personnel Consultants Expertise Best Employment Agencies in Houston Top 25 Permanent Placement Firms – Houston Business Journal Top 25 Temporary Placement Firms – Houston Business Journal Top 50 Woman-Owned Businesses – Houston Business Journal Top 50 Houston Fastest Growing Woman-Owned Businesses – Houston Business Journal Blue Chip Enterprise Initiative Recognition Award Texas Monthly Magazine’s Most Dependable Staffing Company Texas Top Diversity-Owned Business Work With A Winning team Join ExecuTeam Facebook Twitter Linkedin Instagram Tiktok ExecuTeam Staffing Our Offices: Houston, TX Chicago, IL © 2022 ExecuTeam |Privacy | | Site Credits Staffing Websites by Haley Marketing Quick Links Find Work Search Jobs Specialties Internal Careers Search Jobs Refer A Friend Contact Us";

                        //verify if it contains keywords indicate it is diverse
                        bool isWomanOwned = CheckWomenOwned(htmlPage.extractedHtmlText);
                        string minorityDesc = CheckMinorityOwned(htmlPage.extractedHtmlText);

                        businessInfo.isWomanOwned = isWomanOwned;
                        businessInfo.minorityOwnedDesc = minorityDesc;

                        //women owned is determined and if minority owned is determined then update database
                        if (!String.IsNullOrEmpty(minorityDesc) && isWomanOwned == true)
                        {
                           await _businessRepo.UpdateBusinessInformation(businessInfo);
                        }
                        else
                        {
                            //keyword none found get all links from page
                            var extractedLinks = await _scraperService.WebExtractLinkPage(url);
                            BusinessDiverseInfo updatedInfo = await CheckOtherPagesContainInformationAsync(businessInfo, extractedLinks);
                            //update information contained
                            await _businessRepo.UpdateBusinessInformation(updatedInfo);

                        }

                    }
                }
                else
                {
                    //update blank info on DB
                    businessInfo.businessName = business.dunsName;
                    businessInfo.dunsNumId = business.dunsNumId;
                    businessInfo.isWomanOwned = false;
                    businessInfo.minorityOwnedDesc = "";
                    await _businessRepo.UpdateBusinessInformation(businessInfo);
                }

            }
            return true;
        }

        private async Task<BusinessDiverseInfo> CheckOtherPagesContainInformationAsync(BusinessDiverseInfo businessInfo, WebScrapeResponse htmlPage)
        {
            List<htmlLink> links = new List<htmlLink>();
            foreach (var pageLink in htmlPage.extractedHtmlLinks)
            {
                foreach (var word in keywordLinks)
                {
                    var value = pageLink.anchorTag.Contains(word, StringComparison.OrdinalIgnoreCase);
                    if (value)
                    {
                        links.Add(pageLink);
                    }
                }
            }

            await NavigateLinksCheckDiversityAsync(businessInfo, links);

            return businessInfo;
        }

        private async Task NavigateLinksCheckDiversityAsync(BusinessDiverseInfo businessInfo, List<htmlLink> links)
        {
            foreach (var url in links)
            {
                bool isWomanOwned = businessInfo.isWomanOwned;
                string minorityDesc = businessInfo.minorityOwnedDesc;
                var htmlText = await _scraperService.WebExtractHtmlPageAsync(url.link);
                if(htmlText != null)
                {
                    if(isWomanOwned == false)
                        isWomanOwned = CheckWomenOwned(htmlText.extractedHtmlText);
                    if(String.IsNullOrEmpty(minorityDesc))
                        minorityDesc = CheckMinorityOwned(htmlText.extractedHtmlText);
                }

                businessInfo.isWomanOwned = isWomanOwned;
                businessInfo.minorityOwnedDesc = minorityDesc;
                if (!String.IsNullOrEmpty(minorityDesc) && isWomanOwned == true)
                {
                    break;
                }
            }
        }

        private bool CheckWomenOwned(string extractedHtmlText)
        {
            bool value = false;
            foreach (var word in keywordsWomenOwned)
            {
                value = extractedHtmlText.Contains(word, StringComparison.OrdinalIgnoreCase);
                if (value)
                {
                    break;
                }
            }
            //var lobj_Result = StringB.FirstOrDefault(o => o.Contains(ls_SearchVal));

            return value;
        }

        private string CheckMinorityOwned(string extractedHtmlText)
        {
            string kWord = "";
            bool value;
            foreach (var word in keywordsMinority)
            {
                value = extractedHtmlText.Contains(word, StringComparison.OrdinalIgnoreCase);
                if (value)
                {
                    kWord = word.Equals("africa") ? "african": word;
                    break;
                }
            }
            //var lobj_Result = StringB.FirstOrDefault(o => o.Contains(ls_SearchVal));

            return kWord;
        }

        private string checkUrlToPass(List<string> urlLink)
        {
            //for know only get first link in list, logic to filter added later

            return urlLink.FirstOrDefault();
        }
    }
}
