using System;
using System.Text;
using OPIA.API.Contracts.Constants;

namespace OPIA.API.Contracts.OPIAEntities.Request.Network
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class RouteMapPathRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "route-map-path"; }
        }

        public RouteMapPathRequest()
        {
            Date = default(DateTime);
        }

        /// <summary>
        /// MANDATORY: Only one RouteId at a time. The route code retrieve timetables for (e.g. 'P137' or 'SHBN') . 
        /// </summary>
        /// <summary>
        /// You can, but shouldn't, mix different modes of transport because it can't do a mix of vehicles. See the VehicleType property.
        /// </summary>
        public string RouteCode { get; set; }

        /// <summary>
        /// MANDATORY: Type of vehicle, e.g. Bus. // None = 0, Bus = 2, Ferry = 4, Train = 8, Walk = 16. If you use '0', 
        /// you never get anything back, ever.
        /// </summary>
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// MANDATORY: Date to retrieve the information for. Time component is ignored (use 'yyyy-MM-dd' when rendering, it's safest)
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'route-map-path?routeCode=333&vehicleType=2&date=2013-08-10')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) { throw new ArgumentException("RpcMethodName cannot be empty"); }
            if (string.IsNullOrWhiteSpace(RouteCode)) { throw new ArgumentException("RouteCode cannot be empty"); }
            if (VehicleType == VehicleType.None) { throw new ArgumentException("VehicleType should be one of the following: Bus = 2, Ferry = 4, Train = 8, Walk = 16"); }
            if (Date == default(DateTime)) { throw new ArgumentException("Date needs to be properly set: Currently it's defaulted to {0}", Date.ToString("yyyy-MM-dd")); }

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");

            sb.AppendFormat("routeCode={0}", RouteCode);
            sb.AppendFormat("&vehicleType={0}", (int)VehicleType);
            sb.AppendFormat("&date={0}", Date.ToString("yyyy-MM-dd"));

            return sb.ToString();
        }
    }
}