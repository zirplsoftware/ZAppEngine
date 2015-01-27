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
        public static IGeneratedTypeInfo GetGeneratedTypeInfo(this ITransform transform, Type templateType)
        {
            var template = transform.Host.HostTransform.GetChild(Activator.CreateInstance(templateType));
            foreach (var pair in transform.Session)
            {
                template.Session[pair.Key] = pair.Value;
            }
            template.Initialize();
            return new GeneratedTypeInfo(template);
        }
        public static IGeneratedTypeInfo GetGeneratedTypeInfo<T>(this ITransform transform) where T : class
        {
            return transform.GetGeneratedTypeInfo(typeof(T));
        }
    }
}
