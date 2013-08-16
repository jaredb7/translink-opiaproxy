using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Response.Common;

namespace OPIA.API.Contracts.OPIAEntities.Response.Locations
{
    public class Location
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string LandmarkType { get; set; }
        public Position Position { get; set; }
        public LocationType Type { get; set; }
    }
}