﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Zirpl.AppEngine.CodeGeneration.V1.Templates
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using Zirpl.AppEngine.CodeGeneration;
    using Zirpl.AppEngine.CodeGeneration.TextTemplating;
    using Zirpl.AppEngine.CodeGeneration.V1;
    using Zirpl.AppEngine.CodeGeneration.V1.ConfigModel;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class EntityFrameworkMappingTemplate : EntityFrameworkMappingTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 18 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"


	// Generate Mapping classes
	//
	foreach (DomainType domainType in this.TransformationHelper.DomainTypeFilters.DomainTypesToGenerateEntityFrameworkMappingFor)
	{
		this.TransformationHelper.FileHelper.StartEntityFrameworkMappingFile(domainType);
	

            
            #line default
            #line hidden
            this.Write("using System;\r\nusing System.Linq;\r\nusing Zirpl.AppEngine.DataService;\r\nusing Zirp" +
                    "l.AppEngine.DataService.EntityFramework;\r\nusing Zirpl.AppEngine.DataService.Enti" +
                    "tyFramework.Mapping;\r\nusing ");
            
            #line 32 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TransformationHelper.CodeHelper.GetModelNamespace(domainType)));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 34 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TransformationHelper.CodeHelper.GetEntityFrameworkMappingNamespace(domainType)));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public partial class ");
            
            #line 36 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TransformationHelper.CodeHelper.GetEntityFrameworkMappingTypeName(domainType)));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 36 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TransformationHelper.CodeHelper.GetEntityFrameworkMappingBaseDeclaration(domainType)));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n\t\tprotected override void MapProperties()\r\n        {\r\n\r\n");
            
            #line 41 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"

		foreach (Property property in domainType.Properties)
		{	
			if (property.IsCollection
				|| !property.MapProperty)
			{
				// ignore if collection or not supposed to map
			}
			else if (property.IsRelationship)
			{
				if (property.GenerateIdProperty)
                {
					var navigationPropertyString = String.IsNullOrEmpty(property.NavigationProperty) ? "" : ",\r\n										o => o." + property.NavigationProperty;

            
            #line default
            #line hidden
            this.Write("\r\n            this.HasNavigationProperty(o => o.");
            
            #line 56 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(",\r\n                                        o => o.");
            
            #line 57 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("Id,\r\n                                        ");
            
            #line 58 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 58 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsRequired,\r\n                                        CascadeOnDeleteOption.");
            
            #line 59 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.CascadeOnDelete ? "Yes" : "No"));
            
            #line default
            #line hidden
            
            #line 59 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(navigationPropertyString));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 60 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				
                }
			}
			else 
			{
				if (property.Type.ToLowerInvariant() == "string")
                {

            
            #line default
            #line hidden
            this.Write("\t\t\tthis.Property(o => o.");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(").IsRequired(");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsRequired).HasMaxLength(");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_MaxLength, ");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 68 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsMaxLength);\r\n");
            
            #line 69 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				}
				else if (property.Type.ToLowerInvariant() == "decimal"
						|| property.Type.ToLowerInvariant() == "double")
                {
					 var precisionString = String.IsNullOrEmpty(property.Precision) ? "" : ".HasPrecision(" + property.Precision + ")";

            
            #line default
            #line hidden
            this.Write("\t\t\tthis.Property(o => o.");
            
            #line 75 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(").IsRequired(");
            
            #line 75 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 75 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsRequired)");
            
            #line 75 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(precisionString));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 76 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				
                }
				else if (property.Type.ToLowerInvariant() == "datetime")
                {

            
            #line default
            #line hidden
            this.Write("\t\t\tthis.Property(o => o.");
            
            #line 81 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(").IsRequired(");
            
            #line 81 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 81 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsRequired).IsDateTime();\r\n");
            
            #line 82 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				
                }
				else if (property.Type.ToLowerInvariant() == "currency")
                {

            
            #line default
            #line hidden
            this.Write("\t\t\tthis.Property(o => o.");
            
            #line 87 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(").IsRequired(");
            
            #line 87 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 87 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsRequired).IsCurrency();\r\n");
            
            #line 88 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				
                }
				else if (property.Type.ToLowerInvariant() == "bool"
						|| property.Type.ToLowerInvariant() == "guid"
						|| property.Type.ToLowerInvariant() == "int"
						|| property.Type.ToLowerInvariant() == "byte")
                {

            
            #line default
            #line hidden
            this.Write("\t\t\tthis.Property(o => o.");
            
            #line 96 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(").IsRequired(");
            
            #line 96 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(domainType.Name));
            
            #line default
            #line hidden
            this.Write("Metadata.");
            
            #line 96 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("_IsRequired);\r\n");
            
            #line 97 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				
                }
				else 
				{
					throw new NotImplementedException("Unexpected property type: " + property.Type);
                }
			}
		}

            
            #line default
            #line hidden
            this.Write("\r\n\t\t\tthis.MapCustomProperties();\r\n\r\n            base.MapProperties();\r\n        }\r" +
                    "\n\t\t\r\n\t\tpartial void MapCustomProperties();\r\n\t\t\r\n");
            
            #line 114 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"
				
		// if the base class is one of the domain types, then we need to NOT map the core entity properties
		//
        if (!String.IsNullOrEmpty(domainType.BaseClassOverride)
			&& this.TransformationHelper.DomainTypeFilters.GetDomainTypeByFullTypeName(domainType.BaseClassOverride) != null)
        {

            
            #line default
            #line hidden
            this.Write("        protected override bool MapCoreEntityBaseProperties\r\n        {\r\n         " +
                    "   get\r\n            {\r\n                return false;\r\n            }\r\n        }\r\n" +
                    "");
            
            #line 128 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"

        }

            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
            
            #line 133 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V1\Templates\EntityFrameworkMappingTemplate.tt"


	}



            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        private global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost hostValue;
        /// <summary>
        /// The current host for the text templating engine
        /// </summary>
        public virtual global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost Host
        {
            get
            {
                return this.hostValue;
            }
            set
            {
                this.hostValue = value;
            }
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class EntityFrameworkMappingTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
