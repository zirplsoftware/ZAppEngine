﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.Collections;
using Zirpl.FluentReflection;
using Zirpl.Logging;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal sealed class TemplateProvider : ITemplateProvider
    {
        private readonly IList<Assembly> _assembliesToCheck;
        private readonly Assembly _thisAssembly;

        internal TemplateProvider(ITransform transform)
            : this(transform, new Assembly[0])
        {
        }

        internal TemplateProvider(ITransform transform, IEnumerable<String> templateAssemblyNames)
            : this(transform, 
                    from fileName in templateAssemblyNames
                   where AppDomain.CurrentDomain.GetAssemblies().Count(o => !o.IsDynamic && o.Location.Contains(fileName)) == 1
                   select AppDomain.CurrentDomain.GetAssemblies().Single(o => !o.IsDynamic && o.Location.Contains(fileName)))
        {
        }

        internal TemplateProvider(ITransform transform, IEnumerable<Assembly> templateAssemblies)
        {
            if (transform == null) throw new ArgumentNullException("transform");

            this._assembliesToCheck = new List<Assembly>();
            this._thisAssembly = this.GetType().Assembly;

            if (templateAssemblies != null
                && templateAssemblies.Any())
            {
                // if templateAssemblies were supplied, use them
                //
                _assembliesToCheck.AddRange(templateAssemblies);

                // add this assembly too
                //
                if (!_assembliesToCheck.Contains(_thisAssembly))
                {
                    _assembliesToCheck.Add(_thisAssembly);
                }
            }
            else
            {
                // if none were specified
                // then we should use all current assemblies
                // PLUS we should compile running template project into memory if it has templates
                if (transform.Host.GetProjectItem().ContainingProject
                    .GetAllProjectItems()
                    .Any(o => Path.GetExtension(o.GetFullPath()) == ".tt"))
                {
                    // running template project DOES have templates, let's compile and use it
                    //
                    transform.Host.GetProjectItem().ContainingProject
                        .CompileCSharpProjectInMemory();
                }

                // okay, let's add all the assemblies currently in memory
                //
                _assembliesToCheck.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic));
            }
        }

        internal IEnumerable<Type> GetDefaultTemplateTypes()
        {
            return from template in GetAllTemplateTypes()
                   where template.Assembly == _thisAssembly
                   orderby template.FullName
                   select template;
        }

        private IEnumerable<Type> GetAllTemplateTypes()
        {
            return from assembly in _assembliesToCheck
                               from o in assembly.GetTypes()
                               where IsAppTemplate(o)
                               orderby o.FullName
                               select o;
        }

        public IEnumerable<Type> GetTemplateTypes()
        {
            // find ALL templates
            var allTemplates = GetDefaultTemplateTypes();

            // stock templates
            var defaultTemplates = GetAllTemplateTypes();

            // these are the templates that are additional or possibly replacements
            var otherTemplates = from template in allTemplates
                                    where !defaultTemplates.Contains(template)
                                    orderby template.FullName
                                    select template;

            // these are the stock templates that have been replaced
            var defaultTemplatesThatAreBeingReplaced = from template in defaultTemplates
                                                where otherTemplates.Any(o => o.FullName.SubstringAfterLastInstanceOf("_templates.").Equals(template.FullName.SubstringAfterLastInstanceOf("_templates."), StringComparison.InvariantCultureIgnoreCase))
                                                    || otherTemplates.Any(template.IsAssignableFrom)
                                                select template;

            // these are the templates that are specifically REPLACING default templates
            var otherTemplatesThatAreReplacingDefaultTemplates = from template in otherTemplates
                                                where defaultTemplates.Any(d => d.FullName.SubstringAfterLastInstanceOf("_templates.").Equals(template.FullName.SubstringAfterLastInstanceOf("_templates."), StringComparison.InvariantCultureIgnoreCase))
                                                      || defaultTemplates.Any(d => d.IsAssignableFrom(template))
                                                select template;

            var templatesToRun = allTemplates.Where(o => !defaultTemplatesThatAreBeingReplaced.Contains(o));

            var loggingInfos = from o in templatesToRun
                                select new
                                {
                                    Name = o.FullName,
                                    Source = defaultTemplates.Contains(o) ? "Default" : (otherTemplatesThatAreReplacingDefaultTemplates.Contains(o) ? "Replacement" : "Additional")
                                };
            foreach (var loggingInfo in loggingInfos.OrderBy(o => o.Name))
            {
                this.GetLog().DebugFormat("Found template: {0} ({1})", loggingInfo.Name, loggingInfo.Source);
            }

            return templatesToRun;
        }
        
        private bool IsAppTemplate(Type o)
        {
            //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]

            return o.IsClass
                   && !o.IsAbstract
                   && o.Namespace != null
                   && o.Namespace.Contains("_templates")
                   && Attribute.GetCustomAttribute(o, typeof (GeneratedCodeAttribute)) != null
                   && o.Method("TransformText").Exists
                   && o.Method("Initialize").Exists
                   && o.Property("GenerationEnvironment").Exists
                   && o.Property("GenerationEnvironment").PropertyInfo.CanRead
                   && o.Property("GenerationEnvironment").PropertyInfo.CanWrite
                   && o.Property("Session").Exists
                   && o.Property("Session").PropertyInfo.CanRead
                   && o.Property("Session").PropertyInfo.CanWrite;
        }
    }
}
