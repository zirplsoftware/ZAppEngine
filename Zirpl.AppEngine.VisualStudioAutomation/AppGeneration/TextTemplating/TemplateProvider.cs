using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.Collections;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    internal class TemplateProvider
    {
        private IList<Assembly> assembliesToCheck;
        private Assembly thisAssembly;

        internal TemplateProvider()
        {
            this.Initialize(null);
        }

        internal TemplateProvider(IEnumerable<Assembly> templateAssemblies)
        {
            this.Initialize(templateAssemblies);
        }

        internal TemplateProvider(IEnumerable<String> templateAssemblyNames)
        {
            var templateAssemblies = from fileName in templateAssemblyNames
                       where AppDomain.CurrentDomain.GetAssemblies().Count(o => !o.IsDynamic && o.Location.Contains(fileName)) == 1
                       select AppDomain.CurrentDomain.GetAssemblies().Single(o => !o.IsDynamic && o.Location.Contains(fileName));

            this.Initialize(templateAssemblies);
        }

        private void Initialize(IEnumerable<Assembly> providedAssemblies)
        {
            this.assembliesToCheck = new List<Assembly>();
            this.thisAssembly = this.GetType().Assembly;

            if (providedAssemblies != null
                && providedAssemblies.Any())
            {
                // if templateAssemblies were supplied, use them
                //
                assembliesToCheck.AddRange(providedAssemblies);

                // add this assembly too
                //
                if (!assembliesToCheck.Contains(thisAssembly))
                {
                    assembliesToCheck.Add(thisAssembly);
                }
            }
            else
            {
                // if none were specified
                // then we should use all current assemblies
                // PLUS we should compile running template project into memory if it has templates
                if (TextTransformationContext.Instance
                    .CallingTemplateProjectItem
                    .ContainingProject
                    .GetAllProjectItems()
                    .Any(o => Path.GetExtension(o.GetFullPath()) == ".tt"))
                {
                    // running template project DOES have templates, let's compile and use it
                    //
                    TextTransformationContext.Instance
                        .CallingTemplateProjectItem
                        .ContainingProject
                        .CompileCSharpProjectInMemory();
                }

                // okay, let's add all the assemblies currently in memory
                //
                assembliesToCheck.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic));
            }
        }

        internal IEnumerable<Type> GetTemplates()
        {

            // find ALL templates
            var allTemplates = from assembly in assembliesToCheck
                               from o in assembly.GetTypes()
                               where IsAppTemplate(o)
                               orderby o.FullName
                               select o;

            // stock templates
            var defaultTemplates = from template in allTemplates
                                 where template.Assembly == thisAssembly
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
                TextTransformationContext.Instance.LogLineToBuildPane(String.Format("Found template: {0} ({1})", loggingInfo.Name, loggingInfo.Source));
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
                //&& Attribute.GetCustomAttribute(o, typeof (GeneratedCodeAttribute)) != null
                && (typeof(OncePerAppTemplate).IsAssignableFrom(o)
                        || typeof(OncePerDomainTypeTemplate).IsAssignableFrom(o));
        }
    }
}
