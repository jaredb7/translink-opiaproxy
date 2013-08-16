using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request.Network;

namespace OPIA.API.Client.Tests.EntityTests
{
    [TestClass]
    public class NetworkRequestTests
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
        public void RouteTimeTablesRequest_ToString_WithEmptyRoutes_MustThrowArgumentException()
        {
            var requestEntity = new RouteTimeTablesRequest
            {
                FilterToStartEndStops = true,
                Date = DateTime.Now.AddDays(1),
                VehicleType = VehicleType.Bus,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RouteTimeTablesRequest_ToString_WithVehicleTypeNotSet_MustThrowArgumentException()
        {
            var requestEntity = new RouteTimeTablesRequest
            {
                RouteCodes = new List<string>(){"P137"},
                FilterToStartEndStops = true,
                Date = DateTime.Now.AddDays(1),
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RouteTimeTablesRequest_ToString_WithDateNotSet_MustThrowArgumentException()
        {
            var requestEntity = new RouteTimeTablesRequest
            {
                RouteCodes = new List<string>() { "P137" },
                FilterToStartEndStops = true,
                VehicleType = VehicleType.Bus,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void RouteTimeTablesRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new RouteTimeTablesRequest
                        {
                            RouteCodes = new List<string>(){"P137", "130"},
                            FilterToStartEndStops = true,
                            VehicleType = VehicleType.Bus,
                            Date = DateTime.Now.AddDays(1),
                        };

            // route-timetables?routeCodes=P137,130&date=2013-08-07&vehicleType=2&filterToStartEndStops=true
            string expected = string.Format("route-timetables?routeCodes=P137,130&date={0}&vehicleType=2&filterToStartEndStops=true", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopTimeTablesRequest_ToString_WithEmptyStopIds_MustThrowArgumentException()
        {
            var requestEntity = new StopTimeTablesRequest
            {
                Date = DateTime.Now.AddDays(1),
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StopTimeTablesRequest_ToString_WithDateNotSet_MustThrowArgumentException()
        {
            var requestEntity = new StopTimeTablesRequest
            {
                StopIds = new List<string>() { "148", "5840" },
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void StopTimeTablesRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new StopTimeTablesRequest
                        {
                            StopIds = new List<string>() {"148", "5840"},
                            Date = DateTime.Now.AddDays(1),
                            RouteCodesFilter = new List<string>() {"131", "P129"}
                        };
            string expected = string.Format("stop-timetables?stopIds=148,5840&date={0}&routeFilter=131,P129", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TripsRequest_ToString_WithEmptyTripIds_MustThrowArgumentException()
        {
            var requestEntity = new TripsRequest();
            string expected = requestEntity.ToString();
        }


        [TestMethod]
        public void TripsRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new TripsRequest
            {
                TripIds = new List<string>() { "14278_3386311_20130808", "14278_3386354_20130808" },
            };
            string expected = string.Format("trips?ids=14278_3386311_20130808,14278_3386354_20130808");
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TripMapPathRequest_ToString_WithEmptyTripIds_MustThrowArgumentException()
        {
            var requestEntity = new TripMapPathRequest();
            string expected = requestEntity.ToString();
        }


        [TestMethod]
        public void TripMapPathRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new TripMapPathRequest
            {
                TripId = "14278_3386311_20130808",
            };
            string expected = string.Format("trip-map-path?tripId=14278_3386311_20130808");
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RouteRequest_ToString_WithDateNotSet_MustThrowArgumentException()
        {
            var requestEntity = new RoutesRequest
            {
                RouteCodes = new List<string>() { "333", "130" },
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train }
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void RouteRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new RoutesRequest
            {
                Date = DateTime.Now.AddDays(1),
                RouteCodes = new List<string>() { "333", "130" },
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train }
            };

            string expected = string.Format("routes?date={0}&vehicleTypes=2,8&routeCodes=333,130", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

    }
}