using System.Threading.Tasks;
using OPIA.API.Client.Constants;
using OPIA.API.Client.OPIAEntities.Request;
using OPIA.API.Client.OPIAEntities.Response.Plan;
using OPIA.API.Client.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Client.OpiaApiClients
{
    public class OpiaTravelClient : OpiaBaseClient
    {
        public OpiaTravelClient(): base(OpiaApiConstants.TravelAPI)
        {

        }

        /// <summary>
        /// Generates travel plans between two locations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PlanResponse GetPlan(IRequest request)
        {
            var result = this.GetApiResult<IRequest, PlanResponse>(request);
            return result;
        }

        /// <summary>
        /// Generates travel plans between two locations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PlanResponse> GetPlanAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, PlanResponse>(request);
            return result;
        }

        /// <summary>
        /// Generates a URL to Translink's Journey Planner which suggests possible journeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PlanUrlResponse GetPlanUrl(IRequest request)
        {
            var result = this.GetApiResult<IRequest, PlanUrlResponse>(request);
            return result;
        }

        /// <summary>
        /// Generates a URL to Translink's Journey Planner which suggests possible journeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PlanUrlResponse> GetPlanUrlAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, PlanUrlResponse>(request);
            return result;
        }
    }
}