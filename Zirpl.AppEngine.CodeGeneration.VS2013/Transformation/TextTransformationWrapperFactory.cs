using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.Templates;

namespace Zirpl.AppEngine.CodeGeneration.Transformation
{
    public static class TextTransformationWrapperFactory
    {
        public static ITextTransformationWrapper GetWrapper(this TextTransformation textTransformation)
        {
            return new TextTransformationWrapper(textTransformation);
        }
        public static ITextTransformationWrapper GetWrapper(this IPreprocessedTextTransformation textTransformation)
        {
            return new PreprocessedTextTransformationWrapper(textTransformation);
        }
    }
}
