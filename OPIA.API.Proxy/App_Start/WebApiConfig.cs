using System.Web.Http;

namespace OPIA.API.Proxy.App_Start
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
            // subtly different from the default route normally associated with web api - allows 'RPC-style' calls to be made
            // by inserting the '{action}' field.
            // See http://stackoverflow.com/questions/12374657/how-do-i-do-rpc-style-asp-net-web-api-calls-properly?rq=1 for 
            // a possibly better way (tho it seems a little flaky, default post methods stop working with 'bad request').
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
    }
  }
}
