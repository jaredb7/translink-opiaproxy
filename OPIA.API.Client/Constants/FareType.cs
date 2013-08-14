using System;

namespace OPIA.API.Client.Constants
{
    /// <summary>
    /// Types of fares to consider, e.g. Prepaid, Free, etc. 
    /// Free = 1, Standard = 2, Prepaid = 4
    /// </summary>
    [Flags]
    public enum FareType
    {
        Free = 1, 
        Standard = 2, 
        Prepaid = 4
    }
}