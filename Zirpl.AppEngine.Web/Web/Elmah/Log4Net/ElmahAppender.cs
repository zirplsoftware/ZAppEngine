using System;
using System.Web;
using Elmah;

namespace Zirpl.AppEngine.Web.Elmah.Log4Net
{
    // logic taken from https://github.com/edwinf/log4net---ELMAH-Appender

    /// <summary>
    /// A log4net appender that logs directly to Elmah
    /// </summary>
    public class ElmahAppender : log4net.Appender.AppenderSkeleton
    {
        public string HostName { get; set; }
        public bool UseNullContext { get; set; }

        private ErrorSignal errorSignal;


        public override void ActivateOptions()
        {
            base.ActivateOptions();

            HostName = HostName ?? Environment.MachineName;
            try
            {
                this.errorSignal = UseNullContext || HttpContext.Current == null
                                    ? ErrorSignal.FromContext(null)
                                    : ErrorSignal.FromCurrentContext();
            }
            catch (Exception ex)
            {
                this.ErrorHandler.Error("Could not create default ELMAH error log", ex);
            }
        }


        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            if (this.errorSignal != null)
            {
                var detail = base.RenderLoggingEvent(loggingEvent);
                var newException = new Exception(detail, loggingEvent.ExceptionObject);
                this.errorSignal.Raise(newException);
            }
        }
    }
}
