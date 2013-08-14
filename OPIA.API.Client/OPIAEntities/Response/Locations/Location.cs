using OPIA.API.Client.Constants;
using OPIA.API.Client.OPIAEntities.Response.Common;

namespace OPIA.API.Client.OPIAEntities.Response.Locations
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