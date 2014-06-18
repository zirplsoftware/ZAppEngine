using System;
using System.Collections;
using System.Linq;
using Elmah;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;

namespace Zirpl.AppEngine.Web.Elmah.Azure
{
    public class TableErrorLog : ErrorLog
    {
        private string connectionString;
        private String tableName;
        private CloudTableClient cloudTableClient;
        private CloudTable cloudTable;

        public override ErrorLogEntry GetError(string id)
        {
            var tableOperation = TableOperation.Retrieve<TableErrorEntity>(this.ApplicationName, id);
            var result = this.cloudTable.Execute(tableOperation);
            var tableErrorEntity = (TableErrorEntity)result.Result;

            var errorLogEntry = new ErrorLogEntry(
                this,
                id,
                ErrorXml.DecodeString(tableErrorEntity.SerializedError));
            return errorLogEntry;
        }

        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            var query = new TableQuery<TableErrorEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, this.ApplicationName));

            //    .Take((pageIndex + 1) * pageSize).ToList().Skip(pageIndex * pageSize))

            var results = this.cloudTable.ExecuteQuery(query).Skip(pageIndex * pageSize).Take(pageSize);

            var count = 0;
            foreach (var tableErrorEntity in results)
            {

                errorEntryList.Add(new ErrorLogEntry(
                    this,
                    tableErrorEntity.RowKey,
                    ErrorXml.DecodeString(tableErrorEntity.SerializedError)));
                count += 1;
            }
            return count;
        }

        public override string Log(Error error)
        {
            var entity = new TableErrorEntity(error, this.ApplicationName);
            // retry up to 3 times, increasing the time between each by 20 seconds each time
            var retryPolicy = new ExponentialRetry(new TimeSpan(0, 0, 20), 3);
            this.cloudTable.Execute(TableOperation.Insert(entity), new TableRequestOptions() { RetryPolicy = retryPolicy });
            return entity.RowKey;
        }

        public TableErrorLog(IDictionary config)
        {
            this.connectionString = (string)config["connectionString"];
            this.ApplicationName = (string)config["applicationName"];
            this.tableName = (string)config["tableName"] ?? "elmaherrors" + this.ApplicationName;
            this.cloudTableClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();
            this.cloudTable = cloudTableClient.GetTableReference(this.tableName);

            this.cloudTable.CreateIfNotExists();
        }
    }
}
