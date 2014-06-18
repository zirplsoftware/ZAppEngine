using System;

namespace Zirpl.AppEngine
{
    public static class ActionExtensions
    {
        public static bool Try(this Action action, Action<Exception> errorHandler = null)
        {
            bool succeessful = false;

            try
            {
                action();
                succeessful = true;
            }
            catch (Exception e)
            {
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
            //finally
            //{
            //    if (completeHandler != null)
            //    {
            //        try
            //        {
            //            completeHandler(succeessful);
            //        }
            //        catch (Exception ex)
            //        {
            //            // nothing we can do about this- eat it
            //        }
            //    }
            //}
            return succeessful;
        }
    }
}
