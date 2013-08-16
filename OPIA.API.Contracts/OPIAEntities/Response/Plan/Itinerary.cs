using System;

namespace OPIA.API.Contracts.OPIAEntities.Response.Plan
{
    public class Itinerary
    {
        public int DurationMins { get; set; }
        public DateTime EndTime { get; set; }
        public Fare Fare { get; set; }
        public DateTime FirstDepartureTime { get; set; }
        public DateTime LastArrivalTime { get; set; }
        public Leg[] Legs { get; set; }
        public DateTime StartTime { get; set; }
        public int Transfers { get; set; }
    }
}