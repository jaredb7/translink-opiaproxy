using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reflection;
using Akavache;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

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
        private ProxyCache _cache;
        public ProxyCache Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }

        //private SqliteProxyCache _sqliteCache;
        //public SqliteProxyCache SqliteCache
        //{
        //    get { return _sqliteCache; }
        //    set { _sqliteCache = value; }
        //}


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
    /// Use a <see cref="PersistentBlobCache"/> descendant for now. It's backed by a 
    /// directory instead of a DB, but at least it works properly.
    /// </summary>
    public class ProxyCache : PersistentBlobCache
    {

        public ProxyCache(string cacheFileName) : base(cacheFileName)
        {
            SetupCacheClearingSchedule();
        }

        private void SetupCacheClearingSchedule()
        {
            var timeNow = DateTime.UtcNow;
            var scheduler = Scheduler.AsPeriodic(); 
            var timeSpan = new TimeSpan(0, 1, 0, 0); // TODO make this configurable
            Debug.WriteLine("Setting up cache clearing schedule...");
            scheduler.SchedulePeriodic(timeNow, timeSpan, ClearTheExpiredCacheItems);
            Debug.WriteLine("Schedule set.");
        }

        private DateTime ClearTheExpiredCacheItems(DateTime timeNow)
        {
            Debug.WriteLine("Cache-clearing run started...");
            var allKeys = GetAllKeys().ToList();
            allKeys.ForEach(GarbageCollectExpiredItemsByKey);
            Debug.WriteLine("Cache-clearing run completed in {0}ms", TimeSpan.FromTicks(DateTime.UtcNow.Ticks - timeNow.Ticks).Milliseconds);
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Simply trying to load the object associated with the key will invalidate the 
        /// object if it's stale - this is built in functionality provided by Akavache.
        /// We don't need to do anything other than hit that key to cause it's associated 
        /// item to be clobbered. I'd feel better about this if we had an extension method 
        /// that did an "InvalidateExpiredItems" that we could call explicitly, 
        /// or "InvalidateItemsOlderThan" because if the key never gets revisited (for instance
        /// if its value is time dependent) then they just pile up; there's no garbage collection.
        /// </summary>
        /// <param name="key"></param>
        private void GarbageCollectExpiredItemsByKey(string key)
        {
            byte[] dontCare;
            // if the item has expired, Akavache will clobber it for us. This is all we need to do. It'll throw an exception
            // tho, saying said key wasn't in the cache - this is also Akavache functionlity. We'll trap, log and 
            // shut up about it, because we don't care.
            GetAsync(key).Subscribe(bytes => dontCare= bytes, ex => Debug.WriteLine("Key {0} has been invalidated/cleared", key)); 
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
