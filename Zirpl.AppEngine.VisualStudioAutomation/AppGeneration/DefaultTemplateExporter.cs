using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.IO;
using Zirpl.Logging;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration
{
    internal sealed class DefaultTemplateExporter
    {
        internal void ExportDefaultTemplates(ITransform transform)
        {
            var defaultTemplates = new TemplateProvider(transform).GetDefaultTemplateTypes().ToArray();

            foreach (var defaultTemplate in defaultTemplates)
            {
                this.GetLog().DebugFormat("Found default template: {0}", defaultTemplate.FullName);
            }
            // every default template type should have a file as an embedded resource called TypeName.tt
            foreach (var type in defaultTemplates)
            {
                try
                {
                    var manifestResourcePath = type.FullName + ".tt";
                    var text = type.Assembly.GetManifestResourceText(manifestResourcePath);
                    var pathInCodeGenerationProject = type.Namespace.SubstringFromFirstInstanceOf("_templates").Replace(".", "\\") + "\\" + type.Name + ".tt";
                    var fullPath = Path.GetDirectoryName(transform.Host.GetProjectItem().ContainingProject.FullName) + "\\" +
                        pathInCodeGenerationProject;
                    PathUtilities.EnsureDirectoryExists(fullPath);
                    if (!File.Exists(fullPath))
                    {
                        this.GetLog().Debug("Writing: " + fullPath);
                        File.WriteAllText(fullPath, text);
                        transform.Host.GetProjectItem().ContainingProject.ProjectItems.AddFromFile(fullPath);
                        //transform.Host.GetProjectItem().ContainingProject.Save();
                        //var newProjectItem = transform.Host.GetProjectItem().ContainingProject.GetProjectItem(fullPath);

                        //var project = (Project)((Array)(transform.GetDTE().ActiveSolutionProjects)).GetValue(0);
                        //project.Save();
                        //System.Threading.Thread.Sleep(1000);
                        //newProjectItem.Save();
                        //newProjectItem.SetPropertyValue("CustomTool", "TextTemplatingFilePreprocessor");
                        //newProjectItem.Save();
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }
    }
}
