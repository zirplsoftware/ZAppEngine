using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal class ChildTransform : TransformBase
    {
        private readonly IMasterTransform _masterTransform;

        internal ChildTransform(IMasterTransform masterTransform, Object childTemplate)
            : base(childTemplate)
        {
            if (masterTransform == null) throw new ArgumentNullException("masterTransform");

            this._masterTransform = masterTransform;

            var session = this.Session;
            // initialize the child if not done already
            if (!session.ContainsKey("_Initialized"))
            {
                foreach (var keyValuePair in this._masterTransform.Session)
                {
                    session.Add("Master_" + keyValuePair.Key, keyValuePair.Value);
                }
                session.Add("IsMaster", false);
                session.Add("IsChild", true);

                // this is for CONVENIENCE only
                session.Add("_Master", this._masterTransform);

                session.Add("_Initialized", true);
            }
            else if ((bool)session["IsMaster"])
            {
                throw new ArgumentException("childTemplate is already a Master", "childTemplate");
            }
        }

        internal static IMasterTransform GetMasterFromSession(Object childTemplate)
        {
            var session = childTemplate.Access().Property<IDictionary<string, object>>("Session");
            if (session != null
                && session.ContainsKey("_Master"))
            {
                return (IMasterTransform)session["_Master"];
            }
            return null;
        }

        public override bool IsMaster
        {
            get { return false; }
        }

        public override IMasterTransform Master
        {
            get { return this._masterTransform; }
        }
    }
}
