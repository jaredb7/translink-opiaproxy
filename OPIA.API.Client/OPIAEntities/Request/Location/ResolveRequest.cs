using System;
using System.Text;
using OPIA.API.Client.Constants;

namespace OPIA.API.Client.OPIAEntities.Request.Location
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class ResolveRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "resolve"; }
        }

        public ResolveRequest()
        {
        }

        /// <summary>
        /// Mandatory: Free-form text to resolve (e.g. King George Sq., Brisbane)
        /// </summary>
        public string LookupText { get; set; }

        /// <summary>
        /// Optional: Location types to filter on: None = 0, Landmark = 1, StreetAddress = 2, Stop = 3, GeographicPosition = 4.
        /// You can't use more than one at a time, it returns an error. If not sure, leave it at zero (the default)
        /// </summary>
        public LocationType LocationType { get; set; }

        /// <summary>
        /// Mandatory: Maximum results to return - this will be limited to a max of 50 by the source.
        /// </summary>
        public int MaxResults { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'resolve?input=Calam+Rd%2C+Calamvale&filter=2&maxResults=20')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) { throw new ArgumentException("RpcMethodName cannot be empty"); }
            if (string.IsNullOrWhiteSpace(LookupText)) { throw new ArgumentException("LookupText cannot be empty"); }
            if (MaxResults <= 0) { throw new ArgumentException("MaxResults must be > 0"); } // will let remote API force the hard limit, in case they decide to change it

            var escapedLocationLookup = Uri.EscapeDataString(LookupText);

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");
            sb.AppendFormat("input={0}", escapedLocationLookup);
            sb.AppendFormat("&filter={0}", (int)LocationType);
            sb.AppendFormat("&maxResults={0}", MaxResults);
            return sb.ToString();
        }
    }
}