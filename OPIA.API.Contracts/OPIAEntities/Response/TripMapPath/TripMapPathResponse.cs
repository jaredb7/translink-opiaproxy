namespace OPIA.API.Contracts.OPIAEntities.Response.TripMapPath
{
    public class TripMapPathResponse : IResponse
    {
        /// <summary>
        /// This string is a map path encoded in polyline format. It looks something like:
        /// "nusfDkm_e\\u@p@yCvClBlCTZtBvCJN`CbDNTn@~@dAvAnCvDtBvChA`Bz@hAxBgCDENK[...truncated, can run for thousands of characters...]"
        /// <remarks>For more information on understanding and decoding polylines see 
        /// 'https://developers.google.com/maps/documentation/utilities/polylinealgorithm'</remarks>
        /// </summary>
        public string Path { get; set; }
    }

}