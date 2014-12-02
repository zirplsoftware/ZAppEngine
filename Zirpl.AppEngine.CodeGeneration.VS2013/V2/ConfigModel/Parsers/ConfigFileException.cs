using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers
{
    public class ConfigFileException: Exception
    {
        public ConfigFileException(String message, String filePath)
            :base(message)
        {
            this.ConfigFilePath = filePath;
        }

        public ConfigFileException(String filePath)
            : base()
        {
            this.ConfigFilePath = filePath;
        }

        public ConfigFileException(string message, String filePath, Exception innerException)
            : base(message, innerException)
        {
            this.ConfigFilePath = filePath;
        }

        protected ConfigFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string ToString()
        {
            return "Error in config file '" + this.ConfigFilePath + "'" + Environment.NewLine + base.ToString();
        }

        public String ConfigFilePath { get; set; }
    }
}
