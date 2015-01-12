using System;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class TemplateUtilities
    {
        // TODO: give this a look: http://www.olegsych.com/2008/04/t4-template-design-standalone-template/

        private static bool _initialized;

        public static ITransform AsTransform(this Object template)
        {
            if (template == null) throw new ArgumentNullException("template");

            if (MasterTransform.EvaluateIsMaster(template))
            {
                var masterTransform = new MasterTransform(template);
                if (!_initialized)
                {
                    LogFactory.Initialize((IServiceProvider)masterTransform.Host.Host);
                    _initialized = true;
                }
                return masterTransform;
            }
            // we have a child template
            var master = ChildTransform.GetMasterFromSession(template);
            if (master == null)
            {
                // it is not initialized, so we have a problem
                throw new Exception("An Host was not found in the Session. Call the following code from inside the main template to propertyly initialize the child template: 'this.AsTemplate().GetChild(childTemplate)'. Once initialized in this way, you can call 'this.AsTransform()' inside the child template.");
            }
            return master.GetChild(template);
        }
    }
}
