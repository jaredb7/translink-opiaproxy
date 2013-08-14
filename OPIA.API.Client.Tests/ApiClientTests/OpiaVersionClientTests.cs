using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPIA.API.Client.OpiaApiClients;

namespace OPIA.API.Client.Tests.ApiClientTests
{
    [TestClass]
    public class OpiaVersionClientTests
    {
        [TestMethod]
        public void TestAPIVersion_MustReturnVersion()
        {
            var versionLookupClient = new OpiaVersionClient();
            string actualVersion = versionLookupClient.GetCurrentApiVersion(); 
            const string expectedVersion = "1.0";
            Assert.AreEqual(expectedVersion, actualVersion);
        }

        [TestMethod]
        public async Task TestAPIVersionAsync_MustReturnVersion()
        {
            var versionLookupClient = new OpiaVersionClient();
            string actualVersion = await versionLookupClient.GetCurrentApiVersionAsync(); 
            const string expectedVersion = "1.0";
            Assert.AreEqual(expectedVersion, actualVersion);
        }

        [TestMethod]
        public void TestAPIBuildNo_MustReturnBuildNo()
        {
            var versionLookupClient = new OpiaVersionClient();
            string actualVersion = versionLookupClient.GetCurrentApiBuildVersion(); 
            Assert.IsFalse(string.IsNullOrWhiteSpace(actualVersion));
        }

        [TestMethod]
        public async Task TestAPIBuildNoAsync_MustReturnBuildNo()
        {
            var versionLookupClient = new OpiaVersionClient();
            string actualVersion = await versionLookupClient.GetCurrentApiBuildVersionAsync(); 
            Assert.IsFalse(string.IsNullOrWhiteSpace(actualVersion));
        }
    }
}