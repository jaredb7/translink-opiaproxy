using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Logging;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Travel;
using OPIA.API.Contracts.OPIAEntities.Response.Plan;
using OPIA.API.Contracts.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients. 
    /// We're attributing "Get" methods with "HttpPost" because it's easier to post the 
    /// objects from an external client than to translate query strings back into objects,
    /// only to fire them off again, simply to satisfy the Web Api naming conventions. 
    /// It allows us to share the data object contracts.
    /// </summary>
    public class TravelController : ProxyControllerBase
    {

        /// <summary>
        /// Generates travel plans between two locations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetPlan(PlanRequest request)
        {
            var result = CheckCacheForEntry<IRequest, PlanResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaTravelClient().GetPlanAsync(request);
                await StoreResultInCache<IRequest, PlanResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Generates a URL to Translink's Journey Planner which suggests possible journeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetPlanUrl(PlanUrlRequest request)
        {
            var result = CheckCacheForEntry<IRequest, PlanUrlResponse>(request);
            if (result == null)
            {
                Logger.DebugFormat("Getting {0} from web: ", request.ToString());
                result = await new OpiaTravelClient().GetPlanUrlAsync(request);
                await StoreResultInCache<IRequest, PlanUrlResponse>(request, result);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }


    }


}