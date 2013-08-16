using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request;

namespace OPIA.API.Client.OpiaApiClients
{
    public class OpiaBaseClient
    {
        protected WebRequestHandler WebRequestHandler { get; set; }

        private readonly Uri _baseUri;

        private HttpClient _httpClient;
        private readonly string _opiaLogin;
        private readonly string _opiaPassword;

        protected HttpClient HttpClient
        {
            get {  return _httpClient ?? SetupNewHttpClient(); }
            private set { _httpClient = value; }
        }

        /// <summary>
        /// <remarks>'service' has apparently not been exposed as of 2013/08/06</remarks>
        /// </summary>
        /// <param name="lookupType">Should be one of <see cref="OPIA.API.Contracts.Constants.OpiaApiConstants"/></param>
        public OpiaBaseClient(string lookupType)
        {
            if (!OpiaApiConstants.OpiaApiLookupTypes.Contains(lookupType))
            {
                throw new NotImplementedException("Lookup type of {0} isn't valid or is not supported (yet). Check https://opia.api.translink.com.au/v1/api-docs.json");
            }

            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

            _baseUri = new Uri(string.Format("{0}{1}/rest/", OpiaApiConstants.OpiaApiBaseUrl, lookupType));

            _opiaLogin = ConfigurationManager.AppSettings["opiaLogin"];
            _opiaPassword = ConfigurationManager.AppSettings["opiaPassword"];

        }

        /// <summary>
        /// Synchronously load information from the selected OPIA API endpoint
        /// </summary>
        /// <typeparam name="T1">IRequest containing the API RPC method's parameters</typeparam>
        /// <typeparam name="T2">The type of ResponseEntity model to populate </typeparam>
        /// <param name="requestEntity">The <see cref="OPIA.API.Contracts.OPIAEntities.Request.IRequest"/> containing the API RPC method's parameters</param>
        /// <returns>The populated ResponseEntity model</returns>
        public virtual T2 GetApiResult<T1, T2>(T1 requestEntity) where T1 : IRequest
        {
            var request = this.HttpClient.GetAsync(requestEntity.ToString());
            var response = request.Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<T2>().Result;
            return result;
        }

        /// <summary>
        /// Asynchronously load information from the selected OPIA API endpoint
        /// </summary>
        /// <typeparam name="T1">IRequest containing the API RPC method's parameters</typeparam>
        /// <typeparam name="T2">The type of ResponseEntity model to populate </typeparam>
        /// <param name="requestEntity">The <see cref="OPIA.API.Contracts.OPIAEntities.Request.IRequest"/> containing the API RPC method's parameters</param>
        /// <returns>The populated ResponseEntity model</returns>
        public virtual async Task<T2> GetApiResultAsync<T1, T2>(T1 requestEntity) where T1 : IRequest
        {
            var response = await this.HttpClient.GetAsync(requestEntity.ToString());
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<T2>().Result;
            return result;
        }


        private HttpClient SetupNewHttpClient()
        {
            WebRequestHandler = new WebRequestHandler // since this OPIA API uses Basic Auth, we have to handle this too.
            {
                Credentials = new NetworkCredential() { UserName = _opiaLogin, Password = _opiaPassword}
            };

            _httpClient = new HttpClient(WebRequestHandler) { BaseAddress = _baseUri };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }

        /// <summary>
        /// Naiive SSL certificate validation - this one only cares if SSL is being used to encrypt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
            //   if (sslPolicyErrors == SslPolicyErrors.None) return true;
            //
            //    Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            //
            //    // Do not allow this client to communicate with unauthenticated servers. 
            //    return false;
        }

    }
}