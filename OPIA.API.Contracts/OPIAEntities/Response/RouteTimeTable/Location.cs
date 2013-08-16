using OPIA.API.Contracts.OPIAEntities.Response.Common;

namespace OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable
{
    public class Location
    {
        public string __type { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string LandmarkType { get; set; }
        public Position Position { get; set; }
        public int Type { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetType { get; set; }
        public string Suburb { get; set; }
    }
}