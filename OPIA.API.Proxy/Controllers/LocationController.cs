using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Location;
using OPIA.API.Contracts.OPIAEntities.Response;
using OPIA.API.Contracts.OPIAEntities.Response.Locations;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using OPIA.API.Contracts.OPIAEntities.Response.Stops;
using OPIA.API.Contracts.OPIAEntities.Response.StopsAtLandmark;
using OPIA.API.Contracts.OPIAEntities.Response.StopsNearby;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients. 
    /// We're attributing "Get" methods with "HttpPost" because it's easier to post the 
    /// objects from an external client than to translate query strings back into objects,
    /// only to fire them off again, simply to satisfy the Web Api naming conventions. 
    /// It allows us to share the data object contracts.
    /// </summary>
    public class LocationController : ProxyControllerBase
    {

        /// <summary>
        /// Attempts to resolves free-form text into a an address, stop, landmark, etc.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Resolve(ResolveRequest request)
        {
            var result = CheckCacheForEntry<IRequest, ResolveResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaLocationClient().ResolveAsync(request);
                await StoreResultInCache<IRequest, ResolveResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }



        /// <summary>
        /// Retrieves a list of Stops located at a Landmark
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetStopsAtLandmark(StopsAtLandmarkRequest request)
        {
            var result = CheckCacheForEntry<IRequest, StopsAtLandmarkResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaLocationClient().GetStopsAtLandmarkAsync(request);
                await StoreResultInCache<IRequest, StopsAtLandmarkResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Locates stops close to a specific location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetStopsNearby(StopsNearbyRequest request)
        {
            var result = CheckCacheForEntry<IRequest, StopsNearbyResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaLocationClient().GetStopsNearbyAsync(request);
                await StoreResultInCache<IRequest, StopsNearbyResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Retrieves a list of stops by their Stop ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetStopsByIds(StopsRequest request)
        {
            var result = CheckCacheForEntry<IRequest, StopsResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaLocationClient().GetStopsByIdsAsync(request);
                await StoreResultInCache<IRequest, StopsResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Retrieves one or more locations by their ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetLocationsByIds(LocationsRequest request)
        {
            var result = CheckCacheForEntry<IRequest, LocationsResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaLocationClient().GetLocationsByIdsAsync(request);
                await StoreResultInCache<IRequest, LocationsResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }


    }


}