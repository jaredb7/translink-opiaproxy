using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPIA.API.Contracts.OPIAEntities.Request.Network
{
    // TODO Custom attributes on properties; will make mapping to query params easier.
    public class StopTimeTablesRequest : IRequest
    {

        public string RpcMethodName
        {
            get { return "stop-timetables"; }
        }

        public StopTimeTablesRequest()
        {
            StopIds = new List<string>();
            RouteCodesFilter = new List<string>();
            Date = default(DateTime);
        }


        /// <summary>
        /// MANDATORY: These can either be padded out to 6 digits on the left, or not, it appears
        /// their API will pad them internally if not. Render as CSV in query params
        /// </summary>
        public List<string> StopIds { get; set; }

        /// <summary>
        /// OPTIONAL: Optional list of routes travelling through stopIds to filter results to. 
        /// <remarks>This parameter doesn't seem to work very well, it doesn't appear to filter in/out anything</remarks>
        /// </summary>
        public List<string> RouteCodesFilter { get; set; }

        /// <summary>
        /// MANDATORY: Date to retrieve the timetable for. Time component is ignored Date (use yyyy-MM-dd, it's safest)
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Converts the whole object into a series of query string parameters that can be passed to the RPC method
        /// 
        /// </summary>
        /// <returns>String (looks like: 'stop-timetables?stopIds=148,5840&date=2013-08-08&routeFilter=131,P129')</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RpcMethodName)) {  throw new ArgumentException("RpcMethodName cannot be empty");}
            if (!(StopIds.Any())) { throw new ArgumentException("StopIds needs to contain at least one Stop Id (e.g. 148 or 000148, zero-padding is optional)"); }
            if (Date == default(DateTime)) { throw new ArgumentException("Date needs to be properly set: Currently it's defaulted to {0}", Date.ToString("yyyy-MM-dd")); }

            var sb = new StringBuilder();
            sb.Append(RpcMethodName).Append("?");

            string stopIds = String.Join(",", StopIds.Select(s => s.Trim()));
            sb.AppendFormat("stopIds={0}", stopIds);

            sb.AppendFormat("&date={0}", Date.ToString("yyyy-MM-dd"));

            if (RouteCodesFilter.Any())
            {
                sb.Append("&routeFilter=");
                sb.Append(String.Join(",", RouteCodesFilter.Select(rc => rc).ToArray()));
            }

            return sb.ToString();
        }
    }
}