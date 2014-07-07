using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class TransformationHelper :TransformationHelperBase
    {
        public AppDefinition AppDefinition { get; private set; }
        public DomainTypeFilters DomainTypeFilters { get; private set; }
        public ProjectProvider ProjectProvider { get; private set; }
        public CodeHelper CodeHelper { get; private set; }
        public FileHelper FileHelper { get; private set; }

        
        public TransformationHelper(TextTransformation callingTemplate)
            :base(callingTemplate)
        {
            this.DomainTypeFilters = new DomainTypeFilters(this);
            this.ProjectProvider = new ProjectProvider(this);
            this.CodeHelper = new CodeHelper(this);
            this.FileHelper = new FileHelper(this);
            
            this.LoadAppDefinition();
        }

        private void LoadAppDefinition()
        {
            var domaintTypeConfigFilePaths = new List<string>();
            String appDefinitionConfigFilePath = null;
            var modelConfigFileProjectItems = this.ProjectProvider.CodeGenerationProject.ProjectItems.GetAllProjectItemsRecursive();
            foreach (var modelConfigProjectItem in modelConfigFileProjectItems)
            {
                var fullPath = modelConfigProjectItem.GetFullPath();
                if (fullPath.EndsWith(".model.xml"))
                {
                    domaintTypeConfigFilePaths.Add(fullPath);
                }
                else if (fullPath.EndsWith(".app.xml"))
                {
                    appDefinitionConfigFilePath = fullPath;
                }
            }

            var xmlSerializer = new XmlSerializer(typeof(AppDefinition));
            using (var fileStream = new FileStream(appDefinitionConfigFilePath, FileMode.Open, FileAccess.Read))
            {
                this.AppDefinition = (AppDefinition)xmlSerializer.Deserialize(fileStream);
            }
            foreach (var path in domaintTypeConfigFilePaths)
            {
                DomainType domainType = null;
                xmlSerializer = new XmlSerializer(typeof(DomainType));
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    domainType = (DomainType)xmlSerializer.Deserialize(fileStream);
                }
                this.AppDefinition.DomainTypes.Add(domainType);
            }

        }
    }
}
