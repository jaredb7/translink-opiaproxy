using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request.Travel;
using OPIA.API.Contracts.OPIAEntities.Response.Plan;
using OPIA.API.Contracts.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Proxy.Tests.ProxyTests
{
    [TestClass]
    public class ProxyTravelControllerMethodsTests
    {
        private static TestContext _testContext;
        private HttpClient _client;


        [ClassInitialize]
        public static void ClassInitialiser(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestInitialize]
        public void TestInitialiser()
        {
            SetupNewHttpClient();
        }

        private void SetupNewHttpClient()
        {
            string baseUrl = ConfigurationManager.AppSettings["proxyApiBaseUrl"];
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // any SSL, auth or setting of tokens or cookies whatever will probably need to go in here. 
        }

        [TestMethod]
        public void Plan_MustPlanJourney()
        {
            var requestEntity = new PlanRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1), // ensures response will never be cached, as time moves in lock-step with .Now
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };

            var response = _client.PostAsJsonAsync("travel/getplan", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<PlanResponse>().Result;
            Assert.IsTrue(result.TravelOptions.Itineraries.Any());
        }


        [TestMethod]
        public void PlanUrl_MustProduceJourneyPlanUrl()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train, VehicleType.Walk },
                DateAndTime = DateTime.Now.AddDays(1), // ensures response will never be cached, as time moves in lock-step with .Now
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };

            var response = _client.PostAsJsonAsync("travel/getplanurl", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<PlanUrlResponse>().Result;
            Assert.IsFalse(String.IsNullOrWhiteSpace(result.JourneyPlannerUrl));
        }



    }
}