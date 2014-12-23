using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal sealed class ChildTransform : TransformBase
    {
        private readonly ITransformHost _host;

        internal ChildTransform(ITransformHost host, Object childTemplate)
            : base(childTemplate)
        {
            if (host == null) throw new ArgumentNullException("childTemplate");

            this._host = host;

            var session = this.Session;
            // initialize the child if not done already
            if (!session.ContainsKey("_Initialized"))
            {
                session.Add("_IsMaster", false);
                session.Add("_IsChild", true);
                session.Add("_Master", this._host.HostTransform);
                session.Add("_Initialized", true);
            }
            else if ((bool)session["_IsMaster"])
            {
                throw new ArgumentException("childTemplate is already a Master", "childTemplate");
            }
        }

        internal static ITransform GetMasterFromSession(Object childTemplate)
        {
            var session = childTemplate.Access().Property<IDictionary<string, object>>("Session");
            if (session != null
                && session.ContainsKey("_Master"))
            {
                return (ITransform)session["_Master"];
            }
            return null;
        }

        public override ITransformHost Host
        {
            get { return this._host; }
        }

        public override IOutputFileManager FileManager
        {
            get { return this._host.HostTransform.FileManager; }
        }
    }
}
