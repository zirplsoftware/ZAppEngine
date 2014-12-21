using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    internal interface ITextTransformation : IDisposable
    {
        bool IsPreProcessed { get; }
        String FileExtension { get; }
        StringBuilder GenerationEnvironment { get; set; }
        CompilerErrorCollection Errors { get; }
        ITextTemplatingEngineHost Host { get; set; }
        String CurrentIndent { get; }
        IDictionary<string, Object> Session { get; set; }
        void ClearIndent();
        void Error(String message);
        void Initialize();
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
