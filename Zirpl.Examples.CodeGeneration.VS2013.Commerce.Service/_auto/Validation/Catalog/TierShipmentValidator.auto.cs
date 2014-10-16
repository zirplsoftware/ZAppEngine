
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
                TierShipmentMetadataConstants.DisplayProduct_Name, TierShipmentMetadataConstants.DisplayProductId_Name);
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierShipmentMetadataConstants.Quantity_MinValue, TierShipmentMetadataConstants.Quantity_MaxValue);
			this.RuleFor(o => o.BaseWeightInOunces).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierShipmentMetadataConstants.BaseWeightInOunces_MinValue, TierShipmentMetadataConstants.BaseWeightInOunces_MaxValue);
			this.RuleFor(o => o.WeightInOuncesEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierShipmentMetadataConstants.WeightInOuncesEach_MinValue, TierShipmentMetadataConstants.WeightInOuncesEach_MaxValue);
			this.RuleFor(o => o.RequiresManualShipmentHandling).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

