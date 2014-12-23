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
    internal sealed class MasterTransform : TransformBase
    {
        private readonly ITransformHost _host;

        internal MasterTransform(Object masterTemplate)
            : base(masterTemplate)
        {
            this._host = new TransformHost(this);

            var session = this.Session;
            // initialize the master if not done already
            if (!session.ContainsKey("_Initialized"))
            {
                session.Add("_IsMaster", true);
                session.Add("_IsChild", false);
                session.Add("_FileManager", new OutputFileManager(this));
                session.Add("_Initialized", true);
            }
            else if ((bool)session["_IsChild"])
            {
                throw new ArgumentException("masterTransform is already a Child", "masterTemplate");
            }
        }

        internal static bool EvaluateIsMaster(Object template)
        {
            var accessor = template.Access();
            if (accessor.HasGet<IDictionary<String, Object>>("Session"))
            {
                var session = accessor.Property<IDictionary<String, Object>>("Session");
                if (session != null
                    && session.ContainsKey("_IsMaster"))
                {
                    return (bool)session["_IsMaster"];
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

        public override ITransformHost Host
        {
            get { return this._host; }
        }
        public override IOutputFileManager FileManager
        {
            get
            {
                return (IOutputFileManager)this.Session["_FileManager"];
            }
        }
    }
}
