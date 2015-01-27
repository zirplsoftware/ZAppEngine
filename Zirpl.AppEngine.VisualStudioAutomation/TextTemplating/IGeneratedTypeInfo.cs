using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IGeneratedTypeInfo
    {
        String Namespace { get; }
        String TypeName { get; }
        String FullTypeName { get; }
    }
}
