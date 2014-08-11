using System;

namespace Zirpl.Testing
{
    public static class AssertionUtilities
    {
        public static TException AssertThrowsException<TException>(this Object context, Action action) where TException : Exception
        {
            Exception exceptionThrown = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex is TException)
                {
                    exceptionThrown = ex;
                }
                else
                {
                    throw new Exception("Exception thrown but not of correct type: " + ex.GetType().Name);
                }
            }

            if (exceptionThrown == null)
            {
                throw new Exception("Exception not thrown");
            }

            return (TException)exceptionThrown;
        }
    }
}
