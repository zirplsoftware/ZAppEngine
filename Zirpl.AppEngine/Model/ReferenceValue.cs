using System;

namespace Zirpl.AppEngine.Model
{
    public class ReferenceValue<TId> : PersistableBase<TId> where TId : IEquatable<TId>
    {
        public String Value { get; set; }
    }
}
