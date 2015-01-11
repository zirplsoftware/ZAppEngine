#if !PORTABLE
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
