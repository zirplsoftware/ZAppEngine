using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public class ClassToGenerate
    {
        public ClassToGenerate(TemplateOutputFile outputFile)
        {
            this.OutputFile = outputFile;
            this.InterfaceDeclarations = new List<string>();
        }

        public TemplateOutputFile OutputFile { get; private set; }
        public String ClassName { get; set; }
        public String ClassFullName { get; set; }
        public String Namespace { get; set; }
        public String BaseClass { get; set; }
        public IList<String> InterfaceDeclarations { get; set; }
        //public String NameWithoutGenericParameters { get; set; }
        public bool IsAbstract { get; set; }

        public String ClassDeclaration
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("public ");
                if (this.IsAbstract)
                {
                    sb.Append("abstract ");
                }
                sb.Append("partial class ");
                sb.Append(this.ClassName);
                if (!String.IsNullOrEmpty(this.BaseClass)
                    || this.InterfaceDeclarations.Any())
                    //|| this.GenericConstraintDeclarations.Any())
                {
                    sb.AppendLine(" : ");
                    bool useCommaIfAnother = false;
                    if (!String.IsNullOrEmpty(this.BaseClass))
                    {
                        sb.Append(this.BaseClass);
                        useCommaIfAnother = true;
                    }
                    if (this.InterfaceDeclarations.Any())
                    {
                        foreach (var interfaceDeclaration in this.InterfaceDeclarations)
                        {
                            if (useCommaIfAnother)
                            {
                                sb.AppendLine(",");
                            }
                            sb.Append(interfaceDeclaration);
                            useCommaIfAnother = true;
                        }
                    }
                    //if (this.GenericConstraintDeclarations.Any())
                    //{
                    //    foreach (var genericConstraintDeclaration in this.GenericConstraintDeclarations)
                    //    {
                    //        if (useCommaIfAnother)
                    //        {
                    //            sb.AppendLine(",");
                    //        }
                    //        sb.Append(genericConstraintDeclaration);
                    //        useCommaIfAnother = true;
                    //    }
                    //}
                }
                return sb.ToString();
            }
        }
    }
}
