namespace Zirpl.AppEngine.Model.Metadata
{
    public enum RelationshipDeletionBehaviorTypeEnum : byte
    {
        None = 0,
        /// <summary>
        /// When a "Table1" is deleted, delete all related "Table2" instances.
        /// </summary>
        CascadeDelete = 1,
        /// <summary>
        /// "Table1" can't be deleted if related "Table2" instances exist.
        /// For example, choose Restricted to specify that a customer can't be deleted if the database contains related orders.
        /// </summary>
        Restricted = 2,
        /// <summary>
        /// When "Table1" is deleted, set the reference to "Table1" on related "Table2" instances to null.
        /// </summary>
        Disassociate = 3
    }
}
