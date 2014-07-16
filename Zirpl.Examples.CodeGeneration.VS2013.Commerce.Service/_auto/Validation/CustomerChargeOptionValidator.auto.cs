
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public abstract partial class CustomerChargeOptionValidator<T>  : DbEntityValidatorBase<T> where T: CustomerChargeOption
		where T : CustomerChargeOption
    {
        protected CustomerChargeOptionValidator<T>()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                CustomerChargeOptionMetadata.Customer_Name, CustomerChargeOptionMetadata.CustomerId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

