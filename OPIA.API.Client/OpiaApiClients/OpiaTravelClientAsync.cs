using System.Threading.Tasks;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response.Plan;
using OPIA.API.Contracts.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Client.OpiaApiClients
{
    public partial class OpiaTravelClient
    {

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
        public async Task<PlanUrlResponse> GetPlanUrlAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, PlanUrlResponse>(request);
            return result;
        }
    }
}