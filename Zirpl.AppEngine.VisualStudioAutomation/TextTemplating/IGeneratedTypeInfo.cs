using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IGeneratedTypeInfo
    {
        String Namespace { get; }
        String TypeName { get; }
        String FullTypeName { get; }
    }
}
