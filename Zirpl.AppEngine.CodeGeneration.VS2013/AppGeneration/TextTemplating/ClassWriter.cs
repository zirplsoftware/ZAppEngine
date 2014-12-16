using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public class ClassWriter : IDisposable
    {
        private OutputClass outputClass;
        private StringBuilder generationEnvironment;

        public ClassWriter(TextTransformation callingTemplate, OutputClass outputClass)
            : this(new TextTransformationWrapper(callingTemplate), outputClass)
        {
        }

        public ClassWriter(Object callingTemplate, OutputClass outputClass)
            : this(new TextTransformationWrapper(callingTemplate), outputClass)
        {
            
        }

        public ClassWriter(ITextTransformation callingTemplate, OutputClass outputClass)
        {
            this.outputClass = outputClass;
            this.generationEnvironment = callingTemplate.GenerationEnvironment;


            this.generationEnvironment.Append("namespace ");
            this.generationEnvironment.AppendLine(this.outputClass.Namespace);
            this.generationEnvironment.AppendLine("{");
            switch (outputClass.AccessibilityModifier)
            {
                case AccessibilityModifierTypeEnum.Public:
                    this.generationEnvironment.Append("public ");
                    break;
                case AccessibilityModifierTypeEnum.Private:
                    this.generationEnvironment.Append("private ");
                    break;
                case AccessibilityModifierTypeEnum.Internal:
                    this.generationEnvironment.Append("internal ");
                    break;
                case AccessibilityModifierTypeEnum.ProtectedInternal:
                    this.generationEnvironment.Append("protected internal ");
                    break;
                case AccessibilityModifierTypeEnum.Protected:
                    this.generationEnvironment.Append("protected ");
                    break;
                case AccessibilityModifierTypeEnum.None:
                    break;
            }
            if (this.outputClass.IsAbstract)
            {
                this.generationEnvironment.Append("abstract ");
            }
            if (this.outputClass.IsSealed)
            {
                this.generationEnvironment.Append("sealed ");
            }
            if (this.outputClass.IsStatic)
            {
                this.generationEnvironment.Append("static ");
            }
            if (this.outputClass.IsPartial)
            {
                this.generationEnvironment.Append("partial ");
            }
            this.generationEnvironment.Append("class ");
            this.generationEnvironment.Append(this.outputClass.ClassName);
            if (!String.IsNullOrEmpty(this.outputClass.BaseClass)
                || this.outputClass.InterfaceDeclarations.Any())
            //|| this.GenericConstraintDeclarations.Any())
            {
                this.generationEnvironment.AppendLine(" : ");
                bool useCommaIfAnother = false;
                if (!String.IsNullOrEmpty(this.outputClass.BaseClass))
                {
                    this.generationEnvironment.Append("        ");
                    this.generationEnvironment.Append(this.outputClass.BaseClass);
                    useCommaIfAnother = true;
                }
                if (this.outputClass.InterfaceDeclarations.Any())
                {
                    foreach (var interfaceDeclaration in this.outputClass.InterfaceDeclarations)
                    {
                        if (useCommaIfAnother)
                        {
                            this.generationEnvironment.AppendLine(",");
                        }
                        this.generationEnvironment.Append("        ");
                        this.generationEnvironment.Append(interfaceDeclaration);
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
            this.generationEnvironment.AppendLine();
            this.generationEnvironment.AppendLine("    {");
        }

        public void Dispose()
        {
            this.generationEnvironment.AppendLine("    }");
            this.generationEnvironment.AppendLine("}");
        }
    }
}
