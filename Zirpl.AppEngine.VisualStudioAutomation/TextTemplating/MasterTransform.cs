using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal class MasterTransform : TransformBase, IMasterTransform
    {
        internal MasterTransform(Object masterTemplate)
            : base(masterTemplate)
        {
            var session = this.Session;
            // initialize the master if not done already
            if (!session.ContainsKey("_Initialized"))
            {
                session.Add("IsMaster", true);
                session.Add("IsChild", false);
                session.Add("FileManager", new OutputFileManager(this));

                session.Add("_Initialized", true);
            }
            else if ((bool)session["IsChild"])
            {
                throw new ArgumentException("masterTransform is already a Child", "masterTemplate");
            }
        }

        public override IMasterTransform Master
        {
            get { return this; }
        }

        public override bool IsMaster
        {
            get { return true; }
        }

        public IOutputFileManager FileManager
        {
            get
            {
                return (IOutputFileManager)this.Session["FileManager"];
            }
        }
        
        public ITextTemplatingEngineHost Host
        {
            get
            {
                return this.Template.Access().Property<ITextTemplatingEngineHost>("Host");
            }
        }

        public ITransform GetChild(Object childTemplate)
        {
            if (childTemplate == null) throw new ArgumentNullException("childTemplate");

            return new ChildTransform(this, childTemplate);
        }

        internal static bool EvaluateIsMaster(Object template)
        {
            var accessor = template.Access();
            if (accessor.HasGet<IDictionary<String, Object>>("Session"))
            {
                var session = accessor.Property<IDictionary<String, Object>>("Session");
                if (session != null
                    && session.ContainsKey("IsMaster"))
                {
                    return (bool)session["IsMaster"];
                }
            }
            else
            {
                throw new ArgumentException("All Transforms must have a Session property", "template");
            }

            // okay, let's evaulate for the first time
            var fullName = template.GetType().FullName;
            return fullName.StartsWith("Microsoft.VisualStudio.TextTemplating.")
                   && fullName.EndsWith(".GeneratedTextTransformation")
                   && accessor.HasGet<ITextTemplatingEngineHost>("Host")
                   && accessor.Property<ITextTemplatingSession>("Host") != null;
        }
    }
}
