#if !NET35CLIENT && !NET40CLIENT && !SILVERLIGHT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using Zirpl.Logging;

namespace Zirpl.AppEngine.Logging.Common
{
    public class CommonLogFactory : ILogFactory
    {
        private static Boolean _initialized;

        /// <summary>
        /// Initializes the LogManager
        /// </summary>
        internal CommonLogFactory()
        {
            if (!log4net.LogManager.GetRepository().Configured)
            {
                XmlConfigurator.Configure();
                LogManager.GetLog<CommonLogFactory>().DebugFormat("Loaded log4net config.");
            }
            else
            {
                LogManager.GetLog<CommonLogFactory>().DebugFormat("log4net already configured.");
            }
        }

        public static void Initialize()
        {
            if (!_initialized)
            {
                LogManager.LogFactory = new CommonLogFactory();
                _initialized = true;
            }
        }
        public ILog GetLog(string name)
        {
            if (!_initialized)
                return new NullLog();
            else
                return new CommonLogWrapper(global::Common.Logging.LogManager.GetLogger(name));
        }
    }
}
#endif
