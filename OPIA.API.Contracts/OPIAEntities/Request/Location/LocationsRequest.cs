using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPIA.API.Contracts.OPIAEntities.Request.Location
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class LocationsRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "locations"; }
        }

        public LocationsRequest()
        {
            LocationIds = new List<string>();
        }

        /// <summary>
        /// Mandatory: The Location Ids to retrieve information for (e.g. 'LM:Parks & Reserves:Anzac Square,LM:Parks & Reserves:Botanic Gardens'). 
        /// Max of 20 per call.
        /// Render as CSV. These will be escaped before being passed to the OPIA API
        /// </summary>
        public List<string> LocationIds { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// A LocationId looks like 'LM:Parks & Reserves:Anzac Square'
        /// </summary>
        /// <returns>String (looks like 'locations?ids=LM%3AParks+%26+Reserves%3AAnzac+Square%2CLM%3AParks+%26+Reserves%3ABotanic+Gardens')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (!(LocationIds.Any())) { throw new ArgumentException("LocationIds needs to contain at least one Location Id (e.g. 'LM:Parks & Reserves:Anzac Square,LM:Parks & Reserves:Botanic Gardens')"); }

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");
            string locationIds = String.Join(",", LocationIds.Select(rc => rc.Trim()));
            sb.AppendFormat("ids={0}", Uri.EscapeDataString(locationIds));

            return sb.ToString();
        }
    }
}