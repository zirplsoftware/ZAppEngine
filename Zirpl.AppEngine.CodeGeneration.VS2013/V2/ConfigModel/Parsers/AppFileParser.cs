using System;
using System.IO;
using Newtonsoft.Json;
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
            app.DataContextName = json.DataContextName ?? "MyDataContext";
            app.GeneratedCSFileExtension = json.GeneratedCSFileExtension ?? ".auto.cs";
            app.GeneratedCodeRootFolderName = json.DataServiceProjectName ?? @"_auto\";

            // TODO: these can also have defaults based on the namespace of the CodeGeneration project
            app.DataServiceProjectName = json.DataServiceProjectName;
            app.DataServiceTestsProjectName = json.DataServiceTestsProjectName;
            app.ModelProjectName = json.ModelProjectName;
            app.ServiceProjectName = json.ServiceProjectName;
            app.TestsCommonProjectName = json.TestsCommonProjectName;
            app.WebCoreProjectName = json.WebCoreProjectName;
            app.WebProjectName = json.WebProjectName;
            app.Config = json;

            return app;
        }
    }
}
