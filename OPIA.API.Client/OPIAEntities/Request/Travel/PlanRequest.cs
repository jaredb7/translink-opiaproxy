using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPIA.API.Client.Constants;

namespace OPIA.API.Client.OPIAEntities.Request.Travel
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class PlanRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "plan"; }
        }

        public PlanRequest()
        {
            DateAndTime = default(DateTime);
            ServiceTypes = new List<ServiceType>();
            VehicleTypes = new List<VehicleType>();
            FareTypes = new List<FareType>();
        }

        /// <summary>
        /// MANDATORY: Location.Id of journey start. This will look something like 'AD:80 Mary St, City' or 'SI:000026' 
        /// and will probably have to come from a previous call to a different Location API method
        /// </summary>
        public string FromLocationId { get; set; }

        /// <summary>
        /// MANDATORY: Location.Id of journey end. This will look something like 'AD:80 Mary St, City' or 'SI:000026'
        /// and will probably have to come from a previous call to a different Location API method
        /// </summary>
        public string ToLocationId { get; set; }

        /// <summary>
        /// MANDATORY: Type of vehicle, e.g. Bus. // None = 0, Bus = 2, Ferry = 4, Train = 8, Walk = 16. 
        /// </summary>
        public List<VehicleType> VehicleTypes { get; set; }

        /// <summary>
        /// MANDATORY: Whether to arrive before, after, etc the time specified. 
        /// LeaveAfter = 0, ArriveBefore = 1, FirstServices = 2, LastServices = 3
        /// Will default to LeaveAfter (0)
        /// </summary>
        public TimeModeType TimeModeType { get; set; }

        /// <summary>
        /// MANDATORY: Date and time to calculate the journey. Time component must be included. If you
        /// leave it out, it defaults to midnight. (use 'yyyy-MM-dd HH:mm' when rendering, it's safest)
        /// </summary>
        public DateTime DateAndTime { get; set; }

        /// <summary>
        /// MANDATORY: Speed to assume customer can walk between journey legs, and to/from the From and To locations. 
        /// Slow = 0, Normal = 1, Fast = 2. Defaults to 0 (slow)
        /// </summary>
        public WalkingSpeedType WalkingSpeedType { get; set; }

        /// <summary>
        /// MANDATORY: Max walking distance between stops/trips/pickup/dropoff. Must be > 100 metres,
        /// but suggest 400-500 to start, and wind it back in if necessary
        /// </summary>
        public int MaximumWalkingDistanceM { get; set; }

        /// <summary>
        /// MANDATORY: Types of services to consider, e.g. Regular, Express, etc. 
        /// Regular = 1, Express = 2, NightLink = 4, School = 8, Industrial = 16
        /// </summary>
        public List<ServiceType> ServiceTypes { get; set; }


        /// <summary>
        /// Types of fares to consider, e.g. Prepaid, Free, etc. 
        /// Free = 1, Standard = 2, Prepaid = 4
        /// </summary>
        public List<FareType> FareTypes { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'plan/SI%3A000026/AD%3A80%20Mary%20St%2C%20City?timeMode=1&at=2013-08-14%2011%3A00&vehicleTypes=2&walkSpeed=1&maximumWalkingDistanceM=500&serviceTypes=2,4&fareTypes=2')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) { throw new ArgumentException("RpcMethodName cannot be empty"); }
            if (string.IsNullOrWhiteSpace(FromLocationId)) { throw new ArgumentException("FromLocationId cannot be empty. It will look something like 'AD:80 Mary St, City' or 'SI:000026'"); }
            if (string.IsNullOrWhiteSpace(ToLocationId)) { throw new ArgumentException("ToLocationId cannot be empty. It will look something like 'AD:80 Mary St, City' or 'SI:000026'"); }
            if (!VehicleTypes.Any()) { throw new ArgumentException("VehicleTypes should be at least one of the following: Bus = 2, Ferry = 4, Train = 8, Walk = 16"); }
            if (DateAndTime == default(DateTime)) { throw new ArgumentException(string.Format("DateAndTime needs to be properly set: Currently it's defaulted to {0}", DateAndTime.ToString("yyyy-MM-dd HH:mm"))); }
            if (MaximumWalkingDistanceM <= 100) { throw new ArgumentException("MaximumWalkingDistanceM must be > 100m"); }
            if (!ServiceTypes.Any()) { throw new ArgumentException("One or more ServiceTypes should be chosen: Regular = 1, Express = 2, NightLink = 4, School = 8, Industrial = 16"); }
            if (!FareTypes.Any()) { throw new ArgumentException("One or more FareTypes should be chosen: Free = 1, Standard = 2, Prepaid = 4"); }

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("/");

            sb.AppendFormat("{0}/", Uri.EscapeDataString(FromLocationId));
            sb.AppendFormat("{0}?", Uri.EscapeDataString(ToLocationId));
            sb.AppendFormat("timeMode={0}", (int)TimeModeType);
            sb.AppendFormat("&at={0}", Uri.EscapeDataString(DateAndTime.ToString("s"))); // "yyyy-MM-ddTHH:nn:ss
            sb.AppendFormat("&vehicleTypes={0}", String.Join(",", VehicleTypes.Select(v => (int) v).ToArray()));
            sb.AppendFormat("&walkSpeed={0}", (int)WalkingSpeedType);
            sb.AppendFormat("&maximumWalkingDistanceM={0}", MaximumWalkingDistanceM);
            sb.AppendFormat("&serviceTypes={0}", String.Join(",", ServiceTypes.Select(s => (int) s).ToArray()));
            sb.AppendFormat("&fareTypes={0}", String.Join(",", FareTypes.Select(f => (int) f).ToArray()));

            return sb.ToString();
        }
    }
}