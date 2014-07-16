
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public partial class CustomerTagValidator  : DbEntityValidatorBase<CustomerTag>
		
    {
        public CustomerTagValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                CustomerTagMetadata.Customer_Name, CustomerTagMetadata.CustomerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Tag, o => o.TagId,
                CustomerTagMetadata.Tag_Name, CustomerTagMetadata.TagId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

