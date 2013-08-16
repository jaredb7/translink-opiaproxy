using System.Web.Http;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using OPIA.API.Proxy.Infrastructure.Factories;
using OPIA.API.Proxy.Infrastructure.Installers;

namespace OPIA.API.Proxy.App_Start
{
  public class WindsorContainerConfig
  {
    private static IWindsorContainer _container;

    public static void RegisterContainer(HttpConfiguration configuration)
    {
      _container = new WindsorContainer().Install(FromAssembly.This());

      // finally set up the controller factory so that MVC will use it. 
      var controllerFactory = new WindsorControllerFactory(_container.Kernel);
      ControllerBuilder.Current.SetControllerFactory(controllerFactory);

      // we need this just for the WebApi controller stuff, we can't use a ControllerFactory
      // like MVC. See the WindsorDependencyResolver class for more info
      var dependencyResolver = new WindsorDependencyResolver(_container);
      configuration.DependencyResolver = dependencyResolver;
    }

    public static void DisposeContainer()
    {
      if (_container != null)
      {
        _container.Dispose();
      }
    }
  }
}