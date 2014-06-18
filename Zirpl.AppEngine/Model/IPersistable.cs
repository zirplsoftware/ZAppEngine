using System;

namespace Zirpl.AppEngine.Model
{
    /// <summary>
    /// Represents an object that can be saved to and retreived from a DataStore
    /// </summary>
    public interface IPersistable
    {
        bool IsPersisted { get; }
        Object GetId();
        void SetId(Object id);
    }
}

/* $Log: IIdentifiable.cs,v $
/* Revision 1.2  2006/04/25 02:07:47  nathan
/* moved files around in metadata
/* added CollectionConverter class to allow easy conversion between generic and non-generic collection classes
/*
/* Revision 1.1  2006/04/17 12:01:27  nathan
/* rearranged some files for better organization
/*
/* Revision 1.4  2006/04/06 04:45:27  nathan
/* starting testing
/* tweaked IIdentifiable
/* removed metadatakeyfactory - unnecessary and provided a different mechanism of building it than the MetadataKey constructor's default
/*
/* Revision 1.3  2006/04/06 03:23:37  nathan
/* tweaking bean validation, bean collection validation
/*
/* Revision 1.2  2006/04/02 01:20:55  nathan
/* adding documentation
/*
 */