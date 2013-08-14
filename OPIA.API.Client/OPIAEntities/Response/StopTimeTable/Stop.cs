using OPIA.API.Client.OPIAEntities.Response.Common;

namespace OPIA.API.Client.OPIAEntities.Response.StopTimeTable
{
    public class Stop
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string LandmarkType { get; set; }
        public Position Position { get; set; }
        public int Type { get; set; }
        public bool HasParentLocation { get; set; }
        public string Intersection1 { get; set; }
        public string Intersection2 { get; set; }
        public string LocationDescription { get; set; }
        public object ParentLocation { get; set; }
        public object[] Routes { get; set; }
        public int ServiceType { get; set; }
        public string StopId { get; set; }
        public object StopNoteCodes { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string Zone { get; set; }
    }

}