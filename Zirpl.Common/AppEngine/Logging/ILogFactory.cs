using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.AppEngine.Logging
{
    public interface ILogFactory
    {
        ILog GetLog(String name);
    }
}
