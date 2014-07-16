
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public abstract partial class ChargeValidator<T>  : DbEntityValidatorBase<T>
		where T : Charge
    {
        protected ChargeValidator()
        {
			this.RuleFor(o => o.Date).NotEmpty();
			this.RuleFor(o => o.Amount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(ChargeMetadata.Amount_MinValue, ChargeMetadata.Amount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargeType, o => o.ChargeTypeId,
                ChargeMetadata.ChargeType_Name, ChargeMetadata.ChargeTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargeMethodType, o => o.ChargeMethodTypeId,
                ChargeMetadata.ChargeMethodType_Name, ChargeMetadata.ChargeMethodTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargeStatusType, o => o.ChargeStatusTypeId,
                ChargeMetadata.ChargeStatusType_Name, ChargeMetadata.ChargeStatusTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Order, o => o.OrderId,
                ChargeMetadata.Order_Name, ChargeMetadata.OrderId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

