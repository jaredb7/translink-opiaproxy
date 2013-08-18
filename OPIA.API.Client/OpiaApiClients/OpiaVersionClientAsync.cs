using System.Net.Http;
using System.Threading.Tasks;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Version;

namespace OPIA.API.Client.OpiaApiClients
{
    public partial class OpiaVersionClient
    {

        /// <summary>
        /// Asynchronously hits the 'api' RPC command 
        /// </summary>
        /// <returns>The current version API ("\"1.0\"")</returns>
        /// <exception cref="HttpRequestException">An HttpRequestException containing an HTTP error response (e.g. 404, 403, etc.)</exception>
        public async Task<string> GetCurrentApiVersionAsync()
        {
            var result = await this.GetApiResultAsync<IRequest, string>(new ApiVersionRequest());
            return result;
        }

        /// <summary>
        /// Async: This should only be used to get the build version when reporting defects; has
        /// apparently no bearing on the actual API version. Just used for diagnostic purposes
        /// </summary>
        /// <returns>The current version API ("\"1.0\"")</returns>
        /// <exception cref="HttpRequestException">An HttpRequestException containing an HTTP error response (e.g. 404, 403, etc.)</exception>
        public async Task<string> GetCurrentApiBuildVersionAsync()
        {
            var result = await this.GetApiResultAsync<IRequest, string>(new BuildVersionRequest());
            return result;
        }
    }
}