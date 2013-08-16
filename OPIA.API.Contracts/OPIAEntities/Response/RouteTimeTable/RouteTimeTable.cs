using System;
using OPIA.API.Contracts.OPIAEntities.Response.Common;

namespace OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable
{
    public class RouteTimeTable
    {
        public DateTime Date { get; set; }
        public Route Route { get; set; }
        public Trip[] Trips { get; set; }
    }
}