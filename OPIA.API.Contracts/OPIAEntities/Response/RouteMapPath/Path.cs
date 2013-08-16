using Newtonsoft.Json;
using OPIA.API.Contracts.Constants;

namespace OPIA.API.Contracts.OPIAEntities.Response.RouteMapPath
{
    public class Path
    {
        public DirectionType Direction { get; set; }

        [JsonProperty(PropertyName = "Path")] // manually renamed, since property can't have same name as container
        public string MapPoints { get; set; }
    }
}