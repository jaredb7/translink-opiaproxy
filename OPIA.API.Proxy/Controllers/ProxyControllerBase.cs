using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Akavache;
using Castle.Core.Logging;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response;
using OPIA.API.Proxy.Infrastructure.Installers;

namespace OPIA.API.Proxy.Controllers
{
    public class ProxyControllerBase : ApiController
    {
        /// <summary>
        /// Injected by IoC container for logging purposes
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Injected by IoC container for caching purposes
        /// </summary>
        public ProxyCache Cache { get; set; }

        protected T2 CheckCacheForEntry<T1, T2>(T1 request)
            where T1 : IRequest
            where T2 : IResponse
        {
            var result = default(T2);
            string key = GetRequestHash(request);
            Cache.GetObjectAsync<T2>(key).Subscribe(r => result = r, ex => Debug.WriteLine(string.Format("No key matching {0}", request.ToString())));
            Debug.WriteLine("{0} {1}", typeof(T2).Name, result == null ? "not in cache" : "item was cached!");
            Logger.DebugFormat("{0} {1}", typeof(T2).Name, result == null ? "not in cache" : "item was cached!");
            return result;
        }

        protected async Task StoreResultInCache<T1, T2>(T1 request, T2 response) where T1 : IRequest 
                                                                                 where T2 : IResponse
        {
            var key = GetRequestHash(request);
            await Cache.InsertObject<T2>(key, response, DateTime.Now.AddSeconds(60));
        }

        protected async Task StoreResultInCache(IRequest request, string response)
        {
            await Cache.InsertObject(GetRequestHash(request), response, TimeSpan.FromSeconds(10));
        }

        private static string GetRequestHash(IRequest request)
        {
            string hash = string.Empty;
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(request.ToString()));
                var sb = new StringBuilder();
                Array.ForEach(hashBytes, b => sb.AppendFormat("{0:x}", b));
                hash = sb.ToString();
            }
            return hash;
        }


    }
}