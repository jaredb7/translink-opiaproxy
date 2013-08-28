using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OPIA.API.Proxy.Tests.ProxyTests
{
    [TestClass]
    public class ProxyVersionControllerMethodsTests
    {
        private static TestContext _testContext;
        private HttpClient _client;


        [ClassInitialize]
        public static void ClassInitialiser(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestInitialize]
        public void TestInitialiser()
        {
            SetupNewHttpClient();
        }

        private void SetupNewHttpClient()
        {
            string baseUrl = ConfigurationManager.AppSettings["proxyApiBaseUrl"];
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
            // any SSL, auth or setting of tokens or cookies whatever will probably need to go in here. 
        }

        [TestMethod]
        public void TestAPIVersion_MustReturnVersion()
        {
            var response = _client.GetAsync("version/getcurrentapiversion").Result;
            response.EnsureSuccessStatusCode();
            var actualVersion = response.Content.ReadAsAsync<string>().Result;
            const string expectedVersion = "1.0";
            Assert.AreEqual(expectedVersion, actualVersion);
        }


        [TestMethod]
        public void TestAPIBuildNo_MustReturnBuildNo()
        {
            var response = _client.GetAsync("version/getcurrentapibuildversion").Result;
            response.EnsureSuccessStatusCode();
            var actualVersion = response.Content.ReadAsAsync<string>().Result;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actualVersion));
        }

 

    }
}