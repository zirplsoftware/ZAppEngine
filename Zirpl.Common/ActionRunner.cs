using System;

namespace Zirpl
{
    public class ActionRunner
    {
        public Action Action { get; private set; }
        public Func<Exception, Exception> ErrorHandler { get; set; }
        public Action<bool> CompleteHandler { get; set; }

        public ActionRunner(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            this.Action = action;
        }

        public ActionRunner OnError(Func<Exception, Exception> action)
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
            Run(false);
        }

        public void Run()
        {
            Run(true);
        }

        private void Run(bool rethrow)
        {
            bool failed = false;
            Func<Exception, Exception> errorHandler = this.ErrorHandler;
            Action<Boolean> completeHandler = this.CompleteHandler;
            Action action = this.Action;

            try
            {
                action();
            }
            catch (Exception e)
            {
                failed = true;
                Exception newException = null;
                if (errorHandler != null)
                {
                    try
                    {
                        newException = errorHandler(e);
                    }
                    catch (Exception ex)
                    {
                        // nothing we can do about this- eat it
                    }
                }
                if (rethrow)
                {
                    if (newException != null
                        && newException != e)
                    {
                        throw newException;
                    }
                    else
                    {
                        throw;
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
