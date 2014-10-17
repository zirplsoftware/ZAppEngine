using System;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public class User : PersistableBase<Guid>
    {
        public virtual String UserName { get; set; }
        public virtual String LoweredUserName { get; set; }

        //public Guid ApplicationId { get; set; }
        //public bool IsAnonymous { get; set; }
        //public DateTime LastActivityDate { get; set; }
    }

}
