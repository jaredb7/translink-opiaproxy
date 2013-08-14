namespace OPIA.API.Client.OPIAEntities.Request.Version
{
    class ApiVersionRequest : IRequest
    {
        public string RpcMethodName { get { return "api"; } }

        public override string ToString()
        {
            return RpcMethodName;
        }
    }
}