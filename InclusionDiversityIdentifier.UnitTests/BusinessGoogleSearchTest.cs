using InclusionDiversityIdentifier.BusinessLayer;
using InclusionDiversityIdentifier.Models;
using InclusionDiversityIdentifier.Services.ServiceInterfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.UnitTests
{
    [TestFixture]
    public class BusinessGoogleSearchTest
    {
        private  Mock<IGoogleSearch> _googleSearchService;
        [SetUp]
        public void Setup()
        {
            _googleSearchService = new Mock<IGoogleSearch>();
        }

        [Test]
        public void GetBusinessUrls()
        {
            var businessParam = new Business { dunsName = "EXECUTEAM CORPORATION" };
            var controller = new BusinessGoogleSearch(_googleSearchService.Object);
            Task<BusinessDiverseInfo> task = Task.Run(() => controller.GetBusinessUrls(businessParam));
            task.Wait();
            BusinessDiverseInfo result = task.Result;
            Assert.AreEqual("https://www.executeam.com/", result.urlLink[0]);
        }
    }
}