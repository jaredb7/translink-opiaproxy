using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OPIA.API.Proxy.Infrastructure.Installers
{
  public class WebApiControllersInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleScoped());
    }
  }
}