using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zirpl.AppEngine.Testing
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class |
                   AttributeTargets.Interface | AttributeTargets.Assembly,
                   AllowMultiple = false)]
    public class MockHttpContextAttribute : Attribute, NUnit.Framework.ITestAction
    {
        public String Username { get; private set; }

        public MockHttpContextAttribute()
        {
        }
        public MockHttpContextAttribute(String username)
        {
            this.Username = username;
        }

        public void AfterTest(NUnit.Framework.TestDetails testDetails)
        {
        }

        public void BeforeTest(NUnit.Framework.TestDetails testDetails)
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );

            // User is logged in if this.Username is not null, otherwise logged out
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(this.Username ?? String.Empty),
                new string[0]
                );
        }

        public NUnit.Framework.ActionTargets Targets
        {
            get { return NUnit.Framework.ActionTargets.Default; }
        }
    }
}
