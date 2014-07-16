
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class TierShipmentValidator  : DbEntityValidatorBase<TierShipment>
		
    {
        public TierShipmentValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                TierShipmentMetadata.DisplayProduct_Name, TierShipmentMetadata.DisplayProductId_Name);
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierShipmentMetadata.Quantity_MinValue, TierShipmentMetadata.Quantity_MaxValue);
			this.RuleFor(o => o.BaseWeightInOunces).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierShipmentMetadata.BaseWeightInOunces_MinValue, TierShipmentMetadata.BaseWeightInOunces_MaxValue);
			this.RuleFor(o => o.WeightInOuncesEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierShipmentMetadata.WeightInOuncesEach_MinValue, TierShipmentMetadata.WeightInOuncesEach_MaxValue);
			this.RuleFor(o => o.RequiresManualShipmentHandling).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

