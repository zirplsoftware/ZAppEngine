using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel
{
    public class ClassToGenerate : FileToGenerate
    {
        public String Name { get; set; }
        public String FullName { get; set; }
        public String Namespace { get; set; }
        public String BaseClassDeclaration { get; set; }
        public IList<String> InterfaceDeclarations { get; set; }
        public IList<String> GenericConstraintDeclarations { get; set; }
        public String NameWithoutGenericParameters { get; set; }
        public bool IsPartial { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsSealed { get; set; }
        public bool IsGeneric { get; set; }
        public String AccessModifier { get; set; }

        public String BaseDeclaration
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(this.AccessModifier);
                sb.Append(" ");
                if (this.IsPartial)
                {
                    sb.Append("partial ");
                }
                if (this.IsSealed)
                {
                    sb.Append("sealed ");
                }
                if (this.IsAbstract)
                {
                    sb.Append("abstract ");
                }
                sb.Append(this.Name);
                if (!String.IsNullOrEmpty(this.BaseClassDeclaration)
                    || this.InterfaceDeclarations.Any()
                    || this.GenericConstraintDeclarations.Any())
                {
                    sb.AppendLine(" :");
                    bool useCommaIfAnother = false;
                    if (String.IsNullOrEmpty(this.BaseClassDeclaration))
                    {
                        sb.Append(this.BaseClassDeclaration);
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
                    if (this.GenericConstraintDeclarations.Any())
                    {
                        foreach (var genericConstraintDeclaration in this.GenericConstraintDeclarations)
                        {
                            if (useCommaIfAnother)
                            {
                                sb.AppendLine(",");
                            }
                            sb.Append(genericConstraintDeclaration);
                            useCommaIfAnother = true;
                        }
                    }
                }
                return sb.ToString();
            }
        }
    }
}
