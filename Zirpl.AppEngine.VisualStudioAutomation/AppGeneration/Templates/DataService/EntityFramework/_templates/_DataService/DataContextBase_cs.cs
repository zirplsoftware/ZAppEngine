﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Templates.DataService.EntityFramework._templates._DataService
{
    using System.Linq;
    using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
    using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;
    using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class DataContextBase_cs : DataContextBase_csBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Reflection;
using Zirpl.AppEngine;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Session;
using Zirpl.AppEngine.Validation;
using Zirpl.Logging;

namespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Namespace));
            this.Write("\r\n{\r\n    public abstract partial class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TypeName));
            this.Write(@" : global::System.Data.Entity.DbContext
    {
        public IValidationHelper ValidationHelper { get; set; }
        public ICurrentUserKeyProvider CurrentUserKeyProvider { get; set; }
        public IRetryPolicyFactory RetryPolicyFactory { get; set; }

		protected ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TypeName));
            this.Write("()\r\n\t\t{\r\n\t\t\tthis.Database.Log = s => this.GetLog().Debug(s);\r\n\t\t}\r\n        \r\n    " +
                    "    //protected override void OnModelCreating(DbModelBuilder modelBuilder)\r\n    " +
                    "    //{\r\n        //    base.OnModelCreating(modelBuilder);\r\n\r\n        //    this" +
                    ".GetObjectContext().Connection.Open();\r\n        //}\r\n\r\n        protected virtual" +
                    " bool IsModifiable(Object obj)\r\n\t\t{\r\n\t\t\treturn true;\r\n\t\t}\r\n    \r\n        public " +
                    "override int SaveChanges()\r\n        {\r\n            // make sure we aren\'t trying" +
                    " to work with any objects that shouldn\'t be persisted through EF\r\n            //" +
                    "\r\n            var objStateEntries = this.GetObjectContext().ObjectStateManager.G" +
                    "etObjectStateEntries(\r\n                EntityState.Added | EntityState.Modified " +
                    "| EntityState.Deleted);\r\n            foreach (ObjectStateEntry entry in objState" +
                    "Entries)\r\n            {\r\n                if (!this.IsModifiable(entry.Entity))\r\n" +
                    "                {\r\n                    throw new Exception(String.Format(\"Cannot" +
                    " persist entity type directly through DbContext: {0}\", entry.Entity.GetType()));" +
                    "\r\n                }\r\n            }\r\n\r\n            // call OnSaveChanges to set t" +
                    "he automatic properties\r\n            //\r\n            objStateEntries = this.GetO" +
                    "bjectContext().ObjectStateManager.GetObjectStateEntries(\r\n               EntityS" +
                    "tate.Added | EntityState.Modified);\r\n\r\n            foreach (ObjectStateEntry ent" +
                    "ry in objStateEntries)\r\n            {\r\n                this.OnSaveChanges(entry)" +
                    ";\r\n            }\r\n\r\n            // Retry Save if specified\r\n            //\r\n    " +
                    "        return this.RetryPolicyFactory != null\r\n                ? this.RetryPoli" +
                    "cyFactory.CreateRetryPolicy().ExecuteAction<int>(base.SaveChanges)\r\n            " +
                    "    : base.SaveChanges();\r\n        }\r\n\r\n        protected virtual void OnSaveCha" +
                    "nges(ObjectStateEntry entry)\r\n        {\r\n            DateTime now = DateTime.Now" +
                    ";\r\n            var auditable = entry.Entity as IAuditable;\r\n            if (audi" +
                    "table != null)\r\n            {\r\n                auditable.UpdatedDate = now;\r\n   " +
                    "             var id = this.CurrentUserKeyProvider.GetCurrentUserKey();\r\n        " +
                    "        String idAsString = id == null ? null : id.ToString();\r\n                " +
                    "auditable.UpdatedUserId = idAsString;\r\n                if (entry.State == Entity" +
                    "State.Added)\r\n                {\r\n                    auditable.CreatedDate = now" +
                    ";\r\n                    auditable.CreatedUserId = idAsString;\r\n                }\r" +
                    "\n            }\r\n\r\n            // this block rejects ALL changes to properties wh" +
                    "ere the old and new values are the same\r\n            // as otherwise SQL could b" +
                    "e run that attempts to update a column to the same value.\r\n            // This w" +
                    "ill fail in cases where Update has been denied on that column\r\n            //\r\n " +
                    "           if (entry.State == EntityState.Modified)\r\n            {\r\n            " +
                    "    foreach (var propertyName in entry.GetModifiedProperties())\r\n               " +
                    " {\r\n                    if (Object.Equals(entry.OriginalValues[propertyName], en" +
                    "try.CurrentValues[propertyName]))\r\n                    {\r\n                      " +
                    "  entry.RejectPropertyChanges(propertyName);\r\n                    }\r\n           " +
                    "     }\r\n            }\r\n        }\r\n\r\n\r\n        protected override DbEntityValidat" +
                    "ionResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> " +
                    "items)\r\n        {\r\n            List<DbValidationError> errors = new List<DbValid" +
                    "ationError>();\r\n\r\n            //if (entityEntry.State == EntityState.Added\r\n    " +
                    "        //    || entityEntry.State == EntityState.Modified)\r\n            //{\r\n  " +
                    "              if (this.ValidationHelper != null)\r\n                {\r\n           " +
                    "         if (this.ValidationHelper.IsValidatable(entityEntry))\r\n                " +
                    "    {\r\n                        foreach (var error in this.ValidationHelper.Valid" +
                    "ate(entityEntry))\r\n                        {\r\n                            errors" +
                    ".Add(new DbValidationError(error.PropertyName, error.ErrorMessage));\r\n          " +
                    "              }\r\n                    }\r\n                }\r\n            //}\r\n    " +
                    "        errors.AddRange(base.ValidateEntity(entityEntry, items).ValidationErrors" +
                    ");\r\n\r\n            return new DbEntityValidationResult(entityEntry, errors);\r\n   " +
                    "     }\r\n\r\n        public ObjectContext GetObjectContext()\r\n        {\r\n          " +
                    "  return ((IObjectContextAdapter) this).ObjectContext;\r\n        }\r\n\r\n\t\tprotected" +
                    " override void OnModelCreating(DbModelBuilder modelBuilder)\r\n\t\t{\r\n            mo" +
                    "delBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());\r\n\t\t\t" +
                    "\r\n\t\t\tthis.OnOnModelCreating(modelBuilder);\r\n\r\n            base.OnModelCreating(m" +
                    "odelBuilder);\r\n\t\t}\r\n\r\n\t\tpartial void OnOnModelCreating(DbModelBuilder modelBuild" +
                    "er);\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }

private global::Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model.App _AppField;

/// <summary>
/// Access the App parameter of the template.
/// </summary>
private global::Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model.App App
{
    get
    {
        return this._AppField;
    }
}

private string _NamespaceField;

/// <summary>
/// Access the Namespace parameter of the template.
/// </summary>
private string Namespace
{
    get
    {
        return this._NamespaceField;
    }
}

private string _TypeNameField;

/// <summary>
/// Access the TypeName parameter of the template.
/// </summary>
private string TypeName
{
    get
    {
        return this._TypeNameField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool AppValueAcquired = false;
if (this.Session.ContainsKey("App"))
{
    this._AppField = ((global::Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model.App)(this.Session["App"]));
    AppValueAcquired = true;
}
if ((AppValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("App");
    if ((data != null))
    {
        this._AppField = ((global::Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model.App)(data));
    }
}
bool NamespaceValueAcquired = false;
if (this.Session.ContainsKey("Namespace"))
{
    this._NamespaceField = ((string)(this.Session["Namespace"]));
    NamespaceValueAcquired = true;
}
if ((NamespaceValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Namespace");
    if ((data != null))
    {
        this._NamespaceField = ((string)(data));
    }
}
bool TypeNameValueAcquired = false;
if (this.Session.ContainsKey("TypeName"))
{
    this._TypeNameField = ((string)(this.Session["TypeName"]));
    TypeNameValueAcquired = true;
}
if ((TypeNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("TypeName");
    if ((data != null))
    {
        this._TypeNameField = ((string)(data));
    }
}


    }
}


    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class DataContextBase_csBase
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
