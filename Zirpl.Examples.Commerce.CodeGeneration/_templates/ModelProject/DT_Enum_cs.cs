﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Zirpl.Examples.Commerce.CodeGeneration._templates.ModelProject
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Zirpl.AppEngine.VisualStudioAutomation;
    using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration;
    using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
    using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class DT_Enum_cs : Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.OncePerDomainTypeTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing Zirpl" +
                    ".AppEngine.Model;\r\nusing Zirpl.AppEngine.Model.Extensibility;\r\nusing Zirpl.Colle" +
                    "ctions;\r\n\r\nnamespace ");
            
            #line 17 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n\tpublic enum ");
            
            #line 19 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Name));
            
            #line default
            #line hidden
            this.Write("Enum : ");
            
            #line 19 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n");
            
            #line 21 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"

		foreach (var enumValue in this.DomainType.EnumValues)
		{

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 25 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(enumValue.Name));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 25 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(enumValue.Id));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 26 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"
	
		}

            
            #line default
            #line hidden
            this.Write("\t}\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 31 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\ModelProject\DT_Enum_cs.tt"

public override bool ShouldTransform { get { return this.DomainType.IsStaticLookup && this.DomainType.EnumValues.Any(); } }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
