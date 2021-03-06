using System.Net.Http;
using System.Threading.Tasks;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Version;

namespace OPIA.API.Client.OpiaApiClients
{
    /// Abstraction/Facade client layer for interfacing with the actual Opia API method calls. Allows
    /// us to change names, add functionality, etc.
    public partial class OpiaVersionClient : OpiaBaseClient
    {

        // GET /version/rest/api API's public interface version
        // GET /version/rest/build Internal Translink build number of the API

        public OpiaVersionClient(): base(OpiaApiConstants.VersionAPI)
        {
        }


        /// <summary>
        /// Synchronously hits the 'api' RPC command 
        /// </summary>
        /// <returns>The current version API ("\"1.0\"")</returns>
        /// <exception cref="HttpRequestException">An HttpRequestException containing an HTTP error response (e.g. 404, 403, etc.)</exception>
        public string GetCurrentApiVersion()
        {
            var result = this.GetApiResult<IRequest, string>(new ApiVersionRequest());
            return result;
        }

        /// <summary>
        /// This should only be used to get the build version when reporting defects; has
        /// apparently no bearing on the actual API version. Just used for diagnostic purposes
        /// </summary>
        /// <returns>The current version API ("\"1.0\"")</returns>
        /// <exception cref="HttpRequestException">An HttpRequestException containing an HTTP error response (e.g. 404, 403, etc.)</exception>
        public string GetCurrentApiBuildVersion()
        {
            var result = this.GetApiResult<IRequest, string>(new BuildVersionRequest());
            return result;
        }
    }
}