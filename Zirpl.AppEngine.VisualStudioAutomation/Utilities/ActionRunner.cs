using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.Utilities
{
    internal class ActionRunner
    {
        internal Action Action { get; private set; }
        internal Func<Exception, Exception> ErrorHandler { get; set; }
        internal Action<bool> CompleteHandler { get; set; }

        internal ActionRunner(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            this.Action = action;
        }

        internal ActionRunner OnError(Func<Exception, Exception> action)
        {
            this.ErrorHandler = action;
            return this;
        }
        internal ActionRunner OnComplete(Action<bool> action)
        {
            this.CompleteHandler = action;
            return this;
        }

        internal void TryRun()
        {
            Run(false);
        }

        internal void Run()
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
                    catch
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
                    catch
                    {
                        // nothing we can do about this- eat it
                    }
                }
            }
        }
    }
}
