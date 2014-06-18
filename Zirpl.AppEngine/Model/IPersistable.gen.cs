namespace Zirpl.AppEngine.Model
{
    /// <summary>
    /// Represents an object that can be saved to and retreived from a DataStore
    /// </summary>
    /// <typeparam name="TId">Type of value the Id is represented by</typeparam>
    public interface IPersistable<TId> : IPersistable
    {
        /// <summary>
        /// Gets or sets the ID of the object
        /// </summary>
        TId Id { get; set; }
    }
}