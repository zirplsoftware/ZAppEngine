using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionInstance
    {
        public virtual int CreatedByOrderItemId
        {
            get { return this.CreatedByOrderItem == null ? 0 : this.CreatedByOrderItem.Id; }
        }

        public override bool IsPersisted
        {
            get
            {
                // Because this shares the Id of the CreatedByOrderItem, it will often
                // appear as Persisted even though it is not if the normal algorithm is used.
                // Specifically, if CreatedByOrderItem has been set, it would appear as persisted.
                // So we use the checks below instead.
                //
                if (this.Id.IsNullId()
                    //|| this.CreatedByOrderItemId.IsNullId()
                    || !this.CreatedDate.HasValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }

    public partial class SubscriptionInstanceMetadataConstants
    {
        public const String CreatedByOrderItemId_Name = "CreatedByOrderItemId";
        public const bool CreatedByOrderItemId_IsRequired = false;
    }
}
