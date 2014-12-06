using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating
{
    public class OutputClass
    {
        public OutputClass(OutputFile outputFile)
        {
            this.OutputFile = outputFile;
            this.InterfaceDeclarations = new List<string>();
            this.IsPartial = true;
            this.AccessibilityModifier = AccessibilityModifierTypeEnum.Public;
        }

        public OutputFile OutputFile { get; private set; }
        public String ClassName { get; set; }
        public String ClassFullName { get { return this.Namespace + "." + this.ClassName; } }
        public String Namespace { get; set; }
        public String BaseClass { get; set; }
        public IList<String> InterfaceDeclarations { get; set; }
        //public String NameWithoutGenericParameters { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsPartial { get; set; }
        public bool IsSealed { get; set; }
        public bool IsStatic { get; set; }
        public AccessibilityModifierTypeEnum AccessibilityModifier { get; set; }
    }
}
