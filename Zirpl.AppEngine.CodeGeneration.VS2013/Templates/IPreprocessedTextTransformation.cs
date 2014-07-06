using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.Templates
{
    public interface IPreprocessedTextTransformation
    {
        ITextTemplatingEngineHost Host { get; set; }
        TextTransformation CallingTemplate { get; }
        TemplateHelper TemplateHelper { get; }
    }
}
