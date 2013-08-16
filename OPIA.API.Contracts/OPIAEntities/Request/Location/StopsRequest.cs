using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPIA.API.Contracts.OPIAEntities.Request.Location
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class StopsRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "stops"; }
        }

        public StopsRequest()
        {
            StopIds = new List<string>();
        }

        /// <summary>
        /// Mandatory: The Stop Ids to retrieve information for for (e.g. '000026') . Max of 100 accepted.
        /// Render as CSV
        /// </summary>
        public List<string> StopIds { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'stops?ids=000026')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (!(StopIds.Any())) { throw new ArgumentException("StopIds needs to contain at least one zero-padded 6-digit Stop Id (e.g. 000026)"); }

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");
            string stopIds = String.Join(",", StopIds.Select(rc => rc.Trim()));
            sb.AppendFormat("ids={0}", stopIds);

            return sb.ToString();
        }
    }
}