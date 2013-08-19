using OPIA.API.Contracts.OPIAEntities.Response.Common;

namespace OPIA.API.Contracts.OPIAEntities.Response.Trips
{
    public class TripsResponse : IResponse
    {
        public Trip[] Trips { get; set; }
    }
}