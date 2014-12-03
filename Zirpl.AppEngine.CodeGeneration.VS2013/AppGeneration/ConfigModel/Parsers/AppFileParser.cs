using System;
using System.IO;
using Newtonsoft.Json;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel.Parsers.JsonModel;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
using Zirpl.IO;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.ConfigModel.Parsers
{
    public class AppFileParser
    {
        public App Parse(String path)
        {

            AppJson json = null;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var content = fileStream.ReadAllToString();
                json = JsonConvert.DeserializeObject<AppJson>(content);
            }
            var app = new App();
            app.ConfigFilePath = path;
            app.Config = json;
            app.DataContextName = json.DataContextName ?? "MyDataContext";
            app.GeneratedCSFileExtension = json.GeneratedCSFileExtension ?? ".auto.cs";
            app.GeneratedCodeRootFolderName = json.DataServiceProjectName ?? @"_auto\";

            var codeGenerationProjectName = TransformSession.Instance.TemplateProjectItem.ContainingProject.GetDefaultNamespace();
            var defaultProjectNamespace = codeGenerationProjectName.SubstringUntilLastInstanceOf(".");
            // TODO: create the projects if they don't exist
            app.DataServiceProjectName = json.DataServiceProjectName ?? defaultProjectNamespace + ".DataService";
            app.DataServiceTestsProjectName = json.DataServiceTestsProjectName ?? defaultProjectNamespace + ".Tests.DataService";
            app.ModelProjectName = json.ModelProjectName ?? defaultProjectNamespace + ".Model";
            app.ServiceProjectName = json.ServiceProjectName ?? defaultProjectNamespace + ".Service";
            app.TestsCommonProjectName = json.TestsCommonProjectName ?? defaultProjectNamespace + ".Tests.Common";
            app.WebCommonProjectName = json.WebCommonProjectName ?? defaultProjectNamespace + ".Web.Common";
            app.WebProjectName = json.WebProjectName ?? defaultProjectNamespace + ".Web";

            return app;
        }
    }
}
