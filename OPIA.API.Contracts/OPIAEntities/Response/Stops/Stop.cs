using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Response.Common;

namespace OPIA.API.Contracts.OPIAEntities.Response.Stops
{
    public class Stop
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string LandmarkType { get; set; }
        public Position Position { get; set; }
        public LocationType Type { get; set; }
        public bool HasParentLocation { get; set; }
        public string Intersection1 { get; set; }
        public string Intersection2 { get; set; }
        public string LocationDescription { get; set; }
        public object ParentLocation { get; set; }
        public Route[] Routes { get; set; }

        /// <summary>
        /// Bitmask of route service types, eg NightLink | Prepaid. Regular = 1, Express = 2, NightLink = 4, School = 8, Industrial = 16
        /// </summary>
        public int ServiceType { get; set; }

        public string StopId { get; set; }
        public string StopNoteCodes { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string Zone { get; set; }
    }
}