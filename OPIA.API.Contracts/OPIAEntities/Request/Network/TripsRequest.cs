using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPIA.API.Contracts.OPIAEntities.Request.Network
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class TripsRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "trips"; }
        }

        public TripsRequest()
        {
            TripIds = new List<string>();
        }

        /// <summary>
        /// MANDATORY: Trip Ids must match those returned by a route or stop timetable, so you'll need to 
        /// hit one of those methods first. They change on at least a daily basis. 
        /// <remarks>A TripId will look something like '14278_3386354_20130808'. These are NOT the same trip-ids 
        /// as are used in the GTFS format. Render as CSV in query params</remarks>
        /// </summary>
        public List<string> TripIds { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like: 'trips?ids=14278_3386311_20130808,14278_3386354_20130808')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (!(TripIds.Any())) { throw new ArgumentException("TripIds needs to contain at least one TripId (e.g. 14278_3386311_20130808)"); }

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");

            string tripIds = String.Join(",", TripIds.Select(s => s.Trim()));
            sb.AppendFormat("ids={0}", tripIds);
            return sb.ToString();
        }
    }
}