using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    LogFactory.Initialize((IServiceProvider)masterTransform.Host);
                    _initialized = true;
                }
                return masterTransform;
            }
            // we have a child template
            var master = ChildTransform.GetMasterFromSession(template);
            if (master == null)
            {
                // it is not initialized, so we have a problem
                throw new Exception("An IMasterTransform was not found in the Session. Call InitializeChild() from the IMasterTransform. You only need to use that method once, and the Master will be retrievable from the child after that.");
            }
            return master.GetChild(template);
        }
    }
}
