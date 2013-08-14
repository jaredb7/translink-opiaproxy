using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Client.OPIAEntities.Request.Location;
using OPIA.API.Client.OPIAEntities.Response.Locations;

namespace OPIA.API.Client.Tests.ApiClientTests
{
    [TestClass]
    public class OpiaLocationClientTests
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
        public void Resolve_MustResolveFreeFormLocationText()
        {
            var requestEntity = new ResolveRequest
            {
                LookupText = "Anzac Sq., Brisbane",
                LocationType = 0,
                MaxResults = 10
            };

            var locationClient = new OpiaLocationClient();
            var result = locationClient.Resolve(requestEntity);
            Assert.IsTrue(result.Locations.Any());

        }

        [TestMethod]
        public async Task ResolveAsync_MustResolveFreeFormLocationTextAsync()
        {
            var requestEntity = new ResolveRequest
            {
                LookupText = "Anzac Sq., Brisbane",
                LocationType = 0,
                MaxResults = 10
            };

            var locationClient = new OpiaLocationClient();
            var result = await locationClient.ResolveAsync(requestEntity);
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
            var locationClient = new OpiaLocationClient();
            var result = locationClient.GetStopsAtLandmark(requestEntity);

            Assert.Inconclusive("Need to find a landmark of type LocationId.LandmarkType.Landmark");
        }

        [TestMethod]
        public async Task GetStopsAtLandmarkAsync_MustGetStopsAtLandmarkAsync()
        {
            // note that this is not the right landmark type, but it does serve to exercise the query (we just get nothing back)
            var requestEntity = new StopsAtLandmarkRequest()
            {
                LocationId = "LM:Hotels & Motels:The Sebel King George Square",
            };
            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetStopsAtLandmarkAsync(requestEntity);
            Assert.Inconclusive("Need to find a landmark of type LocationId.LandmarkType.Landmark");
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

            var locationClient = new OpiaLocationClient();
            var result = locationClient.GetStopsNearby(requestEntity);
            Assert.IsTrue(result.NearbyStops.Any());
        }

        [TestMethod]
        public async Task GetStopsNearbyAsync_LocationId_MustGetStopsNearbyLocationIdAsync()
        {
            var requestEntity = new StopsNearbyRequest
            {
                LocationId = "AD:Anzac Rd, Eudlo",
                UseWalkingDistance = true,
                RadiusInMetres = 2000,
                MaxResults = 10,
            };

            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetStopsNearbyAsync(requestEntity);
            Assert.IsTrue(result.NearbyStops.Any());
        }

        [TestMethod]
        public void GetStopsByIds_MustGetStopsByIds()
        {
            var requestEntity = new StopsRequest
            {
                StopIds = new List<string>() { "000026", "005468" }
            };

            var locationClient = new OpiaLocationClient();
            var result = locationClient.GetStopsByIds(requestEntity);
            Assert.IsTrue(result.Stops.Any());
        }

        [TestMethod]
        public async Task GetStopsByIdsAsync_MustGetStopsByIdsAsync()
        {
            var requestEntity = new StopsRequest
            {
                StopIds = new List<string>() { "000026", "005468" }
            };

            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetStopsByIdsAsync(requestEntity);
            Assert.IsTrue(result.Stops.Any());
        }

        [TestMethod]
        public void GetLocationsByIds_MustGetLocationsByIds()
        {
            var requestEntity = new LocationsRequest()
            {
                LocationIds = new List<string>() { "LM:Parks & Reserves:Anzac Square", "LM:Parks & Reserves:Botanic Gardens" }
            };

            var locationClient = new OpiaLocationClient();
            LocationsResponse result = locationClient.GetLocationsByIds(requestEntity);
            Assert.IsTrue(result.Locations.Any());
        }

        [TestMethod]
        public async Task GetLocationsByIdsAsync_MustGetLocationsByIdsAsync()
        {
            var requestEntity = new LocationsRequest()
            {
                LocationIds = new List<string>() { "LM:Parks & Reserves:Anzac Square", "LM:Parks & Reserves:Botanic Gardens" }
            };

            var locationClient = new OpiaLocationClient();
            LocationsResponse result = await locationClient.GetLocationsByIdsAsync(requestEntity);
            Assert.IsTrue(result.Locations.Any());
        }

    }
}