using System;
using System.Text;

namespace OPIA.API.Client.OPIAEntities.Request.Location
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class StopsAtLandmarkRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "stops-at-landmark"; }
        }

        public StopsAtLandmarkRequest()
        {
        }

        /// <summary>
        /// Mandatory: Location Id (will usually need to be obtained by some other method, e.g. a call to 'resolve' first)
        /// Only Locations of 'Location.TypeLocationType.Landmark' are accepted by this function
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'stops-at-landmark/LM:blah')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) { throw new ArgumentException("RpcMethodName cannot be empty"); }
            if (string.IsNullOrWhiteSpace(LocationId)) { throw new ArgumentException("LocationId cannot be empty"); }

            var escapedLocationLookup = Uri.EscapeDataString(LocationId);

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("/"); // a gotcha, it uses a '/' instead of the usual '?'
            sb.AppendFormat("{0}", escapedLocationLookup);
            return sb.ToString();
        }
    }
}