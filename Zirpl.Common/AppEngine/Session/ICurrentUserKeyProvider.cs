using System;

namespace Zirpl.AppEngine.Session
{
    public interface ICurrentUserKeyProvider
    {
        /// <summary>
        /// Gets the persistable ID of the current User
        /// </summary>
        /// <returns></returns>
        Object GetCurrentUserKey();
    }
}
