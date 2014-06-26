#if !NET35 && !NET35CLIENT && !NET40 && !NET40CLIENT && !SILVERLIGHT
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace Zirpl.AppEngine
{
    public interface IRetryPolicyFactory
    {
        int RetryCount { get; set; }
        RetryPolicy CreateRetryPolicy();
    }
}
#endif
