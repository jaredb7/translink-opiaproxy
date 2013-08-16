namespace OPIA.API.Contracts.OPIAEntities.Request.Version
{
    public class ApiVersionRequest : IRequest
    {
        public string RpcMethodName { get { return "api"; } }

        public override string ToString()
        {
            return RpcMethodName;
        }
    }
}