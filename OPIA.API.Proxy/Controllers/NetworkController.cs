using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Logging;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Network;
using OPIA.API.Contracts.OPIAEntities.Response.RouteMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Routes;
using OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.StopTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.TripMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Trips;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients. 
    /// We're attributing "Get" methods with "HttpPost" because it's easier to post the 
    /// objects from an external client than to translate query strings back into objects,
    /// only to fire them off again, simply to satisfy the Web Api naming conventions. 
    /// It allows us to share the data object contracts.
    /// </summary>
    public class NetworkController : ProxyControllerBase
    {
        [HttpPost]
        public async Task<HttpResponseMessage> GetRouteTimeTables(RouteTimeTablesRequest request)
        {
            var result = CheckCacheForEntry<IRequest, RouteTimeTablesResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaNetworkClient().GetRouteTimeTablesAsync(request);
                await StoreResultInCache<IRequest, RouteTimeTablesResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetStopTimeTables(StopTimeTablesRequest request)
        {
            var result = CheckCacheForEntry<IRequest, StopTimeTablesResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaNetworkClient().GetStopTimeTablesAsync(request);
                await StoreResultInCache<IRequest, StopTimeTablesResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetTrips(TripsRequest request)
        {
            var result = CheckCacheForEntry<IRequest, TripsResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaNetworkClient().GetTripsAsync(request);
                await StoreResultInCache<IRequest, TripsResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetTripMapPath(TripMapPathRequest request)
        {
            var result = CheckCacheForEntry<IRequest, TripMapPathResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaNetworkClient().GetTripMapPathAsync(request);
                await StoreResultInCache<IRequest, TripMapPathResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetRouteMapPath(RouteMapPathRequest request)
        {
            var result = CheckCacheForEntry<IRequest, RouteMapPathResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaNetworkClient().GetRouteMapPathAsync(request);
                await StoreResultInCache<IRequest, RouteMapPathResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetRoutes(RoutesRequest request)
        {
            var result = CheckCacheForEntry<IRequest, RoutesResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaNetworkClient().GetRoutesAsync(request);
                await StoreResultInCache<IRequest, RoutesResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }



    }


}