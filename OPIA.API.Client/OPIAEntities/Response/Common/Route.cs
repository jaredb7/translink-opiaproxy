using OPIA.API.Client.Constants;

namespace OPIA.API.Client.OPIAEntities.Response.Common
{
    public class Route
    {
        public string Code { get; set; }
        public DirectionType Direction { get; set; }
        public bool IsExpress { get; set; }
        public bool IsFree { get; set; }
        public bool IsPrepaid { get; set; }
        public bool IsTransLinkService { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Bitmask of route service types, eg NightLink | Prepaid. Regular = 1, Express = 2, NightLink = 4, School = 8, Industrial = 16
        /// </summary>
        public int ServiceType { get; set; }

        public VehicleType Vehicle { get; set; }
    }
}