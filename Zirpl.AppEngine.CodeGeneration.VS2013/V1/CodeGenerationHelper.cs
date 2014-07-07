using System;
using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;

namespace Zirpl.AppEngine.CodeGeneration.V1
{
    public class CodeGenerationHelper
    {
        private TransformationHelper templateHelper;

        public CodeGenerationHelper(TransformationHelper templateHelper)
        {
            this.templateHelper = templateHelper;
        }

        public string GetKendoUIModelDeclarationForProperty(Property property, string returnValue = "")
        {
            if (property.IsRelationship)
            {
                var type = this.templateHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(property.Type);
                if (type.WebOptions.GenerateSupportIndexView)
                {
                    returnValue = String.Format("{0}{1}: {{   }}", returnValue, property.Name);
                    foreach (var prop in type.Properties)
                    {
                        //returnValue = this.GetKendoUIModelDeclarationForProperty(prop, returnValue) + ",";
                    }
                }
            }
            else
            {
                var propertyType = this.GetKendoUIModelPropertyType(property.Type);
                returnValue = String.Format("{0}{1}:  {{ type: {3}\"{2}{3}\", editable: false }}", returnValue, property.Name, propertyType, @"\");
            }
            return returnValue;
        }
        private string GetKendoUIModelPropertyType(string propertyTypeName)
        {
            var propertyType = propertyTypeName.ToLowerInvariant();
            switch (propertyType)
            {
                case "currency":
                case "decimal":
                case "int":
                case "double":
                case "byte":
                    propertyType = "number";
                    break;
                case "datetime":
                    propertyType = "date";
                    break;
                case "bool":
                    propertyType = "boolean";
                    break;
                case "guid":
                    propertyType = "string";
                    break;
            }
            return propertyType;
        }
    }
}
