﻿using OPIA.API.Contracts.OPIAEntities.Response.Resolve;

namespace OPIA.API.Contracts.OPIAEntities.Response.StopsAtLandmark
{
    public class StopsAtLandmarkResponse : IResponse
    {
        Location[] Locations { get; set; }
    }
}