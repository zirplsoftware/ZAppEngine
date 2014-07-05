using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.FileManagement
{

    //public class ParamTextTemplate
    //{
    //    private ITextTemplatingEngineHost Host { get; set; }

    //    private ParamTextTemplate(ITextTemplatingEngineHost host)
    //    {
    //        this.Host = host;
    //    }

    //    public static ParamTextTemplate Create(ITextTemplatingEngineHost host)
    //    {
    //        return new ParamTextTemplate(host);
    //    }

    //    public static TextTemplatingSession GetSessionObject()
    //    {
    //        return new TextTemplatingSession();
    //    }

    //    public string TransformText(string templateName, TextTemplatingSession session)
    //    {
    //        return this.GetTemplateContent(templateName, session);
    //    }

    //    public string GetTemplateContent(string templateName, TextTemplatingSession session)
    //    {
    //        string fullName = this.Host.ResolvePath(templateName);
    //        string templateContent = File.ReadAllText(fullName);

    //        var sessionHost = this.Host as ITextTemplatingSessionHost;
    //        sessionHost.Session = session;

    //        var engine = new Microsoft.VisualStudio.TextTemplating.Engine();
    //        return engine.ProcessTemplate(templateContent, this.Host);
    //    }
    //}
}
