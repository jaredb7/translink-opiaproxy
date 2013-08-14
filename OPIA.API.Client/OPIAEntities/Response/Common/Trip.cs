using System;

namespace OPIA.API.Client.OPIAEntities.Response.Common
{
    public class Trip
    {
        public DateTime[] ArrivalTimes { get; set; }
        public bool ContainsStartEndStopsOnly { get; set; }
        public DateTime[] DepartureTimes { get; set; }
        public string Id { get; set; }
        public Route Route { get; set; }
        public string[] StopIds { get; set; }
        public object VehicleDisplay { get; set; }
    }
}