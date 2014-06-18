using System;
#if !NET35CLIENT
using System.ComponentModel.DataAnnotations;
#endif
#if !NET35 && !NET35CLIENT && !NET40 && !NET40CLIENT
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace Zirpl.AppEngine.Model
{
    [Serializable]
    public abstract class PersistableBase<TId> : IPersistable<TId>
        where TId : IEquatable<TId>
    {
        public const String ID_PROPERTY_NAME = "Id";

        #region IPersistable<TId> Members
        
#if !NET35 && !NET35CLIENT
        [Key]
#endif
        public virtual TId Id { get; set; }

        public virtual Object GetId()
        {
            return Id;
        }

        public virtual void SetId(Object id)
        {
            Id = (TId) id;
        }

        //[ScaffoldColumn(false)]
#if !NET35 && !NET35CLIENT && !NET40 && !NET40CLIENT
        [NotMapped]
#endif
        public virtual bool IsPersisted
        {
            get { return this.EvaluateIsPersisted(); }
        }

        #endregion

        public override bool Equals(object other)
        {
            return this.EvaluateEquals(other);
        }

        public override int GetHashCode()
        {
            return this.EvaluateGetHashCode();
        }
    }
}

/* $Log: BaseIdentifiable.cs,v $
/* Revision 1.1  2006/04/17 12:01:27  nathan
/* rearranged some files for better organization
/*
/* Revision 1.2  2006/04/08 07:17:08  nathan
/* added documentation. fixed nhibernate files
/*
 */