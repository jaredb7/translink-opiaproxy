using System.Collections.Generic;

namespace OPIA.API.Contracts.Constants
{

    public static class OpiaApiConstants
    {

        public const string NetworkAPI = "network";
        public const string LocationAPI = "location";
        public const string VersionAPI = "version";
        public const string TravelAPI = "travel";
        public const string ServiceAPI = "??service??"; // they've not exposed this one yet.

        public const string OpiaApiBaseUrl = "https://opia.api.translink.com.au/v1/";

        public static List<string> OpiaApiLookupTypes = new List<string>()
                                                        {
                                                            NetworkAPI,
                                                            LocationAPI,
                                                            VersionAPI,
                                                            TravelAPI,
                                                            ServiceAPI,
                                                        };


    }

}