using System.Collections.Generic;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public class OutputFileProperties
    {
        public OutputFileProperties()
        {
            this.BuildAction = BuildActionTypeEnum.None;
            this.TemplateParameter = new Dictionary<string, string>();
        }

        public string CustomTool { get; set; }
        public BuildActionTypeEnum BuildAction { get; set; }
        public Dictionary<string, string> TemplateParameter { get; set; }

        internal string BuildActionString
        {
            get
            {
                switch (BuildAction)
                {
                    case BuildActionTypeEnum.Compile:
                        return "Compile";
                    case BuildActionTypeEnum.Content:
                        return "Content";
                    case BuildActionTypeEnum.EmbeddedResource:
                        return "EmbeddedResource";
                    case BuildActionTypeEnum.EntityDeploy:
                        return "EntityDeploy";
                    case BuildActionTypeEnum.None:
                    default:
                        return "None";
                }
            }
        }
    }
}
