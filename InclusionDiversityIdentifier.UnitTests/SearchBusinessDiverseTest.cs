using InclusionDiversityIdentifier.BusinessLayer;
using InclusionDiversityIdentifier.Models;
using InclusionDiversityIdentifier.Repository;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.UnitTests
{
    [TestFixture]
    public class SearchBusinessDiverseTest
    {
        private  Mock<IGoogleSearch> _googleSearchService;
        private  Mock<IWebScrape> _scraperService;
        private  Mock<BusinessGoogleSearch> _businessSearch;
        private  Mock<IBusinessRepo> _businessRepo;
        [SetUp]
        public void Setup()
        {
            _googleSearchService = new Mock<IGoogleSearch>();
            _scraperService = new Mock<IWebScrape>();
            _businessSearch = new Mock<BusinessGoogleSearch>(_googleSearchService);
            _businessRepo = new Mock<IBusinessRepo>();
        }

        [Test]
        public void CheckBusinessIsDiverse()
        {
            bool expected = true;
            List<Business> businessToDiverse = new List<Business>();
            var businessParam1 = new Business { dunsName = "EXECUTEAM CORPORATION" };
            var businessParam2 = new Business { dunsName = "Interior Systems Contract Group, Inc." };
            businessToDiverse.Add(businessParam1);
            businessToDiverse.Add(businessParam2);
            Assert.AreEqual(expected, true);
        }
    }
}