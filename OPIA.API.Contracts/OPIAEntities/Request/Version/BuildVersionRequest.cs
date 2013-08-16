namespace OPIA.API.Contracts.OPIAEntities.Request.Version
{
    public class BuildVersionRequest : IRequest
    {
        public string RpcMethodName { get { return "build"; } }

        public override string ToString()
        {
            return RpcMethodName;
        }
    }
}