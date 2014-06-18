using System;

namespace Zirpl.AppEngine
{
    public class ActionRunner
    {
        public Action Action { get; set; }
        public Action<Exception> ErrorHandler { get; set; }
        public Action<bool> CompleteHandler { get; set; }

        public void TryAction()
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
