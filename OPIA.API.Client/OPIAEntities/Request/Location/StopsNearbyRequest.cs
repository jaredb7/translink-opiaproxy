using System;
using System.Text;

namespace OPIA.API.Client.OPIAEntities.Request.Location
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class StopsNearbyRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "stops-nearby"; }
        }

        public StopsNearbyRequest()
        {
            UseWalkingDistance = false;
        }

        /// <summary>
        /// Mandatory: LocationId can be any Location.Id returned by another API function
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// Mandatory: Maximum radius in metres to search for stops. Recommended 1000, maximum 3000
        /// </summary>
        public int RadiusInMetres { get; set; }

        /// <summary>
        /// Whether to use actual walking distance (true) or as the crow flies (false).
        /// Defaults to false.
        /// </summary>
        public bool UseWalkingDistance { get; set; }

        /// <summary>
        /// Mandatory: Maximum results to return - this will be limited to a max of 50 by the source.
        /// </summary>
        public int MaxResults { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'stops-nearby/AD%3AAnzac%20Rd%2C%20Eudlo?radiusM=2000&useWalkingDistance=true&maxResults=10')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) { throw new ArgumentException("RpcMethodName cannot be empty"); }
            if (string.IsNullOrWhiteSpace(LocationId)) { throw new ArgumentException("LocationId cannot be empty"); }
            if (RadiusInMetres <= 0) { throw new ArgumentException("RadiusInMetres must be > 0, recommended 1000"); } // will let remote API force any hard limit (apparently 3000m at this point)
            if (MaxResults <= 0) { throw new ArgumentException("MaxResults must be > 0"); } // will let remote API force the hard limit, in case they decide to change it

            var escapedLocationLookup = Uri.EscapeDataString(LocationId);

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("/"); // this one also uses a '/' instead of the usual '?'
            sb.AppendFormat("{0}", escapedLocationLookup);
            sb.AppendFormat("?radiusM={0}", RadiusInMetres); // '?' goes AFTER the actual LocationId. Interesting routing schema...
            sb.AppendFormat("&useWalkingDistance={0}", UseWalkingDistance ? "true" : "false");
            sb.AppendFormat("&maxResults={0}", MaxResults);
            return sb.ToString();
        }
    }
}