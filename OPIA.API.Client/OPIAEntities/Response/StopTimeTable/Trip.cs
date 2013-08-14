using System;
using OPIA.API.Client.OPIAEntities.Response.Common;

namespace OPIA.API.Client.OPIAEntities.Response.StopTimeTable
{
    public class Trip
    {
        public DateTime DepartureTime { get; set; }
        public object NoteCodes { get; set; }
        public Route Route { get; set; }
        public string TripId { get; set; }
    }
}