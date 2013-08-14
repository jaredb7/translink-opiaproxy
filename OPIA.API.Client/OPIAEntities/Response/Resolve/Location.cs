﻿using Newtonsoft.Json;
using OPIA.API.Client.Constants;
using OPIA.API.Client.OPIAEntities.Response.Common;

namespace OPIA.API.Client.OPIAEntities.Response.Resolve
{
    public class Location
    {
        public string __type { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string LandmarkType { get; set; }
        public Position Position { get; set; }

        [JsonProperty(PropertyName= "Type")]
        public LocationType LocationType { get; set; }

        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetType { get; set; }
        public string Suburb { get; set; }
    }
}