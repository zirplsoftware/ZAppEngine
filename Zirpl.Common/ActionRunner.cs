using System;

namespace Zirpl
{
    public class ActionRunner
    {
        public Action Action { get; private set; }
        public Action<Exception> ErrorHandler { get; set; }
        public Action<bool> CompleteHandler { get; set; }

        public ActionRunner(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            this.Action = action;
        }

        public ActionRunner OnError(Action<Exception> action)
        {
            this.ErrorHandler = action;
            return this;
        }
        public ActionRunner OnComplete(Action<bool> action)
        {
            this.CompleteHandler = action;
            return this;
        }

        public void TryRun()
        {
            bool failed = false;
            Action<Exception> errorHandler = this.ErrorHandler;
            Action<Boolean> completeHandler = this.CompleteHandler;
            Action action = this.Action;

            try
            {
                action();
            }
            catch (Exception e)
            {
                failed = true;
                if (errorHandler != null)
                {
                    try
                    {
                        errorHandler(e);
                    }
                    catch (Exception ex)
                    {
                        // nothing we can do about this- eat it
                    }
                }
            }
            finally
            {
                if (completeHandler != null)
                {
                    try
                    {
                        completeHandler(!failed);
                    }
                    catch (Exception ex)
                    {
                        // nothing we can do about this- eat it
                    }
                }
            }
        }
    }
}
