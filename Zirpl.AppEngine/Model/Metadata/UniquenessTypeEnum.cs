namespace Zirpl.AppEngine.Model.Metadata
{
    public enum UniquenessTypeEnum : byte
    {
        None = 0,
        NotUnique = 1,
        Unique = 2,
        UniqueAmongNonNullValues = 3
    }
}
