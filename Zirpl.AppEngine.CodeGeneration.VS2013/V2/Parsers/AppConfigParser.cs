using System;
using System.IO;
using EnvDTE;
using Newtonsoft.Json;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.V2.Parsers.JsonModel;
using Zirpl.IO;

namespace Zirpl.AppEngine.CodeGeneration.V2.Parsers
{
    public class AppConfigParser
    {
        public AppConfig Parse(ProjectItem appConfigProjectItem)
        {
            var fullPath = appConfigProjectItem.GetFullPath();

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new Exception("An App config file with extension .app.zae must be present");
            }

            AppConfigJson json = null;
            using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                var content = fileStream.ReadAllToString();
                json = JsonConvert.DeserializeObject<AppConfigJson>(content);
            }
            var appConfig = new AppConfig();
            appConfig.SourceProjectItem = appConfigProjectItem;
            appConfig.DataContextName = json.DataContextName ?? "MyDataContext";
            appConfig.GeneratedCSFileExtension = json.GeneratedCSFileExtension ?? ".auto.cs";
            appConfig.GeneratedCodeRootFolderName = json.DataServiceProjectName ?? @"_auto\";

            // TODO: these can also have defaults based on the namespace of the CodeGeneration project
            appConfig.DataServiceProjectName = json.DataServiceProjectName;
            appConfig.DataServiceTestsProjectName = json.DataServiceTestsProjectName;
            appConfig.ModelProjectName = json.ModelProjectName;
            appConfig.ServiceProjectName = json.ServiceProjectName;
            appConfig.TestsCommonProjectName = json.TestsCommonProjectName;
            appConfig.WebCoreProjectName = json.WebCoreProjectName;
            appConfig.WebProjectName = json.WebProjectName;

            return appConfig;
        }
    }
}
