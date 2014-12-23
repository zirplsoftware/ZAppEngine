using System;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public static class ObjectExtensions
    {
        public static ITextTransformation AsTextTransformation(this Object textTransformation)
        {
            if (textTransformation is TextTransformation)
            {
                ((TextTransformation) textTransformation).Wrap();
            }
            return new TextTransformationWrapper(textTransformation);
        }
    }
}
