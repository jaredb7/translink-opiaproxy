namespace OPIA.API.Contracts.OPIAEntities.Response.StopTimeTable
{
    public class StopTimeTablesResponse : IResponse
    {
        public StopTimeTable[] StopTimetables { get; set; }
    }
}