using System;
using Elmah;
using Microsoft.WindowsAzure.Storage.Table;

namespace Zirpl.AppEngine.Web.Elmah.Azure
{
    public class TableErrorEntity : TableEntity
    {
        public string SerializedError { get; set; }

        public TableErrorEntity()
        {

        }

        public TableErrorEntity(Error error, String applicationName)
            : base(applicationName, (DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks).ToString("d19"))// + Guid.NewGuid().ToString())
        {
            this.SerializedError = ErrorXml.EncodeString(error);
        }
    }
}
