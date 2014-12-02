using System;

namespace Zirpl.AppEngine.CodeGeneration.AppGeneration.ConfigModel.Parsers.JsonModel
{
    public class AppJson
    {
        public String GeneratedCodeRootFolderName { get; set; }
        public String GeneratedCSFileExtension { get; set; }
        public String ModelProjectName { get; set; }
        public String DataServiceProjectName { get; set; }
        public String ServiceProjectName { get; set; }
        public String WebProjectName { get; set; }
        public String WebCommonProjectName { get; set; }
        public String DataServiceTestsProjectName { get; set; }
        public String ServiceTestsProjectName { get; set; }
        public String TestsCommonProjectName { get; set; }
        public String DataContextName { get; set; }
    }
}
