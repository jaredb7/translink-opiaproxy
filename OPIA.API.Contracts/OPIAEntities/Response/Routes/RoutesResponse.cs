using OPIA.API.Contracts.OPIAEntities.Response.Common;

namespace OPIA.API.Contracts.OPIAEntities.Response.Routes
{
    public class RoutesResponse : IResponse
    {
        public Route[] Routes { get; set; }
    }

}