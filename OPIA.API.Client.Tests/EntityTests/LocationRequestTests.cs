using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request.Location;

namespace OPIA.API.Client.Tests.EntityTests
{
    [TestClass]
    public class LocationRequestTests
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
        [ExpectedException(typeof(ArgumentException))]
        public void ResolveRequest_ToString_WithEmptyLookupText_MustThrowArgumentException()
        {
            var requestEntity = new ResolveRequest()
            {
                MaxResults = 10,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResolveRequest_ToString_WithZeroMaxResults_MustThrowArgumentException()
        {
            var requestEntity = new ResolveRequest()
            {
                MaxResults = 0,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void ResolveRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new ResolveRequest
            {
                LookupText = "King George Sq., Brisbane",
                LocationType = LocationType.None, // get us everything: stops, landmarks, etc.
                MaxResults = 10,
            };

            string expected = string.Format("resolve?input=King%20George%20Sq.%2C%20Brisbane&filter=0&maxResults=10");
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopsAtLandmarkRequest_ToString_WithEmptyLookupId_MustThrowArgumentException()
        {
            var requestEntity = new StopsAtLandmarkRequest();
            string expected = requestEntity.ToString();
        }


        [TestMethod]
        public void StopsAtLandmarkRequest_ToString_WithNonEmptyLookupId_MustReturnCorrectQueryString()
        {
            var requestEntity = new StopsAtLandmarkRequest {LocationId = "some test landmark"};
            const string expected = "stops-at-landmark/some%20test%20landmark"; // note the use of a '/' instead of the usual '?'
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopsNearbyRequest_ToString_WithEmptyLookupText_MustThrowArgumentException()
        {
            var requestEntity = new StopsNearbyRequest()
            {
                MaxResults = 10,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopsNearbyRequest_ToString_WithZeroMaxResults_MustThrowArgumentException()
        {
            var requestEntity = new StopsNearbyRequest()
            {
                MaxResults = 0,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopsNearbyRequest_ToString_WithZeroRadiusInMetres_MustThrowArgumentException()
        {
            var requestEntity = new StopsNearbyRequest()
            {
                MaxResults = 10,
                RadiusInMetres = 0
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void StopsNearbyRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new StopsNearbyRequest
            {
                LocationId = "AD:Anzac Rd, Eudlo",
                UseWalkingDistance = true,
                RadiusInMetres = 2000,
                MaxResults = 10,
            };

            string expected = string.Format("stops-nearby/AD%3AAnzac%20Rd%2C%20Eudlo?radiusM=2000&useWalkingDistance=true&maxResults=10");
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopsRequest_WithNoStopIds_MustThrowArgumentException()
        {
            var requestEntity = new StopsRequest();
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void StopsRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new StopsRequest
            {
                StopIds = new List<string>() { "000026", "005468" }
            };

            string expected = string.Format("stops?ids=000026,005468");
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LocationsRequest_WithNoLocationIds_MustThrowArgumentException()
        {
            var requestEntity = new LocationsRequest();
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void LocationsRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new LocationsRequest()
            {
                LocationIds = new List<string>() { "LM:Parks & Reserves:Anzac Square", "LM:Parks & Reserves:Botanic Gardens" }
            };

            string expected = string.Format("locations?ids=LM%3AParks%20%26%20Reserves%3AAnzac%20Square%2CLM%3AParks%20%26%20Reserves%3ABotanic%20Gardens");
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

    }
}