#if !NET35CLIENT && !NET40CLIENT
using System;
using System.Globalization;
using System.Reflection;

namespace Zirpl.AppEngine.Logging.Common
{
    /// <summary>
    /// Log class that wraps a Common.Logging.ILog object
    /// </summary>
    public class CommonLogWrapper :ILog
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


#region ILog Members

        /// <summary>
        /// Logs the message in Debug severity
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            this.Log.Debug(message);
        }

        /// <summary>
        /// Logs the message in Debug severity
        /// </summary>
        public void Debug(Exception exception, object message)
        {
            this.Log.Debug(message, exception);
        }

        /// <summary>
        /// Logs the message in Debug severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void DebugFormat(string format, params object[] args)
        {
            if (this.IsDebugEnabled)
            {
                this.Log.Debug(String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        /// <summary>
        /// Logs the message in Debug severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsDebugEnabled)
            {
                this.Log.Debug(String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// Logs the message in Info severity
        /// </summary>
        public void Info(object message)
        {
            this.Log.Info(message);
        }

        /// <summary>
        /// Logs the message in Info severity
        /// </summary>
        public void Info(Exception exception, object message)
        {
            this.Log.Info(message, exception);
        }

        /// <summary>
        /// Logs the message in Info severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void InfoFormat(string format, params object[] args)
        {
            if (this.IsInfoEnabled)
            {
                this.Log.Info(String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        /// <summary>
        /// Logs the message in Info severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsInfoEnabled)
            {
                this.Log.Info(String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// Logs the message in Warn severity
        /// </summary>
        public void Warn(object message)
        {
            this.Log.Warn(message);
        }

        /// <summary>
        /// Logs the message in Warn severity
        /// </summary>
        public void Warn(Exception exception, object message)
        {
            this.Log.Warn(message, exception);
        }

        /// <summary>
        /// Logs the message in Warn severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void WarnFormat(string format, params object[] args)
        {
            if (this.IsWarnEnabled)
            {
                this.Log.Warn(String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        /// <summary>
        /// Logs the message in Warn severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsWarnEnabled)
            {
                this.Log.Warn(String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// Logs the message in Error severity
        /// </summary>
        public void Error(object message)
        {
            this.Log.Error(message);
        }

        /// <summary>
        /// Logs the message in Error severity
        /// </summary>
        public void Error(Exception exception, object message)
        {
            this.Log.Error(message, exception);
        }

        /// <summary>
        /// Logs the message in Error severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void ErrorFormat(string format, params object[] args)
        {
            if (this.IsErrorEnabled)
            {
                this.Log.Error(String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        /// <summary>
        /// Logs the message in Error severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsErrorEnabled)
            {
                this.Log.Error(String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// Logs the message in Fatal severity
        /// </summary>
        public void Fatal(object message)
        {
            this.Log.Fatal(message);
        }

        /// <summary>
        /// Logs the message in Fatal severity
        /// </summary>
        public void Fatal(Exception exception, object message)
        {
            this.Log.Fatal(message, exception);
        }

        /// <summary>
        /// Logs the message in Fatal severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void FatalFormat(string format, params object[] args)
        {
            if (this.IsFatalEnabled)
            {
                this.Log.Fatal(String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        /// <summary>
        /// Logs the message in Fatal severity
        /// </summary>
        /// <param name="format">String to format</param>
        /// <param name="args">Args to format into the string</param>
        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsFatalEnabled)
            {
                this.Log.Fatal(String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// Gets whether or not Debug severity logging is enabled
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return this.Log.IsDebugEnabled; }
        }

        /// <summary>
        /// Gets whether or not Info severity logging is enabled
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return this.Log.IsInfoEnabled; }
        }

        /// <summary>
        /// Gets whether or not Warn severity logging is enabled
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return this.Log.IsWarnEnabled; }
        }

        /// <summary>
        /// Gets whether or not Error severity logging is enabled
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return this.Log.IsErrorEnabled; }
        }

        /// <summary>
        /// Gets whether or not Fatal severity logging is enabled
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return this.Log.IsFatalEnabled; }
        }

        #endregion


        public void TryDebug(object message)
        {
            try
            {
                this.Debug(message);
            }
            catch
            {
                
            }
        }

        public void TryDebug(Exception exception, object message)
        {
            try
            {
                this.Debug(exception, message);
            }
            catch
            {

            }
        }

        public void TryDebugFormat(string format, params object[] args)
        {
            try
            {
                this.DebugFormat(format, args);
            }
            catch
            {

            }
        }

        public void TryDebugFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.DebugFormat(exception, format, args);
            }
            catch
            {

            }
        }

        public void TryInfo(object message)
        {
            try
            {
                this.Info(message);
            }
            catch
            {

            }
        }

        public void TryInfo(Exception exception, object message)
        {
            try
            {
                this.Info(exception, message);
            }
            catch
            {

            }
        }

        public void TryInfoFormat(string format, params object[] args)
        {
            try
            {
                this.InfoFormat(format, args);
            }
            catch
            {

            }
        }

        public void TryInfoFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.InfoFormat(exception, format, args);
            }
            catch
            {

            }
        }

        public void TryWarn(object message)
        {
            try
            {
                this.Warn(message);
            }
            catch
            {

            }
        }

        public void TryWarn(Exception exception, object message)
        {
            try
            {
                this.Warn(exception, message);
            }
            catch
            {

            }
        }

        public void TryWarnFormat(string format, params object[] args)
        {
            try
            {
                this.WarnFormat(format, args);
            }
            catch
            {

            }
        }

        public void TryWarnFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.WarnFormat(exception, format, args);
            }
            catch
            {

            }
        }

        public void TryError(object message)
        {
            try
            {
                this.Error(message);
            }
            catch
            {

            }
        }

        public void TryError(Exception exception, object message)
        {
            try
            {
                this.Error(exception, message);
            }
            catch
            {

            }
        }

        public void TryErrorFormat(string format, params object[] args)
        {
            try
            {
                this.ErrorFormat(format, args);
            }
            catch
            {

            }
        }

        public void TryErrorFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.ErrorFormat(exception, format, args);
            }
            catch
            {

            }
        }

        public void TryFatal(object message)
        {
            try
            {
                this.Fatal(message);
            }
            catch
            {

            }
        }

        public void TryFatal(Exception exception, object message)
        {
            try
            {
                this.Fatal(exception, message);
            }
            catch
            {

            }
        }

        public void TryFatalFormat(string format, params object[] args)
        {
            try
            {
                this.FatalFormat(format, args);
            }
            catch
            {

            }
        }

        public void TryFatalFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.FatalFormat(exception, format, args);
            }
            catch
            {

            }
        }
    }
}

#endif