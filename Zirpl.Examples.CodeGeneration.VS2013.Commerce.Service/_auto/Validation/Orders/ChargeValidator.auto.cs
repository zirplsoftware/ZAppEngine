
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public abstract partial class ChargeValidator<T>  : DbEntityValidatorBase<T>
		where T : Charge
    {
        protected ChargeValidator()
        {
			this.RuleFor(o => o.Date).NotEmpty();
			this.RuleFor(o => o.Amount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(ChargeMetadataConstants.Amount_MinValue, ChargeMetadataConstants.Amount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargeType, o => o.ChargeTypeId,
                ChargeMetadataConstants.ChargeType_Name, ChargeMetadataConstants.ChargeTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargeMethodType, o => o.ChargeMethodTypeId,
                ChargeMetadataConstants.ChargeMethodType_Name, ChargeMetadataConstants.ChargeMethodTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargeStatusType, o => o.ChargeStatusTypeId,
                ChargeMetadataConstants.ChargeStatusType_Name, ChargeMetadataConstants.ChargeStatusTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Order, o => o.OrderId,
                ChargeMetadataConstants.Order_Name, ChargeMetadataConstants.OrderId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

