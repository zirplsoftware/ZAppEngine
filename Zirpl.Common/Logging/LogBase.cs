#if !NET35CLIENT && !NET40CLIENT && !SILVERLIGHT
using System;
using System.Globalization;

namespace Zirpl.Logging
{
    /// <summary>
    /// Log class that wraps a Common.Logging.ILog object
    /// </summary>
    public abstract class LogBase :ILog
    {
        protected enum MessageType : byte
        {
            Debug,Info,Warn,Error,Fatal
        }

        protected abstract void WriteMessageToLog(MessageType messageType, object message, Exception exception = null);
        protected abstract bool IsLogEnabled(MessageType messageType);

        public virtual bool IsDebugEnabled
        {
            get { return this.IsLogEnabled(MessageType.Debug); }
        }

        public virtual void Debug(object message)
        {
            if (this.IsLogEnabled(MessageType.Debug))
            {
                this.WriteMessageToLog(MessageType.Debug, message);
            }
        }

        public virtual void Debug(Exception exception, object message)
        {
            if (this.IsLogEnabled(MessageType.Debug))
            {
                this.WriteMessageToLog(MessageType.Debug, message, exception);
            }
        }

        public virtual void DebugFormat(string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Debug))
            {
                this.WriteMessageToLog(MessageType.Debug, String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        public virtual void DebugFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Debug))
            {
                this.WriteMessageToLog(MessageType.Debug, String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        public virtual void TryDebug(object message)
        {
            try
            {
                this.Debug(message);
            }
            catch
            {
                
            }
        }

        public virtual void TryDebug(Exception exception, object message)
        {
            try
            {
                this.Debug(exception, message);
            }
            catch
            {

            }
        }

        public virtual void TryDebugFormat(string format, params object[] args)
        {
            try
            {
                this.DebugFormat(format, args);
            }
            catch
            {

            }
        }

        public virtual void TryDebugFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.DebugFormat(exception, format, args);
            }
            catch
            {

            }
        }









        public virtual bool IsInfoEnabled
        {
            get { return this.IsLogEnabled(MessageType.Info); }
        }

        public virtual void Info(object message)
        {
            if (this.IsLogEnabled(MessageType.Info))
            {
                this.WriteMessageToLog(MessageType.Info, message);
            }
        }

        public virtual void Info(Exception exception, object message)
        {
            if (this.IsLogEnabled(MessageType.Info))
            {
                this.WriteMessageToLog(MessageType.Info, message, exception);
            }
        }

        public virtual void InfoFormat(string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Info))
            {
                this.WriteMessageToLog(MessageType.Info, String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        public virtual void InfoFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Info))
            {
                this.WriteMessageToLog(MessageType.Info, String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        public virtual void TryInfo(object message)
        {
            try
            {
                this.Info(message);
            }
            catch
            {

            }
        }

        public virtual void TryInfo(Exception exception, object message)
        {
            try
            {
                this.Info(exception, message);
            }
            catch
            {

            }
        }

        public virtual void TryInfoFormat(string format, params object[] args)
        {
            try
            {
                this.InfoFormat(format, args);
            }
            catch
            {

            }
        }

        public virtual void TryInfoFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.InfoFormat(exception, format, args);
            }
            catch
            {

            }
        }





        public virtual bool IsWarnEnabled
        {
            get { return this.IsLogEnabled(MessageType.Warn); }
        }

        public virtual void Warn(object message)
        {
            if (this.IsLogEnabled(MessageType.Warn))
            {
                this.WriteMessageToLog(MessageType.Warn, message);
            }
        }

        public virtual void Warn(Exception exception, object message)
        {
            if (this.IsLogEnabled(MessageType.Warn))
            {
                this.WriteMessageToLog(MessageType.Warn, message, exception);
            }
        }

        public virtual void WarnFormat(string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Warn))
            {
                this.WriteMessageToLog(MessageType.Warn, String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        public virtual void WarnFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Warn))
            {
                this.WriteMessageToLog(MessageType.Warn, String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        public virtual void TryWarn(object message)
        {
            try
            {
                this.Warn(message);
            }
            catch
            {

            }
        }

        public virtual void TryWarn(Exception exception, object message)
        {
            try
            {
                this.Warn(exception, message);
            }
            catch
            {

            }
        }

        public virtual void TryWarnFormat(string format, params object[] args)
        {
            try
            {
                this.WarnFormat(format, args);
            }
            catch
            {

            }
        }

        public virtual void TryWarnFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.WarnFormat(exception, format, args);
            }
            catch
            {

            }
        }






        public virtual bool IsErrorEnabled
        {
            get { return this.IsLogEnabled(MessageType.Error); }
        }

        public virtual void Error(object message)
        {
            if (this.IsLogEnabled(MessageType.Error))
            {
                this.WriteMessageToLog(MessageType.Error, message);
            }
        }

        public virtual void Error(Exception exception, object message)
        {
            if (this.IsLogEnabled(MessageType.Error))
            {
                this.WriteMessageToLog(MessageType.Error, message, exception);
            }
        }

        public virtual void ErrorFormat(string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Error))
            {
                this.WriteMessageToLog(MessageType.Error, String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        public virtual void ErrorFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Error))
            {
                this.WriteMessageToLog(MessageType.Error, String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        public virtual void TryError(object message)
        {
            try
            {
                this.Error(message);
            }
            catch
            {

            }
        }

        public virtual void TryError(Exception exception, object message)
        {
            try
            {
                this.Error(exception, message);
            }
            catch
            {

            }
        }

        public virtual void TryErrorFormat(string format, params object[] args)
        {
            try
            {
                this.ErrorFormat(format, args);
            }
            catch
            {

            }
        }

        public virtual void TryErrorFormat(Exception exception, string format, params object[] args)
        {
            try
            {
                this.ErrorFormat(exception, format, args);
            }
            catch
            {

            }
        }




        public virtual bool IsFatalEnabled
        {
            get { return this.IsLogEnabled(MessageType.Fatal); }
        }

        public virtual void Fatal(object message)
        {
            if (this.IsLogEnabled(MessageType.Fatal))
            {
                this.WriteMessageToLog(MessageType.Fatal, message);
            }
        }

        public virtual void Fatal(Exception exception, object message)
        {
            if (this.IsLogEnabled(MessageType.Fatal))
            {
                this.WriteMessageToLog(MessageType.Fatal, message, exception);
            }
        }

        public virtual void FatalFormat(string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Fatal))
            {
                this.WriteMessageToLog(MessageType.Fatal, String.Format(CultureInfo.InvariantCulture, format, args));
            }
        }

        public virtual void FatalFormat(Exception exception, string format, params object[] args)
        {
            if (this.IsLogEnabled(MessageType.Fatal))
            {
                this.WriteMessageToLog(MessageType.Fatal, String.Format(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        public virtual void TryFatal(object message)
        {
            try
            {
                this.Fatal(message);
            }
            catch
            {

            }
        }

        public virtual void TryFatal(Exception exception, object message)
        {
            try
            {
                this.Fatal(exception, message);
            }
            catch
            {

            }
        }

        public virtual void TryFatalFormat(string format, params object[] args)
        {
            try
            {
                this.FatalFormat(format, args);
            }
            catch
            {

            }
        }

        public virtual void TryFatalFormat(Exception exception, string format, params object[] args)
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