using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.Collections;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal class TemplateProvider : ITemplateProvider
    {
        private readonly IList<Assembly> _assembliesToCheck;
        private readonly Assembly _thisAssembly;

        internal TemplateProvider(TextTransformation callingTemplate)
            : this(callingTemplate, new Assembly[0])
        {
        }

        internal TemplateProvider(TextTransformation callingTemplate, IEnumerable<String> templateAssemblyNames)
            : this(callingTemplate, 
                    from fileName in templateAssemblyNames
                   where AppDomain.CurrentDomain.GetAssemblies().Count(o => !o.IsDynamic && o.Location.Contains(fileName)) == 1
                   select AppDomain.CurrentDomain.GetAssemblies().Single(o => !o.IsDynamic && o.Location.Contains(fileName)))
        {
        }

        internal TemplateProvider(TextTransformation callingTemplate, IEnumerable<Assembly> templateAssemblies)
        {
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
                if (callingTemplate.GetProjectItem().ContainingProject
                    .GetAllProjectItems()
                    .Any(o => Path.GetExtension(o.GetFullPath()) == ".tt"))
                {
                    // running template project DOES have templates, let's compile and use it
                    //
                    callingTemplate.GetProjectItem().ContainingProject
                        .CompileCSharpProjectInMemory();
                }

                // okay, let's add all the assemblies currently in memory
                //
                _assembliesToCheck.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic));
            }
        }

        public IEnumerable<Type> GetTemplates(TextTransformation textTransformation)
        {

            // find ALL templates
            var allTemplates = from assembly in _assembliesToCheck
                               from o in assembly.GetTypes()
                               where IsAppTemplate(o)
                               orderby o.FullName
                               select o;

            // stock templates
            var defaultTemplates = from template in allTemplates
                                 where template.Assembly == _thisAssembly
                                 orderby template.FullName
                                 select template;

            // these are the templates that are additional or possibly replacements
            var otherTemplates = from template in allTemplates
                                    where !defaultTemplates.Contains(template)
                                    orderby template.FullName
                                    select template;

            // these are the stock templates that have been replaced
            var defaultTemplatesThatAreBeingReplaced = from template in defaultTemplates
                                                where otherTemplates.Any(o => o.FullName.SubstringAfterLastInstanceOf("_templates.").Equals(template.FullName.SubstringAfterLastInstanceOf("_templates."), StringComparison.InvariantCultureIgnoreCase))
                                                    || otherTemplates.Any(o => template.IsAssignableFrom(o))
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
                && o.GetTypeAccessor().HasMethod<String>("TransformText")
                && o.GetTypeAccessor().HasMethod("Initialize")
                && o.GetTypeAccessor().HasPropertyGetter<StringBuilder>("GenerationEnvironment")
                && o.GetTypeAccessor().HasPropertySetter<StringBuilder>("GenerationEnvironment")
                && o.GetTypeAccessor().HasPropertyGetter<IDictionary<String, Object>>("Session")
                && o.GetTypeAccessor().HasPropertySetter<IDictionary<String, Object>>("Session");
        }
    }
}
