using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace OPIA.API.Proxy.Infrastructure.Installers
{
  /// <summary>
  /// We can't set up a controller factory for WebApi controllers, that would be too easy!
  /// WebAPI Only:: We need to create our own DependencyScope and DependencyResolver
  /// to point Castle Windsor at, for resolving the sub-dependencies introduced in the WebApi
  /// IHttpController. It's either that, or register all the IHttpController dependencies manually
  /// which could get messy (especially if they change over time). See
  /// http://localitgeeks.blogspot.com.au/2012/08/castle-windsor-web-api-and.html for more info.
  /// </summary>
  public class WindsorDependencyScope : IDependencyScope
  {
    private readonly IWindsorContainer _container;
    private readonly IDisposable _scope;
    private bool _disposed;

    public WindsorDependencyScope(IWindsorContainer container)
    {
      _container = container;
      _scope = _container.BeginScope();
    }
    public void Dispose()
    {
      if (_disposed) return;
      _scope.Dispose();
      _disposed = true;
      GC.SuppressFinalize(this);
    }
    public object GetService(Type serviceType)
    {
      EnsureNotDisposed();
      return _container.Kernel.HasComponent(serviceType) ? _container.Kernel.Resolve(serviceType) : null;
    }
    public IEnumerable<object> GetServices(Type serviceType)
    {
      EnsureNotDisposed();
      return _container.ResolveAll(serviceType).Cast<object>();
    }
    private void EnsureNotDisposed()
    {
      if (_disposed) throw new ObjectDisposedException("WindsorDependencyScope");
    }
  }

  /// <summary>
  /// WebAPI Only:: We need to create our own DependencyScope and DependencyResolver
  /// to point Castle Windsor at, for resolving the sub-dependencies introduced in the WebApi
  /// IHttpController. It's either that, or register all the IHttpController dependencies manually
  /// which could get messy. See
  /// http://localitgeeks.blogspot.com.au/2012/08/castle-windsor-web-api-and.html for more info    /// </summary>
  public class WindsorDependencyResolver : IDependencyResolver
  {
    private readonly IWindsorContainer _container;

    public WindsorDependencyResolver(IWindsorContainer container)
    {
      _container = container;
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public object GetService(Type serviceType)
    {
      return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
      return _container.ResolveAll(serviceType).Cast<object>();
    }

    public IDependencyScope BeginScope()
    {
      return new WindsorDependencyScope(_container);
    }
  }
}