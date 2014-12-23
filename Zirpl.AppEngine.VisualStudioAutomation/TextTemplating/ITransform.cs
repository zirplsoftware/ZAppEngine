using System;
using System.Collections.Generic;
using System.Text;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface ITransform
    {
        IOutputFileManager FileManager { get; }
        ITransform GetChild(Object childTemplate);
        ITransformHost Host { get; }
        Object Template { get; }
        StringBuilder GenerationEnvironment { get; set; }
        IDictionary<string, Object> Session { get; }
        void Initialize();
        String TransformText();

        void RunTemplate(Type templateType, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplate(Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplate<T>(IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplate(ITemplateRunner templateRunner, Type templateType, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplate(ITemplateRunner templateRunner, Object template, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplate<T>(ITemplateRunner templateRunner, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);

        void RunTemplates(ITemplateRunner templateRunner, ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplates(ITemplateRunner templateRunner, IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplates(ITemplateRunner templateRunner, IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplates(ITemplateProvider templateProvider, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplates(IEnumerable<Type> templateTypes, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);
        void RunTemplates(IEnumerable<Object> templates, IDictionary<string, object> sessionParameters = null, IOutputInfoProvider outputInfoProvider = null);

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
