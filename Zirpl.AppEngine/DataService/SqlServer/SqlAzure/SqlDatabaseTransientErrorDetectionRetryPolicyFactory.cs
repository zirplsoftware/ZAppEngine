#if !NET35 && !NET35CLIENT && !NET40 && !NET40CLIENT && !SILVERLIGHT
using System;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace Zirpl.AppEngine.DataService.SqlServer.SqlAzure
{
    public class SqlDatabaseTransientErrorDetectionRetryPolicyFactory :IRetryPolicyFactory
    {
        public int RetryCount { get; set; }
        public RetryPolicy CreateRetryPolicy()
        {
            if (this.RetryCount <= 0)
            {
                throw new InvalidOperationException("Cannot CreateRetryPolicy without valid RetryCount value");
            }
            return new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(this.RetryCount);
        }
    }
}
#endif