using System;

namespace Zirpl.AppEngine.VisualStudioAutomation.Testing.TextTemplating
{
    public abstract class NonInheritingTextTransformationBase
    {
        public abstract String TransformText();
        private global::System.Text.StringBuilder generationEnvironmentField;
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
    }
}
