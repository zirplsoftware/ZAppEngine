using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public abstract partial class UserRegistrationRequestBase : IUserRegistrationRequest
    {
        public string UserName
        {
            get { return this.EmailAddress; }
        }
    }
}
