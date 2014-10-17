
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public abstract partial class CustomerChargeOptionValidator<T>  : DbEntityValidatorBase<T>
		where T : CustomerChargeOption
    {
        protected CustomerChargeOptionValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                CustomerChargeOptionMetadataConstants.Customer_Name, CustomerChargeOptionMetadataConstants.CustomerId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

