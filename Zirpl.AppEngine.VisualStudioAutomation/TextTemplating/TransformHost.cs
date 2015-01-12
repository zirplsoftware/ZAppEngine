using Microsoft.VisualStudio.TextTemplating;
using Zirpl.FluentReflection;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal sealed class TransformHost : ITransformHost
    {
        private readonly ITransform _transform;

        internal TransformHost(MasterTransform transform)
        {
            this._transform = transform;
        }

        public ITextTemplatingEngineHost Host
        {
            get
            {
                return this._transform.Template.Property<ITextTemplatingEngineHost>("Host").Value;
            }
        }

        public ITransform HostTransform
        {
            get { return _transform; }
        }
    }
}
