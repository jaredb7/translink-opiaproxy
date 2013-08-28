using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request.Network;
using OPIA.API.Contracts.OPIAEntities.Response.RouteMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Routes;
using OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.StopTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.TripMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Trips;

namespace OPIA.API.Proxy.Tests.ProxyTests
{
    [TestClass]
    public class ProxyNetworkControllerMethodsTests
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
            _client = new HttpClient {BaseAddress = new Uri(baseUrl)};
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
            // any SSL, auth or setting of tokens or cookies whatever will probably need to go in here. 
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

            var response = _client.PostAsJsonAsync("network/getroutetimetables", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<RouteTimeTablesResponse>().Result;
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

            var response = _client.PostAsJsonAsync("network/getstoptimetables", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<StopTimeTablesResponse>().Result;
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

            var response = _client.PostAsJsonAsync("network/gettrips", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<TripsResponse>().Result;
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

            var response = _client.PostAsJsonAsync("network/gettripmappath", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<TripMapPathResponse>().Result;

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

            var response = _client.PostAsJsonAsync("network/getroutemappath", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<RouteMapPathResponse>().Result;
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

            var response = _client.PostAsJsonAsync("network/getroutes", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<RoutesResponse>().Result;
            Assert.IsTrue(result.Routes.Any());
        }


    }

    


}