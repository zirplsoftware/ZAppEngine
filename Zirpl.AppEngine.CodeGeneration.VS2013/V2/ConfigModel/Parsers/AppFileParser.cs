using System;
using System.IO;
using EnvDTE;
using Newtonsoft.Json;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers.JsonModel;
using Zirpl.IO;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers
{
    public class AppFileParser
    {
        public AppInfo Parse(String path)
        {

            AppJson json = null;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var content = fileStream.ReadAllToString();
                json = JsonConvert.DeserializeObject<AppJson>(content);
            }
            var app = new AppInfo();
            app.ConfigFilePath = path;
            app.Config = json;
            app.DataContextName = json.DataContextName ?? "MyDataContext";
            app.GeneratedCSFileExtension = json.GeneratedCSFileExtension ?? ".auto.cs";
            app.GeneratedCodeRootFolderName = json.DataServiceProjectName ?? @"_auto\";

            var codeGenerationProjectName = TextTransformationSession.Instance.TemplateProjectItem.ContainingProject.GetDefaultNamespace();
            var defaultProjectNamespace = codeGenerationProjectName.SubstringUntilLastInstanceOf(".");

            this.LogLineToBuildPane(defaultProjectNamespace);

            app.DataServiceProjectName = json.DataServiceProjectName ?? defaultProjectNamespace + ".DataService";
            app.DataServiceTestsProjectName = json.DataServiceTestsProjectName ?? defaultProjectNamespace + ".Tests.DataService";
            app.ModelProjectName = json.ModelProjectName ?? defaultProjectNamespace + ".Model";
            app.ServiceProjectName = json.ServiceProjectName ?? defaultProjectNamespace + ".Service";
            app.TestsCommonProjectName = json.TestsCommonProjectName ?? defaultProjectNamespace + ".Tests.Common";
            app.WebCommonProjectName = json.WebCommonProjectName ?? defaultProjectNamespace + ".Web.Common";
            app.WebProjectName = json.WebProjectName ?? defaultProjectNamespace + ".Web";

            //this.LogLineToBuildPane(app.ModelProject.GetUniqueGuid().GetValueOrDefault().ToString());


            return app;
        }
    }
}
