using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Client.Constants;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Client.OPIAEntities.Request.Network;
using OPIA.API.Client.OPIAEntities.Response.Routes;
using OPIA.API.Client.OPIAEntities.Response.TripMapPath;

namespace OPIA.API.Client.Tests.ApiClientTests
{
    [TestClass]
    public class OpiaNetworkClientTests
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
        public void GetRouteTimeTables_MustGetRouteTimeTables()
        {
            var requestEntity = new RouteTimeTablesRequest
            {
                Date = DateTime.Today.AddDays(1), // remember some express pre-paid routes don't run on weekends, P137 I am looking at you :)
                FilterToStartEndStops = false,
                VehicleType = VehicleType.Bus
            };
            requestEntity.RouteCodes.Add("130"); 

            var networkRequestClient = new OpiaNetworkClient();
            var result = networkRequestClient.GetRouteTimeTables(requestEntity);
            Assert.IsTrue(result.RouteTimeTables.Any());
        }

        [TestMethod]
        public async Task GetRouteTimeTablesAsync_MustGetRouteTimeTables()
        {
            var requestEntity = new RouteTimeTablesRequest
            {
                Date = DateTime.Today.AddDays(1), // remember some express pre-paid routes don't run on weekends, P137 I am looking at you :)
                FilterToStartEndStops = false,
                VehicleType = VehicleType.Bus
            };
            requestEntity.RouteCodes.Add("130"); 

            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetRouteTimeTablesAsync(requestEntity);
            Assert.IsTrue(result.RouteTimeTables.Any());
        }


        [TestMethod]
        public void GetStopTimeTables_MustGetStopTimeTables()
        {
            var requestEntity = new StopTimeTablesRequest
            {
                StopIds = new List<string>() { "148", "5840" },
                Date = DateTime.Now.AddDays(1),
                RouteCodesFilter = new List<string>() { "131", "P129" }
            };

            var networkRequestClient = new OpiaNetworkClient();
            var result = networkRequestClient.GetStopTimeTables(requestEntity);
            Assert.IsTrue(result.StopTimetables.Any());
        }

        [TestMethod]
        public async Task GetStopTimeTablesAsync_MustGetStopTimeTablesAsync()
        {
            var requestEntity = new StopTimeTablesRequest
            {
                StopIds = new List<string>() { "148", "5840" },
                Date = DateTime.Now.AddDays(1),
                RouteCodesFilter = new List<string>() { "131", "P129" }
            };

            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetStopTimeTablesAsync(requestEntity);
            Assert.IsTrue(result.StopTimetables.Any());
        }

        [TestMethod]
        public void GetTrips_MustGetTrips()
        {
            var requestEntity = new TripsRequest
            {
                // TODO these trip ids may not be valid for long 
                // TODO populate properly after a call to GetStopTimeTables or GetRouteTimeTables
                TripIds = new List<string>() { "14278_3386311_20130808", "14278_3386354_20130808" },
            };

            var networkRequestClient = new OpiaNetworkClient();
            var result = networkRequestClient.GetTrips(requestEntity);
            Assert.IsTrue(result.Trips.Any());
        }

        [TestMethod]
        public async Task GetTripsAsync_MustGetTripsAsync()
        {
            var requestEntity = new TripsRequest
            {
                // TODO these trip ids may not be valid for long 
                // TODO populate properly after a call to GetStopTimeTables or GetRouteTimeTables
                TripIds = new List<string>() { "14278_3386311_20130808", "14278_3386354_20130808" },
            };

            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetTripsAsync(requestEntity);
            Assert.IsTrue(result.Trips.Any());
        }

        [TestMethod]
        public void GetTripMapPath_MustGetTripMapPath()
        {
            var requestEntity = new TripMapPathRequest
            {
                // TODO these trip ids may not be valid for long 
                // TODO populate properly after a call to GetStopTimeTables or GetRouteTimeTables
                TripId = "14278_3386311_20130808"
            };

            var networkRequestClient = new OpiaNetworkClient();
            TripMapPathResponse result = networkRequestClient.GetTripMapPath(requestEntity);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(result.Path));
        }

        [TestMethod]
        public async Task GetTripMapPathAsync_MustGetTripMapPathAsync()
        {
            var requestEntity = new TripMapPathRequest
            {
                // TODO these trip ids may not be valid for long 
                // TODO populate properly after a call to GetStopTimeTables or GetRouteTimeTables
                TripId = "14278_3386311_20130808"
            };

            var networkRequestClient = new OpiaNetworkClient();
            TripMapPathResponse result = await networkRequestClient.GetTripMapPathAsync(requestEntity);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(result.Path));
        }

        [TestMethod]
        public void GetRouteMapPath_MustGetRouteMapPath()
        {
            var requestEntity = new RouteMapPathRequest
            {
                RouteCode = "333",
                VehicleType = VehicleType.Bus,
                Date = DateTime.Now.AddDays(1),
            };

            var networkRequestClient = new OpiaNetworkClient();
            var result = networkRequestClient.GetRouteMapPath(requestEntity);
            Assert.IsTrue(result.Paths.Any() && !String.IsNullOrWhiteSpace(result.Paths.First().MapPoints));
        }

        [TestMethod]
        public async Task GetRouteMapPathAsync_MustGetRouteMapPathAsync()
        {
            var requestEntity = new RouteMapPathRequest
            {
                RouteCode = "333",
                VehicleType = VehicleType.Bus,
                Date = DateTime.Now.AddDays(1),
            };

            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetRouteMapPathAsync(requestEntity);
            Assert.IsTrue(result.Paths.Any() && !String.IsNullOrWhiteSpace(result.Paths.First().MapPoints));
        }

        [TestMethod]
        public void GetRoutes_MustGetRoutes()
        {
            var requestEntity = new RoutesRequest
            {
                Date = DateTime.Now.AddDays(1),
                RouteCodes = new List<string>() { "333", "130" },
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train }
            };

            var networkRequestClient = new OpiaNetworkClient();
            RoutesResponse result = networkRequestClient.GetRoutes(requestEntity);
            Assert.IsTrue(result.Routes.Any());
        }

        [TestMethod]
        public async Task GetRoutesAsync_MustGetRoutesAsync()
        {
            var requestEntity = new RoutesRequest
            {
                Date = DateTime.Now.AddDays(1),
                RouteCodes = new List<string>() { "333", "130" },
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train }
            };

            var networkRequestClient = new OpiaNetworkClient();
            RoutesResponse result = await networkRequestClient.GetRoutesAsync(requestEntity);
            Assert.IsTrue(result.Routes.Any());
        }

    }

    


}