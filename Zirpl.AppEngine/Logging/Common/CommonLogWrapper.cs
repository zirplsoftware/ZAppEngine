#if !NET35CLIENT && !NET40CLIENT && !SILVERLIGHT
using System;
using System.Globalization;
using System.Reflection;
using Zirpl.Logging;

namespace Zirpl.AppEngine.Logging.Common
{
    /// <summary>
    /// Log class that wraps a Common.Logging.ILog object
    /// </summary>
    public class CommonLogWrapper :LogBase
    {
        private static readonly FieldInfo declaringTypeFieldInfo;
        private static readonly Type newDeclaringType;

        static CommonLogWrapper()
        {
            // this is to ensure that locationInfo is correct
            //
            declaringTypeFieldInfo = typeof(global::Common.Logging.Log4Net.Log4NetLogger)
                .GetField("declaringType", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            newDeclaringType = typeof (CommonLogWrapper);
        }

        /// <summary>
        /// Creates a new CommonLogWrapper to wrap the provided ILog
        /// </summary>
        /// <param name="log"></param>
        public CommonLogWrapper(global::Common.Logging.ILog log)
        {
            this.Log = log;

            // this ensures that locationinfo is correct
            //
            if (log is global::Common.Logging.Log4Net.Log4NetLogger)
            {
                // take a look at static constructor for Log4NetLogger in Reflector.
                // that field is passed to log4net logger telling it where to start
                // looking for stack info.
                // we are changing that here to make sure it starts one level higher up
                //
                declaringTypeFieldInfo.SetValue(log, newDeclaringType);
            }
        }

        /// <summary>
        /// Gets or sets the ILog
        /// </summary>
        protected global::Common.Logging.ILog Log { get; private set; }

        protected override void WriteMessageToLog(LogBase.MessageType messageType, object message, Exception exception = null)
        {
            switch (messageType)
            {
                case MessageType.Debug:
                    if (exception != null)
                    {
                        this.Log.Debug(message, exception);
                    }
                    else
                    {
                        this.Log.Debug(message);   
                    }
                    break;
                case MessageType.Info:
                    if (exception != null)
                    {
                        this.Log.Info(message, exception);
                    }
                    else
                    {
                        this.Log.Info(message);
                    }
                    break;
                case MessageType.Warn:
                    if (exception != null)
                    {
                        this.Log.Warn(message, exception);
                    }
                    else
                    {
                        this.Log.Warn(message);
                    }
                    break;
                case MessageType.Error:
                    if (exception != null)
                    {
                        this.Log.Error(message, exception);
                    }
                    else
                    {
                        this.Log.Error(message);
                    }
                    break;
                case MessageType.Fatal:
                    if (exception != null)
                    {
                        this.Log.Fatal(message, exception);
                    }
                    else
                    {
                        this.Log.Fatal(message);
                    }
                    break;
            }
        }

        protected override bool IsLogEnabled(LogBase.MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Debug:
                    return this.Log.IsDebugEnabled;
                    break;
                case MessageType.Info:
                    return this.Log.IsInfoEnabled;
                    break;
                case MessageType.Warn:
                    return this.Log.IsWarnEnabled;
                    break;
                case MessageType.Error:
                    return this.Log.IsErrorEnabled;
                    break;
                case MessageType.Fatal:
                    return this.Log.IsFatalEnabled;
                    break;
            }
            return true;
        }
    }
}

#endif