using System;

namespace Zirpl.AppEngine.Model.Metadata
{
    public interface IEntityMetadata
    {
        String FullTypeName { get; set; }
        bool IsInsertable { get; set; }
        bool IsUpdateable { get; set; }
        bool IsDeletable { get; set; }
    }
}
