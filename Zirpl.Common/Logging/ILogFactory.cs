using System;

namespace Zirpl.Logging
{
    public interface ILogFactory
    {
        ILog GetLog(String name);
    }
}
