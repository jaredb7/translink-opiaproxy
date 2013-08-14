namespace OPIA.API.Client.OPIAEntities.Request
{
    public interface IRequest
    {
        /// <summary>
        /// The API method name you'll be calling, this is suffixed to the 
        /// http://host/v1/lookupType/rest/ - e.g. https://opia.api.translink.com.au/v1/network/rest/route-timetables
        /// </summary>
        string RpcMethodName { get; }

        /// <summary>
        /// Method which maps all the properties to a set of query-string parameters
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}