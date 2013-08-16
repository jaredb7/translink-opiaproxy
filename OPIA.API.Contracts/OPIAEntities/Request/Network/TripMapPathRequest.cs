using System;
using System.Text;

namespace OPIA.API.Contracts.OPIAEntities.Request.Network
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class TripMapPathRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "trip-map-path"; }
        }

        public TripMapPathRequest()
        {
        }

        /// <summary>
        /// MANDATORY: Only one TripId at a time. TripId must match one of those returned by a route or stop timetable, 
        /// so you'll need to hit one of those methods first. They change on at least a daily basis. 
        /// <remarks>A TripId will look something like '14278_3386354_20130808'. These are NOT the same trip-ids 
        /// as are used in the GTFS format.</remarks>
        /// </summary>
        public string TripId { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like: 'trip-map-path?tripId=14278_3386311_20130808')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (string.IsNullOrWhiteSpace(TripId)) {  throw new ArgumentException("TripId cannot be empty");}

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");
            sb.AppendFormat("tripId={0}", TripId);
            return sb.ToString();
        }
    }
}