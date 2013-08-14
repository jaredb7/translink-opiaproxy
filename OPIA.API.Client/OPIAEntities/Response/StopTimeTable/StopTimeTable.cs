﻿using System;
using OPIA.API.Client.OPIAEntities.Response.Common;

namespace OPIA.API.Client.OPIAEntities.Response.StopTimeTable
{
    public class StopTimeTable
    {
        public DateTime Date { get; set; }
        public Route[] Routes { get; set; }
        public Stop Stop { get; set; }
        public Trip[] Trips { get; set; }
    }
}