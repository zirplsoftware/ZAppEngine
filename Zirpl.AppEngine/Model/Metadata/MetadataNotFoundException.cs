using System;

namespace Zirpl.AppEngine.Model.Metadata
{
    public class MetadataNotFoundException : Exception
    {
        public MetadataNotFoundException(Type type)
            : base("No Metadata could be found to describe: " + type.Name)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }
    }
}