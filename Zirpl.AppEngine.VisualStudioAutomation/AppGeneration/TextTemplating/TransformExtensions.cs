using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public static class TransformExtensions
    {
        public static OutputInfo GetGeneratedTypeInfo(this ITransform transform, Type templateType)
        {
            var childTransform = transform.Host.HostTransform.GetChild(Activator.CreateInstance(templateType));
            foreach (var pair in transform.Session)
            {
                childTransform.Session[pair.Key] = pair.Value;
            }
            childTransform.Initialize();
            return transform.OutputInfoProvider.GetOutputInfo(childTransform);
        }
        public static OutputInfo GetGeneratedTypeInfo<T>(this ITransform transform) where T : class
        {
            return transform.GetGeneratedTypeInfo(typeof(T));
        }
    }
}
