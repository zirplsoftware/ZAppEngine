using System;

namespace Zirpl.AppEngine.Model
{   
    /// <summary>
    /// Represents an object that can be saved to and retreived from a DataStore
    /// </summary>
    /// <typeparam name="TId">Type of value the Id is represented by</typeparam>
    public interface IPersistable<TId> : IPersistable, IIdable<TId>
    {
        new TId Id { get; set; }
    }

    /// <summary>
    /// Represents an object that can be saved to and retreived from a DataStore
    /// </summary>
    public interface IPersistable : IIdable
    {
        bool IsPersisted { get; }
        void SetId(Object id);
    }
}