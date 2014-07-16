
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation
{
    public partial class AddressValidator  : DbEntityValidatorBase<Address>
		
    {
        public AddressValidator()
        {
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Prefix, o => o.PrefixId,
                AddressMetadata.Prefix_Name, AddressMetadata.PrefixId_Name);
			this.RuleFor(o => o.FirstName).Length(AddressMetadata.FirstName_MinLength, AddressMetadata.FirstName_MaxLength);
			this.RuleFor(o => o.MiddleName).Length(AddressMetadata.MiddleName_MinLength, AddressMetadata.MiddleName_MaxLength);
			this.RuleFor(o => o.LastName).Length(AddressMetadata.LastName_MinLength, AddressMetadata.LastName_MaxLength);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Suffix, o => o.SuffixId,
                AddressMetadata.Suffix_Name, AddressMetadata.SuffixId_Name);
			this.RuleFor(o => o.Position).Length(AddressMetadata.Position_MinLength, AddressMetadata.Position_MaxLength);
			this.RuleFor(o => o.CompanyName).Length(AddressMetadata.CompanyName_MinLength, AddressMetadata.CompanyName_MaxLength);
			this.RuleFor(o => o.StreetLine1).Length(AddressMetadata.StreetLine1_MinLength, AddressMetadata.StreetLine1_MaxLength);
			this.RuleFor(o => o.StreetLine2).Length(AddressMetadata.StreetLine2_MinLength, AddressMetadata.StreetLine2_MaxLength);
			this.RuleFor(o => o.City).Length(AddressMetadata.City_MinLength, AddressMetadata.City_MaxLength);
			this.RuleFor(o => o.PostalCode).Length(AddressMetadata.PostalCode_MinLength, AddressMetadata.PostalCode_MaxLength);
			this.RuleFor(o => o.PhoneNumber).Length(AddressMetadata.PhoneNumber_MinLength, AddressMetadata.PhoneNumber_MaxLength);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.StateProvinceType, o => o.StateProvinceTypeId,
                AddressMetadata.StateProvinceType_Name, AddressMetadata.StateProvinceTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

