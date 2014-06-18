using System;
using System.IO;
using System.Web;
using log4net.Appender;

namespace Zirpl.AppEngine.Logging.Log4Net
{
    public class WebRollingFileAppender : RollingFileAppender
    {
        public override string File
        {
            get
            {
                return base.File;
            }
            set
            {
                String file = HttpContext.Current.Server.MapPath(value);
                String directory = Path.GetDirectoryName(file);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                base.File = file;
            }
        }
    }
}
