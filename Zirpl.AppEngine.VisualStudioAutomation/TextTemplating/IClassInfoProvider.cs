using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IClassInfoProvider
    {
        String GetNamespace();
        String GetClassName();
    }
}
