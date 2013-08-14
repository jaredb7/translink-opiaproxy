using System;
using OPIA.API.Client.Constants;

namespace OPIA.API.Client.OpiaApiClients
{
    /// <summary>
    /// 'Service' API apparently not implemented yet (2013/08/06)
    /// </summary>
    public class OpiaServiceClient : OpiaBaseClient
    {
        public OpiaServiceClient(): base(OpiaApiConstants.ServiceAPI)
        {
            throw new NotImplementedException(
                string.Format("Lookup type of {0} isn't valid or is not supported (yet). " +
                              "Check https://opia.api.translink.com.au/v1/api-docs.json",
                              OpiaApiConstants.ServiceAPI));
        }

    }
}