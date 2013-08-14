namespace OPIA.API.Client.OPIAEntities.Request.Version
{
    class BuildVersionRequest : IRequest
    {
        public string RpcMethodName { get { return "build"; } }

        public override string ToString()
        {
            return RpcMethodName;
        }
    }
}