using System;

namespace Zirpl.Logging
{
    public class NullLog : ILog
    {
        public void Debug(object message)
        {
        }

        public void Debug(Exception exception, object message)
        {
        }

        public void DebugFormat(string format, params object[] args)
        {
        }

        public void Info(object message)
        {
        }

        public void Info(Exception exception, object message)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public void Warn(object message)
        {
        }

        public void Warn(Exception exception, object message)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        public void Error(object message)
        {
        }

        public void Error(Exception exception, object message)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void Fatal(object message)
        {
        }

        public void Fatal(Exception exception, object message)
        {
        }

        public void FatalFormat(string format, params object[] args)
        {
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }


        public void DebugFormat(Exception exception, string format, params object[] args)
        {
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
        }


        public void TryDebug(object message)
        {
        }

        public void TryDebug(Exception exception, object message)
        {
        }

        public void TryDebugFormat(string format, params object[] args)
        {
        }

        public void TryDebugFormat(Exception exception, string format, params object[] args)
        {
        }

        public void TryInfo(object message)
        {
        }

        public void TryInfo(Exception exception, object message)
        {
        }

        public void TryInfoFormat(string format, params object[] args)
        {
        }

        public void TryInfoFormat(Exception exception, string format, params object[] args)
        {
        }

        public void TryWarn(object message)
        {
        }

        public void TryWarn(Exception exception, object message)
        {
        }

        public void TryWarnFormat(string format, params object[] args)
        {
        }

        public void TryWarnFormat(Exception exception, string format, params object[] args)
        {
        }

        public void TryError(object message)
        {
        }

        public void TryError(Exception exception, object message)
        {
        }

        public void TryErrorFormat(string format, params object[] args)
        {
        }

        public void TryErrorFormat(Exception exception, string format, params object[] args)
        {
        }

        public void TryFatal(object message)
        {
        }

        public void TryFatal(Exception exception, object message)
        {
        }

        public void TryFatalFormat(string format, params object[] args)
        {
        }

        public void TryFatalFormat(Exception exception, string format, params object[] args)
        {
        }
    }
}
