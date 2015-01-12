using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.FluentReflection;
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
            if (template.Property<IDictionary<String, Object>>("Session").Exists)
            {
                var session = template.Property<IDictionary<String, Object>>("Session").Value;
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
            return fullName.StartsWith("Microsoft.VisualStudio.TextTemplating")
                   && fullName.EndsWith(".GeneratedTextTransformation")
                   && template.Property<ITextTemplatingEngineHost>("Host").Exists
                   && template.Property<ITextTemplatingEngineHost>("Host").Value != null
                   && template.Method("TransformText").Exists
                   && template.Method("Initialize").Exists
                   && template.Property<StringBuilder>("GenerationEnvironment").Exists
                   && template.Property<IDictionary<String, Object>>("Session").Exists;
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
