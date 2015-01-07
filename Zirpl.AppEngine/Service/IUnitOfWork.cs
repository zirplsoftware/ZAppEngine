using System;

namespace Zirpl.AppEngine.Service
{
    public interface IUnitOfWork : IDisposable
    {
        void Flush();
        void Commit();
    }
}
