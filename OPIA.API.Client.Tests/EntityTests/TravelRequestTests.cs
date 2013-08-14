using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Client.Constants;
using OPIA.API.Client.OPIAEntities.Request.Travel;

namespace OPIA.API.Client.Tests.EntityTests
{
    [TestClass]
    public class TravelRequestTests
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
        public void PlanRequest_ToString_WithEmptyFromLocationId_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
                                {
                                    FromLocationId = string.Empty,
                                    ToLocationId = "SI:000026",
                                    DateAndTime = DateTime.Now.AddDays(1),
                                    FareTypes = new List<FareType>() {FareType.Standard, FareType.Prepaid, FareType.Free},
                                    MaximumWalkingDistanceM = 120,
                                    ServiceTypes = new List<ServiceType>() {ServiceType.Regular, ServiceType.Express},
                                    TimeModeType = TimeModeType.ArriveBefore,
                                    VehicleTypes = new List<VehicleType>() {VehicleType.Bus, VehicleType.Train},
                                    WalkingSpeedType = WalkingSpeedType.Normal,
                                };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanRequest_ToString_WithEmptyToLocationId_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
                                {
                                    ToLocationId = string.Empty,
                                    FromLocationId = "SI:000026",
                                    VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                                    DateAndTime = DateTime.Now.AddDays(1),
                                    FareTypes = new List<FareType>() {FareType.Standard, FareType.Prepaid, FareType.Free},
                                    MaximumWalkingDistanceM = 120,
                                    ServiceTypes = new List<ServiceType>() {ServiceType.Regular, ServiceType.Express},
                                    TimeModeType = TimeModeType.ArriveBefore,
                                    WalkingSpeedType = WalkingSpeedType.Normal,
                                };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanRequest_ToString_WithNoVehicleTypes_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
                                {
                                    FromLocationId = "SI:000026",
                                    ToLocationId = "AD:80 Mary St, City",
                                    VehicleTypes = new List<VehicleType>(),
                                    DateAndTime = DateTime.Now.AddDays(1),
                                    FareTypes = new List<FareType>() {FareType.Standard, FareType.Prepaid, FareType.Free},
                                    MaximumWalkingDistanceM = 120,
                                    ServiceTypes = new List<ServiceType>() {ServiceType.Regular, ServiceType.Express},
                                    TimeModeType = TimeModeType.ArriveBefore,
                                    WalkingSpeedType = WalkingSpeedType.Normal,
                                };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanRequest_ToString_WithDateAndTimeNotSet_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
                                {
                                    FromLocationId = "SI:000026",
                                    ToLocationId = "AD:80 Mary St, City",
                                    VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                                    FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                                    MaximumWalkingDistanceM = 120,
                                    ServiceTypes = new List<ServiceType>() {ServiceType.Regular, ServiceType.Express},
                                    TimeModeType = TimeModeType.ArriveBefore,
                                    WalkingSpeedType = WalkingSpeedType.Normal,
                                };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanRequest_ToString_WithMaximumWalkingDistanceUnder100Metres_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 99,
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanRequest_ToString_WithNoServiceTypes_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                ServiceTypes = new List<ServiceType>(),
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanRequest_ToString_WithNoFareTypes_MustThrowArgumentException()
        {
            var requestEntity = new PlanRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>(),
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void PlanRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
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
            string expected = string.Format("plan/SI%3A000026/AD%3A80%20Mary%20St%2C%20City?timeMode=1&at={0}&vehicleTypes=2,8&walkSpeed=1&maximumWalkingDistanceM=500&serviceTypes=1,2&fareTypes=2,4,1", Uri.EscapeDataString(DateTime.Now.AddDays(1).ToString("s")));
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithEmptyFromLocationId_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = string.Empty,
                ToLocationId = "SI:000026",
                DateAndTime = DateTime.Now.AddDays(1),
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                MaximumWalkingDistanceM = 120,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                TimeModeType = TimeModeType.ArriveBefore,
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithEmptyToLocationId_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                ToLocationId = string.Empty,
                FromLocationId = "SI:000026",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                MaximumWalkingDistanceM = 120,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithNoVehicleTypes_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>(),
                DateAndTime = DateTime.Now.AddDays(1),
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                MaximumWalkingDistanceM = 120,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithDateAndTimeNotSet_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                MaximumWalkingDistanceM = 120,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithMaximumWalkingDistanceUnder100Metres_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 99,
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithNoServiceTypes_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                FareTypes = new List<FareType>() { FareType.Standard, FareType.Prepaid, FareType.Free },
                ServiceTypes = new List<ServiceType>(),
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlanUrlRequest_ToString_WithNoFareTypes_MustThrowArgumentException()
        {
            var requestEntity = new PlanUrlRequest()
            {
                FromLocationId = "SI:000026",
                ToLocationId = "AD:80 Mary St, City",
                VehicleTypes = new List<VehicleType>() { VehicleType.Bus, VehicleType.Train },
                DateAndTime = DateTime.Now.AddDays(1),
                MaximumWalkingDistanceM = 500,
                ServiceTypes = new List<ServiceType>() { ServiceType.Regular, ServiceType.Express },
                FareTypes = new List<FareType>(),
                TimeModeType = TimeModeType.ArriveBefore,
                WalkingSpeedType = WalkingSpeedType.Normal,
            };
            string expected = requestEntity.ToString();
        }

        [TestMethod]
        public void PlanUrlRequest_ToString_WithMandatoryValuesSet_MustReturnCorrectQueryString()
        {
            var requestEntity = new PlanUrlRequest()
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
            string expected = string.Format("plan-url/SI%3A000026/AD%3A80%20Mary%20St%2C%20City?timeMode=1&at={0}&vehicleTypes=2,8&walkSpeed=1&maximumWalkingDistanceM=500&serviceTypes=1,2&fareTypes=2,4,1", Uri.EscapeDataString(DateTime.Now.AddDays(1).ToString("s")));
            string actual = requestEntity.ToString();
            Assert.AreEqual(expected, actual);
        }

 

    }
}