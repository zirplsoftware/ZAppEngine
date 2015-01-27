﻿using System;
using System.Linq;
using EnvDTE80;
using Zirpl.AppEngine.AppGeneration;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing
{
    internal sealed class AppProvider
    {
        private readonly DTE2 _visualStudio;
        //private readonly Project _callingTemplateProject;

        internal AppProvider(ITransform transform)
        {
            if (transform == null) throw new ArgumentNullException("transform");

            this._visualStudio = transform.GetDTE();
            //this._callingTemplateProject = transform.Host.GetProjectItem().ContainingProject;
        }
        
        internal App GetApp()
        {
            // set all of the settings defaults
            //
            //var projectNamespacePrefix = this._callingTemplateProject.GetDefaultNamespace()
            //                                        .SubstringUntilLastInstanceOf(".");

            // create the app
            //
            var app = new App();
            //{
            //    CodeGenerationProjectIndex = this._callingTemplateProject.GetIndex(),
            //    ModelProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Model").GetIndex(),
            //    DataServiceProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".DataService").GetIndex(),
            //    ServiceProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Service").GetIndex(),
            //    WebCommonProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Web.Common").GetIndex(),
            //    WebProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Web").GetIndex(),
            //    TestsCommonProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.Common").GetIndex(),
            //    DataServiceTestsProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.DataService").GetIndex(),
            //    ServiceTestsProjectIndex = _visualStudio.Solution.GetProject(projectNamespacePrefix + ".Tests.Service").GetIndex(),
            //};

            // create all of the domain types
            //
            var projectItems = _visualStudio.Solution.GetAllProjectItems();
            var paths = (from p in projectItems
                        where p.GetFullPath().ToLowerInvariant().EndsWith(".domain.zae")
                            && p.GetFullPath().ToLowerInvariant().Contains("_config")
                        select p.GetFullPath()).ToArray();
            
            this.GetLog().Debug("Parsing domain files:");
            foreach (var path in paths)
            {
                this.GetLog().Debug("   " + path);
            }

            new DomainFileParser(_visualStudio).ParseDomainTypes(app, paths);

            this.GetLog().Debug("Resulting domain types:");
            foreach (var domainType in app.DomainTypes.OrderBy(o => o.FullName))
            {
                this.GetLog().Debug("   " + domainType.FullName);
            }
            return app;
        }
    }
}