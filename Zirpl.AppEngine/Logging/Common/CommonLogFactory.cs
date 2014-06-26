#if !NET35CLIENT && !NET40CLIENT && !SILVERLIGHT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;

namespace Zirpl.AppEngine.Logging.Common
{
    public class CommonLogFactory : ILogFactory
    {
        private static Boolean initialized;

        /// <summary>
        /// Initializes the LogManager
        /// </summary>
        static CommonLogFactory()
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
            initialized = true;
        }

        public ILog GetLog(string name)
        {
            if (!initialized)
                return new NullLog();
            else
                return new CommonLogWrapper(global::Common.Logging.LogManager.GetLogger(name));
        }
    }
}
#endif
