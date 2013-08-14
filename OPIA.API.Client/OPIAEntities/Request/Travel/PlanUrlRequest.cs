using System;
using System.Linq;
using System.Text;

namespace OPIA.API.Client.OPIAEntities.Request.Travel
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    /// <summary>
    /// This is essentially a clone of the PlanRequest, with only the RPC/route being slightly different.
    /// If this changes in future, we'll separate it and built it out at that point.
    /// </summary>
    public class PlanUrlRequest : PlanRequest, IRequest
    {

        public new string RpcMethodName
        {
            get { return "plan-url"; }
        }

        public PlanUrlRequest() : base()
        {
        }

        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'plan-url/SI%3A000026/AD%3A80%20Mary%20St%2C%20City?timeMode=1&at=2013-08-14%2011%3A00&vehicleTypes=2&walkSpeed=1&maximumWalkingDistanceM=500&serviceTypes=2,4&fareTypes=2')</returns>
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