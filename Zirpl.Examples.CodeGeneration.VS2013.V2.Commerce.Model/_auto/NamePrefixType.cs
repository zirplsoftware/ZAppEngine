using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Extensibility;
using Zirpl.Collections;

namespace Zirpl.Examples.Commerce.Model
{
    public partial class NamePrefixType : System.Object
            , IPersistable<int>
            , IStaticLookup
            , IEnumDescribed<int, Zirpl.Examples.Commerce.Model.NamePrefixTypeEnum>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        #region Interface implementations

        public virtual Object GetId()
        {
            return Id;
        }

        public virtual void SetId(Object id)
        {
            Id = (int)id;
        }

        public virtual bool IsPersisted
        {
            get { return this.EvaluateIsPersisted(); }
        }

        public override bool Equals(object other)
        {
            return this.EvaluateEquals(other);
        }

        public override int GetHashCode()
        {
            return this.EvaluateGetHashCode();
        }
        #endregion
    }
}
