using System;

namespace OPIA.API.Contracts.Constants
{
    /// <summary>
    /// Types of services to consider, e.g. Regular, Express, etc. 
    /// Regular = 1, Express = 2, NightLink = 4, School = 8, Industrial = 16
    /// </summary>
    [Flags]
    public enum ServiceType
    {
        Regular = 1, 
        Express = 2, 
        NightLink = 4, 
        School = 8, 
        Industrial = 16
    }
}