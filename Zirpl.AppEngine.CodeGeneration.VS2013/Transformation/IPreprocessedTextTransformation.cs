using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.Transformation;

namespace Zirpl.AppEngine.CodeGeneration.Templates
{
    public interface IPreprocessedTextTransformation
    {
        TemplateHelper TemplateHelper { get; }

        CompilerErrorCollection Errors { get; }
        ITextTemplatingEngineHost Host { get; set; }
        String CurrentIndent { get; }
        IDictionary<string, Object> Session { get; set; }
        void ClearIndent();
        void Error(String message);
        //void Initialize();
        String PopIndent();
        void PushIndent(String indent);
        String TransformText();
        void Warning(String message);
        void Write(string text);
        void Write(String format, params Object[] args);
        void WriteLine(string text);
        void WriteLine(String format, params Object[] args);
    }
}
