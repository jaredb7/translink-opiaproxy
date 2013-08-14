using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPIA.API.Client.Constants;

namespace OPIA.API.Client.OPIAEntities.Request.Network
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class RouteTimeTablesRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "route-timetables"; }
        }

        public RouteTimeTablesRequest()
        {
            RouteCodes = new List<string>();
            Directions = new List<DirectionType>();
            Date = default(DateTime);
        }

        /// <summary>
        /// MANDATORY: The route codes or train lines to retrieve timetables for (e.g. 'P137' or 'SHBN') . Hard limit of 50 Route Codes per request.
        /// You can, but shouldn't, mix different modes of transport because it can't do a mix of vehicles. See the VehicleType property.
        /// </summary>
        public List<string> RouteCodes { get; set; }

        /// <summary>
        /// MANDATORY: Type of vehicle, e.g. Bus. // None = 0, Bus = 2, Ferry = 4, Train = 8, Walk = 16. Can only use ONE at a time,
        /// so be careful if you try to get a mix of route codes for different modes of transport (e.g. Bus AND Train, or don't use anything at all,
        /// it just comes back with invalid parameter error). If you use '0', you never get anything back, ever.
        /// </summary>
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// MANDATORY: Date to retrieve the timetable for. Time component is ignored	Date (use yyyy-MM-dd, it's safest)
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// MANDATORY: Whether to filter the resulting Timetable[n].Trips[i].StopIds list to just start/end stop (use 'true' or 'false')
        /// </summary>
        public bool FilterToStartEndStops { get; set; } 

        /// <summary>
        /// Optional list of directions to filter the resulting timetables (cast to integer and use as csv)
        /// Leave empty to take all directions, otherwise can string these together. 
        /// </summary>
        public List<DirectionType> Directions { get; set; }

        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// </summary>
        /// <returns>String (looks like 'route-timetables?routeCodes=P137,130&date=2013-08-07&vehicleType=2&filterToStartEndStops=true')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (!(RouteCodes.Any())) { throw new ArgumentException("RouteCodes needs to contain at least one route code (e.g. P137)"); }
            if (VehicleType == VehicleType.None){ throw new ArgumentException("VehicleType should be one of the following: Bus = 2, Ferry = 4, Train = 8, Walk = 16"); }
            if (Date == default(DateTime)) { throw new ArgumentException("Date needs to be properly set: Currently it's defaulted to {0}", Date.ToString("yyyy-MM-dd"));}

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");

            string routeCodes = String.Join(",", RouteCodes.Select(rc => rc.Trim()));
            sb.AppendFormat("routeCodes={0}", routeCodes);

            sb.AppendFormat("&date={0}", Date.ToString("yyyy-MM-dd"));

            sb.AppendFormat("&vehicleType={0}", (int)VehicleType);

            if (Directions.Any())
            {
                sb.Append("&directions=");
                sb.Append(String.Join(",", Directions.Select(d => Convert.ToString((int)d).ToArray())));
            }

            sb.AppendFormat("&filterToStartEndStops={0}", FilterToStartEndStops ? "true" : "false");

            return sb.ToString();
        }
    }
}