using System;

namespace Zirpl.AppEngine.Model
{
    public interface IStaticLookup<TId> : IPersistable<TId>, IStaticLookup
    {
    
    }
    public interface IStaticLookup : IPersistable
    {
        String Name { get; set; }
    }
}
