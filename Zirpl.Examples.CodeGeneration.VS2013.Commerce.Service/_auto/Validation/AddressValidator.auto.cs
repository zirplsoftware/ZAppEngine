
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation
{
    public partial class AddressValidator  : DbEntityValidatorBase<Address>
		
    {
        public AddressValidator()
        {
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Prefix, o => o.PrefixId,
                AddressMetadataConstants.Prefix_Name, AddressMetadataConstants.PrefixId_Name);
			this.RuleFor(o => o.FirstName).Length(AddressMetadataConstants.FirstName_MinLength, AddressMetadataConstants.FirstName_MaxLength);
			this.RuleFor(o => o.MiddleName).Length(AddressMetadataConstants.MiddleName_MinLength, AddressMetadataConstants.MiddleName_MaxLength);
			this.RuleFor(o => o.LastName).Length(AddressMetadataConstants.LastName_MinLength, AddressMetadataConstants.LastName_MaxLength);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Suffix, o => o.SuffixId,
                AddressMetadataConstants.Suffix_Name, AddressMetadataConstants.SuffixId_Name);
			this.RuleFor(o => o.Position).Length(AddressMetadataConstants.Position_MinLength, AddressMetadataConstants.Position_MaxLength);
			this.RuleFor(o => o.CompanyName).Length(AddressMetadataConstants.CompanyName_MinLength, AddressMetadataConstants.CompanyName_MaxLength);
			this.RuleFor(o => o.StreetLine1).Length(AddressMetadataConstants.StreetLine1_MinLength, AddressMetadataConstants.StreetLine1_MaxLength);
			this.RuleFor(o => o.StreetLine2).Length(AddressMetadataConstants.StreetLine2_MinLength, AddressMetadataConstants.StreetLine2_MaxLength);
			this.RuleFor(o => o.City).Length(AddressMetadataConstants.City_MinLength, AddressMetadataConstants.City_MaxLength);
			this.RuleFor(o => o.PostalCode).Length(AddressMetadataConstants.PostalCode_MinLength, AddressMetadataConstants.PostalCode_MaxLength);
			this.RuleFor(o => o.PhoneNumber).Length(AddressMetadataConstants.PhoneNumber_MinLength, AddressMetadataConstants.PhoneNumber_MaxLength);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.StateProvinceType, o => o.StateProvinceTypeId,
                AddressMetadataConstants.StateProvinceType_Name, AddressMetadataConstants.StateProvinceTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

