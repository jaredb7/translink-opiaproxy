using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Client.Constants;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Client.OPIAEntities.Request.Travel;

namespace OPIA.API.Client.Tests.ApiClientTests
{
    [TestClass]
    public class OpiaTravelClientTests
    {
        private static TestContext _testContext;


        [ClassInitialize]
        public static void ClassInitialiser(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestInitialize]
        public void TestInitialiser()
        {
        }

        [TestMethod]
        public void Plan_MustPlanJourney()
        {
            var requestEntity = new PlanRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };

            var travelClient = new OpiaTravelClient();
            var result = travelClient.GetPlan(requestEntity);
            Assert.IsTrue(result.TravelOptions.Itineraries.Any());
        }

        [TestMethod]
        public async Task PlanAsync_MustPlanJourneyAsync()
        {
            var requestEntity = new PlanRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };

            var travelClient = new OpiaTravelClient();
            var result = await travelClient.GetPlanAsync(requestEntity);
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
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };

            var travelClient = new OpiaTravelClient();
            var result = travelClient.GetPlanUrl(requestEntity);
            Assert.IsFalse(String.IsNullOrWhiteSpace(result.JourneyPlannerUrl));
        }

        [TestMethod]
        public async Task PlanUrlAsync_MustProduceJourneyPlanUrlAsync()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train, VehicleType.Walk },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };

            var travelClient = new OpiaTravelClient();
            var result = await travelClient.GetPlanUrlAsync(requestEntity);
            Assert.IsFalse(String.IsNullOrWhiteSpace(result.JourneyPlannerUrl));
        }


    }
}