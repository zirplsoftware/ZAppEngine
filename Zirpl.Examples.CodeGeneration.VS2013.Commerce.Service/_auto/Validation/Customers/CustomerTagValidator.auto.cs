
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public partial class CustomerTagValidator  : DbEntityValidatorBase<CustomerTag>
		
    {
        public CustomerTagValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                CustomerTagMetadataConstants.Customer_Name, CustomerTagMetadataConstants.CustomerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Tag, o => o.TagId,
                CustomerTagMetadataConstants.Tag_Name, CustomerTagMetadataConstants.TagId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

