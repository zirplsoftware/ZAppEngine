﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Zirpl.Examples.Commerce.CodeGeneration._templates
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
    
    #line 1 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class DT_cs : Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating.OncePerDomainTypeTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing Zirpl" +
                    ".AppEngine.Model;\r\nusing Zirpl.AppEngine.Model.Metadata;\r\nusing Zirpl.AppEngine." +
                    "Model.Extensibility;\r\nusing Zirpl.Collections;\r\n\r\nnamespace ");
            
            #line 18 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n\tpublic ");
            
            #line 20 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IsAbstract ? "abstract" : ""));
            
            #line default
            #line hidden
            this.Write(" partial class ");
            
            #line 20 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Name));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 20 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.InheritsFrom != null ? this.DomainType.InheritsFrom.FullName : "System.Object"));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t, IMetadataDescribed\r\n");
            
            #line 22 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		if (this.DomainType.IsPersistable
                && (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsPersistable))
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t, IPersistable<");
            
            #line 28 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write(">\r\n");
            
            #line 29 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        } 
		if (this.DomainType.IsAuditable
                && (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsAuditable))
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t, IAuditable\r\n");
            
            #line 37 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        } 
		if (this.DomainType.IsExtensible
                && (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsExtensible))
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t, IExtensible<");
            
            #line 44 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Name));
            
            #line default
            #line hidden
            this.Write(",");
            
            #line 44 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.ExtendedBy.Name));
            
            #line default
            #line hidden
            this.Write(",");
            
            #line 44 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write(">\r\n");
            
            #line 45 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        }
		if (this.DomainType.IsExtendedEntityFieldValue
                && (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsExtendedEntityFieldValue))
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t, IExtendedEntityFieldValue<");
            
            #line 52 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Name));
            
            #line default
            #line hidden
            this.Write(",");
            
            #line 52 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Extends.Name));
            
            #line default
            #line hidden
            this.Write(",");
            
            #line 52 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write(">\r\n");
            
            #line 53 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        }
		if (this.DomainType.IsMarkDeletable
                && (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsMarkDeletable))
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t, IsMarkDeletable\r\n");
            
            #line 61 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        }
		if (this.DomainType.IsStaticLookup
                && (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsStaticLookup))
        {

            
            #line default
            #line hidden
            this.Write("\t\t\t, IStaticLookup\r\n\t\t\t, IEnumDescribed<");
            
            #line 69 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write(",");
            
            #line 69 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.FullName));
            
            #line default
            #line hidden
            this.Write("Enum>\r\n");
            
            #line 70 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        }

            
            #line default
            #line hidden
            this.Write("\t{\r\n");
            
            #line 74 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		if (this.DomainType.Properties.GetCollectionProperties().Any())
        {

            
            #line default
            #line hidden
            this.Write("\t\t");
            
            #line 78 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IsAbstract ? "protected" : "public"));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 78 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Name));
            
            #line default
            #line hidden
            this.Write("()\r\n\t\t{\r\n");
            
            #line 80 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

			foreach (var collectionProperty in this.DomainType.Properties.GetCollectionProperties())
            {

            
            #line default
            #line hidden
            this.Write("\t\t\t\tthis.");
            
            #line 84 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(collectionProperty.Name));
            
            #line default
            #line hidden
            this.Write(" = this.");
            
            #line 84 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(collectionProperty.Name));
            
            #line default
            #line hidden
            this.Write(" ?? new ");
            
            #line 84 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(collectionProperty.InitializationDataTypeString));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 85 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

            }

            
            #line default
            #line hidden
            this.Write("\t\t}\r\n");
            
            #line 89 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

        }
		foreach (var property in this.DomainType.Properties)
		{

            
            #line default
            #line hidden
            this.Write("\t\tpublic virtual ");
            
            #line 94 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.DataTypeString));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 94 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 95 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		}

            
            #line default
            #line hidden
            this.Write("\r\n\t\t#region Interface implementations\r\n\r\n");
            
            #line 101 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		if (this.DomainType.IsPersistable
			&& (this.DomainType.InheritsFrom == null
				|| !this.DomainType.InheritsFrom.IsPersistable))
		{

            
            #line default
            #line hidden
            this.Write("\t\tpublic virtual Object GetId()\r\n        {\r\n            return Id;\r\n        }\r\n\r\n" +
                    "        public virtual void SetId(Object id)\r\n        {\r\n            Id = (");
            
            #line 114 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write(@")id;
        }

        public virtual bool IsPersisted
        {
            get { return this.EvaluateIsPersisted(); }
        }

		public override bool Equals(object other)
        {
            return this.EvaluateEquals(other);
        }

        public override int GetHashCode()
        {
            return this.EvaluateGetHashCode();
        }
");
            
            #line 131 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		}
		
		if (this.DomainType.IsExtensible
				&& (this.DomainType.InheritsFrom == null
					|| !this.DomainType.InheritsFrom.IsExtensible))
		{

            
            #line default
            #line hidden
            this.Write(@"		public virtual IList<IExtendedEntityFieldValue> GetExtendedFieldValues()
		{
            return this.ExtendedFieldValues.Cast<IExtendedEntityFieldValue>().ToList();
		}
        public virtual void SetExtendedFieldValues(IList<IExtendedEntityFieldValue> list)
		{
            this.ExtendedFieldValues.Clear();
            this.ExtendedFieldValues.AddRange(list.Cast<");
            
            #line 146 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.ExtendedBy.FullName));
            
            #line default
            #line hidden
            this.Write(">());\r\n\t\t}\r\n");
            
            #line 148 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		}

		if (this.DomainType.IsExtendedEntityFieldValue
			&& (this.DomainType.InheritsFrom == null
			|| !this.DomainType.InheritsFrom.IsExtendedEntityFieldValue))
		{

            
            #line default
            #line hidden
            this.Write(@"        public virtual object GetExtendedEntityId()
        {
            return this.ExtendedEntityId;
        }

        public virtual IExtensible GetExtendedEntity()
        {
            return this.ExtendedEntity;
        }

        public virtual void SetExtendedEntityId(object id)
        {
            this.ExtendedEntityId = (");
            
            #line 168 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.IdProperty.DataTypeString));
            
            #line default
            #line hidden
            this.Write(")id;\r\n        }\r\n\r\n        public virtual void SetExtendedEntity(IExtensible enti" +
                    "ty)\r\n        {\r\n            this.ExtendedEntity = (");
            
            #line 173 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.DomainType.Extends.FullName));
            
            #line default
            #line hidden
            this.Write(")entity;\r\n        }\r\n\t\t\r\n");
            
            #line 176 "E:\projects\ZAppEngine\Zirpl.Examples.Commerce.CodeGeneration\_templates\DT_cs.tt"

		}

            
            #line default
            #line hidden
            this.Write("\t\t#endregion\r\n\t}\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
