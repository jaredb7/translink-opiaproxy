namespace OPIA.API.Contracts.OPIAEntities.Response.Plan
{
    public class Fare
    {
        public FareDetails[] Fares { get; set; }
        public int MaximumZone { get; set; }
        public int MinimumZone { get; set; }
        public int TotalZones { get; set; }
    }
}