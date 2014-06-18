using System;

namespace Zirpl.AppEngine.Model
{
    public abstract class UserBase<TId> : PersistableBase<TId> where TId : IEquatable<TId>
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
    }
}
