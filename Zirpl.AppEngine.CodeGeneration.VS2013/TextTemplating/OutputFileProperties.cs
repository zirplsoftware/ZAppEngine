using System.Collections.Generic;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public class OutputFileProperties
    {
        public OutputFileProperties()
        {
            this.BuildAction = OutputFileBuildActionType.None;
            this.TemplateParameter = new Dictionary<string, string>();
        }

        public string CustomTool { get; set; }
        public OutputFileBuildActionType BuildAction { get; set; }
        public Dictionary<string, string> TemplateParameter { get; set; }

        internal string BuildActionString
        {
            get
            {
                switch (BuildAction)
                {
                    case OutputFileBuildActionType.Compile:
                        return "Compile";
                    case OutputFileBuildActionType.Content:
                        return "Content";
                    case OutputFileBuildActionType.EmbeddedResource:
                        return "EmbeddedResource";
                    case OutputFileBuildActionType.EntityDeploy:
                        return "EntityDeploy";
                    case OutputFileBuildActionType.None:
                    default:
                        return "None";
                }
            }
        }
    }
}
