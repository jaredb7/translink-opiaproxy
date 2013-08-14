﻿using System;
using OPIA.API.Client.Constants;
using OPIA.API.Client.OPIAEntities.Response.Common;

namespace OPIA.API.Client.OPIAEntities.Response.Plan
{
    public class Leg
    {
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int DistanceM { get; set; }
        public int DurationMins { get; set; }
        public string FromStopId { get; set; }
        public string Instruction { get; set; }
        public string Polyline { get; set; }
        public Route Route { get; set; }
        public bool SameVehicleContinuation { get; set; }
        public string ToStopId { get; set; }
        public VehicleType TravelMode { get; set; }
        public string TripDetails { get; set; }
    }
}