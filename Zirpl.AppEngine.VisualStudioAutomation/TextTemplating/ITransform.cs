using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITransform
    {
        IMasterTransform Master { get; }
        bool IsMaster { get; }
        Object Template { get; }
        T TemplateAs<T>();
        StringBuilder GenerationEnvironment { get; set; }
        IDictionary<string, Object> Session { get; }
        void Initialize();
        String TransformText();

        #region items that are auto-generated that we might need later
        //CompilerErrorCollection Errors { get; }
        //String CurrentIndent { get; }
        //void ClearIndent();
        //void Error(String message);
        //String PopIndent();
        //void PushIndent(String indent);
        //void Warning(String message);
        //void Write(string text);
        //void Write(String format, params Object[] args);
        //void WriteLine(string text);
        //void WriteLine(String format, params Object[] args);
        #endregion
    }
}
