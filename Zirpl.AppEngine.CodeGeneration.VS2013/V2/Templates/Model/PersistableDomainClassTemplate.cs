﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Zirpl.AppEngine.CodeGeneration.V2.Templates.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using Newtonsoft.Json;
    using Zirpl.AppEngine.Model;
    using Zirpl.AppEngine.Model.Metadata;
    using Zirpl.AppEngine.CodeGeneration;
    using Zirpl.AppEngine.CodeGeneration.TextTemplating;
    using Zirpl.AppEngine.CodeGeneration.V2;
    using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V2\Templates\Model\PersistableDomainClassTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class PersistableDomainClassTemplate : PersistableDomainClassTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 23 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V2\Templates\Model\PersistableDomainClassTemplate.tt"

if (App != null)
{

            
            #line default
            #line hidden
            this.Write("public partial class asdasdasd\r\n{\r\n}\r\n");
            
            #line 30 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V2\Templates\Model\PersistableDomainClassTemplate.tt"

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
        
        #line 1 "E:\projects\ZAppEngine\Zirpl.AppEngine.CodeGeneration.VS2013\V2\Templates\Model\PersistableDomainClassTemplate.tt"

private global::Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate _FileToGenerateField;

/// <summary>
/// Access the FileToGenerate parameter of the template.
/// </summary>
private global::Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate FileToGenerate
{
    get
    {
        return this._FileToGenerateField;
    }
}

private global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo _AppField;

/// <summary>
/// Access the App parameter of the template.
/// </summary>
private global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo App
{
    get
    {
        return this._AppField;
    }
}

private global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo _DomainTypeField;

/// <summary>
/// Access the DomainType parameter of the template.
/// </summary>
private global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo DomainType
{
    get
    {
        return this._DomainTypeField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool FileToGenerateValueAcquired = false;
if (this.Session.ContainsKey("FileToGenerate"))
{
    this._FileToGenerateField = ((global::Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate)(this.Session["FileToGenerate"]));
    FileToGenerateValueAcquired = true;
}
if ((FileToGenerateValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "FileToGenerate");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(global::Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate));
        if (((tc != null) 
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._FileToGenerateField = ((global::Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate)(tc.ConvertFrom(parameterValue)));
            FileToGenerateValueAcquired = true;
        }
        else
        {
            this.Error("The type \'Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate\' of the pa" +
                    "rameter \'FileToGenerate\' did not match the type of the data passed to the templa" +
                    "te.");
        }
    }
}
if ((FileToGenerateValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("FileToGenerate");
    if ((data != null))
    {
        this._FileToGenerateField = ((global::Zirpl.AppEngine.CodeGeneration.TextTemplating.FileToGenerate)(data));
    }
}
bool AppValueAcquired = false;
if (this.Session.ContainsKey("App"))
{
    this._AppField = ((global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo)(this.Session["App"]));
    AppValueAcquired = true;
}
if ((AppValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "App");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo));
        if (((tc != null) 
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._AppField = ((global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo)(tc.ConvertFrom(parameterValue)));
            AppValueAcquired = true;
        }
        else
        {
            this.Error("The type \'Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo\' of the parameter" +
                    " \'App\' did not match the type of the data passed to the template.");
        }
    }
}
if ((AppValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("App");
    if ((data != null))
    {
        this._AppField = ((global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.AppInfo)(data));
    }
}
bool DomainTypeValueAcquired = false;
if (this.Session.ContainsKey("DomainType"))
{
    this._DomainTypeField = ((global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo)(this.Session["DomainType"]));
    DomainTypeValueAcquired = true;
}
if ((DomainTypeValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "DomainType");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo));
        if (((tc != null) 
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._DomainTypeField = ((global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo)(tc.ConvertFrom(parameterValue)));
            DomainTypeValueAcquired = true;
        }
        else
        {
            this.Error("The type \'Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo\' of the pa" +
                    "rameter \'DomainType\' did not match the type of the data passed to the template.");
        }
    }
}
if ((DomainTypeValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("DomainType");
    if ((data != null))
    {
        this._DomainTypeField = ((global::Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.DomainTypeInfo)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class PersistableDomainClassTemplateBase
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
