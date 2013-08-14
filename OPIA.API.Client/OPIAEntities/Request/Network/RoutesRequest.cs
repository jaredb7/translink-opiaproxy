using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPIA.API.Client.Constants;

namespace OPIA.API.Client.OPIAEntities.Request.Network
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class RoutesRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "routes"; }
        }

        public RoutesRequest()
        {
            Date = default(DateTime);
            VehicleTypes = new List<VehicleType>();
            RouteCodes = new List<string>();
        }

        /// <summary>
        /// Optional: The route codes or train lines to retrieve timetables for (e.g. 'P137' or 'SHBN') . Not sure about a limit on this one.
        /// Render as CSV
        /// </summary>
        public List<string> RouteCodes { get; set; }

        /// <summary>
        /// Optional: Types of vehicle, e.g. Bus. // None = 0, Bus = 2, Ferry = 4, Train = 8, Walk = 16 to filter to. 
        /// You can use more than one at a time, but there's no guarantee on the order the types of vehicle routes will be returned in. 
        /// Render as CSV
        /// </summary>
        public List<VehicleType> VehicleTypes { get; set; }

        /// <summary>
        /// MANDATORY: Date to retrieve the timetable for. Time component is ignored	Date (use yyyy-MM-dd, it's safest)
        /// </summary>
        public DateTime Date { get; set; }



        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'routes?date=2013-08-10&vehicleTypes=2&routeCodes=333')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (!(RouteCodes.Any())) { throw new ArgumentException("RouteCodes needs to contain at least one route code (e.g. 330 or P137)"); }
            if (Date == default(DateTime)) { throw new ArgumentException("Date needs to be properly set: Currently it's defaulted to {0}", Date.ToString("yyyy-MM-dd"));}

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");
            sb.AppendFormat("date={0}", Date.ToString("yyyy-MM-dd"));

            if (VehicleTypes.Any())
            {
                sb.Append("&vehicleTypes=");
                sb.Append(String.Join(",", VehicleTypes.Select(v => (int) v).ToArray()));
            }

            if (RouteCodes.Any())
            {
                string routeCodes = String.Join(",", RouteCodes.Select(rc => rc.Trim()));
                sb.AppendFormat("&routeCodes={0}", routeCodes);
            }

            return sb.ToString();
        }
    }
}