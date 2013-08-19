namespace OPIA.API.Contracts.OPIAEntities.Response.StopsNearby
{
    public class StopsNearbyResponse : IResponse
    {
        public Nearbystop[] NearbyStops { get; set; }
    }
}