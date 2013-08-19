using System.Diagnostics;
using System.IO;
using System.Reactive.Concurrency;
using System.Reflection;
using Akavache;
using Akavache.Sqlite3;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OPIA.API.Proxy.Controllers;
using ReactiveUI;

namespace OPIA.API.Proxy.Infrastructure.Installers
{
    public class CacheInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<CacheFacility>();
        }
    }

    public class CacheFacility : AbstractFacility
    {

        //private SqliteProxyCache _sqliteCache;
        //public SqliteProxyCache SqliteCache
        //{
        //    get { return _sqliteCache; }
        //    set { _sqliteCache = value; }
        //}

        private ProxyCache _cache;
        public ProxyCache Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }

        protected override void Init()
        {
            Kernel.Register(
                            Component.For<ProxyCache>()
                                     .UsingFactoryMethod(_ => SetupCache())
                                     .LifestyleSingleton()
                           );

            //Kernel.Register(
            //                Component.For<SqliteProxyCache>()
            //                         .UsingFactoryMethod(_ => SetupSqliteCache())
            //                         .LifestyleSingleton()
            //               );

        }


        private ProxyCache SetupCache()
        {
            Debug.WriteLine("Using normal ProxyCache");
            string applicationName = Assembly.GetExecutingAssembly().GetName().Name;
            BlobCache.ApplicationName = applicationName;
            string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string cacheDir = Path.Combine(currentDir, @"..\App_Data\Cache");
            _cache = new ProxyCache(cacheDir);
            return _cache;
        }

        ///// <summary>
        ///// Can't use this one just yet, it won't to reload stuff from cache, even tho
        ///// it persists correctly into the sqlite DB. Item not found, yet it's there!
        ///// Works perfectly in a debugger, but no dice when running standalone. Must be 
        ///// some thread/concurrency issue
        ///// </summary>
        //private SqliteProxyCache SetupSqliteCache()
        //{
        //    Debug.WriteLine("Using SqliteProxyCache");
        //    string applicationName = Assembly.GetExecutingAssembly().GetName().Name;
        //    BlobCache.ApplicationName = applicationName;

        //    string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    string cacheDir = Path.Combine(currentDir, @"..\App_Data\Cache");
        //    if (!Directory.Exists(cacheDir)) { Directory.CreateDirectory(cacheDir); }

        //    string cacheFileName = Path.Combine(cacheDir, "cache.db");

        //    _sqliteCache = new SqliteProxyCache(cacheFileName);
        //    return _sqliteCache;
        //}


        protected override void Dispose()
        {
            BlobCache.Shutdown().Wait();
            base.Dispose();
        }
    }


    /// <summary>
    /// Use this one for now. It's backed by a directory instead of a DB, but at least 
    /// it works properly.
    /// </summary>
    public class ProxyCache : PersistentBlobCache
    {
        public ProxyCache(string cacheFileName) : base(cacheFileName)
        {
        }

    }
    ///// <summary>
    ///// Can't use this one just yet, it won't to reload stuff from cache, even tho
    ///// it persists correctly into the sqlite DB. Item not found, yet it's there!
    ///// Works perfectly in a debugger, but no dice when running standalone. Must be 
    ///// some thread/concurrency issue
    ///// </summary>
    //public class SqliteProxyCache : SqlitePersistentBlobCache
    //{
    //    public SqliteProxyCache(string cacheFileName) : base(cacheFileName)
    //    {
    //    }

    //    public SqliteProxyCache(string cacheFileName, IScheduler scheduler)
    //        : base(cacheFileName, scheduler)
    //    {
    //    }
    //}

}
