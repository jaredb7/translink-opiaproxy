using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Contracts.OPIAEntities.Request.Location;
using OPIA.API.Contracts.OPIAEntities.Response.Locations;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using OPIA.API.Contracts.OPIAEntities.Response.Stops;
using OPIA.API.Contracts.OPIAEntities.Response.StopsNearby;

namespace OPIA.API.Proxy.Tests.ProxyTests
{
    [TestClass]
    public class ProxyLocationControllerMethodsTests
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
        public void Resolve_MustResolveFreeFormLocationText()
        {
            var requestEntity = new ResolveRequest
            {
                LookupText = "Anzac Sq., Brisbane",
                LocationType = 0,
                MaxResults = 10
            };

            var response = _client.PostAsJsonAsync("location/resolve", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<ResolveResponse>().Result;


            Assert.IsTrue(result.Locations.Any());
        }


        [TestMethod]
        public void GetStopsAtLandmark_MustGetStopsAtLandmark()
        {
            // note that this is not the right landmark type, but it does serve to exercise the query (we just get nothing back)
            var requestEntity = new StopsAtLandmarkRequest()
                                                         {
                                                             LocationId = "LM:Hotels & Motels:The Sebel King George Square",
                                                         };

            var response = _client.PostAsJsonAsync("location/getstopsatlandmark", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<ResolveResponse>().Result;
            Assert.Inconclusive("Need to find a landmark of type LocationId.LandmarkType.Landmark to use as a parameter");
        }


        [TestMethod]
        public void GetStopsNearby_LocationId_MustGetStopsNearbyLocationId()
        {
            var requestEntity = new StopsNearbyRequest
            {
                LocationId = "AD:Anzac Rd, Eudlo",
                UseWalkingDistance = true,
                RadiusInMetres = 2000,
                MaxResults = 10,
            };

            var response = _client.PostAsJsonAsync("location/getstopsnearby", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<StopsNearbyResponse>().Result;
            Assert.IsTrue(result.NearbyStops.Any());
        }


        [TestMethod]
        public void GetStopsByIds_MustGetStopsByIds()
        {
            var requestEntity = new StopsRequest
            {
                StopIds = new List<string>() { "000026", "005468" }
            };

            var response = _client.PostAsJsonAsync("location/getstopsbyids", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<StopsResponse>().Result;
            Assert.IsTrue(result.Stops.Any());
        }


        [TestMethod]
        public void GetLocationsByIds_MustGetLocationsByIds()
        {
            var requestEntity = new LocationsRequest()
            {
                LocationIds = new List<string>() { "LM:Parks & Reserves:Anzac Square", "LM:Parks & Reserves:Botanic Gardens" }
            };

            var response = _client.PostAsJsonAsync("location/getlocationsbyids", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<LocationsResponse>().Result;
            Assert.IsTrue(result.Locations.Any());
        }


    }
}