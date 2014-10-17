using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping
{
    public partial class AddressMapping : CoreEntityMappingBase<Address, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Prefix,
                                        o => o.PrefixId,
                                        AddressMetadataConstants.Prefix_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.FirstName).IsRequired(AddressMetadataConstants.FirstName_IsRequired).HasMaxLength(AddressMetadataConstants.FirstName_MaxLength, AddressMetadataConstants.FirstName_IsMaxLength);
			this.Property(o => o.MiddleName).IsRequired(AddressMetadataConstants.MiddleName_IsRequired).HasMaxLength(AddressMetadataConstants.MiddleName_MaxLength, AddressMetadataConstants.MiddleName_IsMaxLength);
			this.Property(o => o.LastName).IsRequired(AddressMetadataConstants.LastName_IsRequired).HasMaxLength(AddressMetadataConstants.LastName_MaxLength, AddressMetadataConstants.LastName_IsMaxLength);

            this.HasNavigationProperty(o => o.Suffix,
                                        o => o.SuffixId,
                                        AddressMetadataConstants.Suffix_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Position).IsRequired(AddressMetadataConstants.Position_IsRequired).HasMaxLength(AddressMetadataConstants.Position_MaxLength, AddressMetadataConstants.Position_IsMaxLength);
			this.Property(o => o.CompanyName).IsRequired(AddressMetadataConstants.CompanyName_IsRequired).HasMaxLength(AddressMetadataConstants.CompanyName_MaxLength, AddressMetadataConstants.CompanyName_IsMaxLength);
			this.Property(o => o.StreetLine1).IsRequired(AddressMetadataConstants.StreetLine1_IsRequired).HasMaxLength(AddressMetadataConstants.StreetLine1_MaxLength, AddressMetadataConstants.StreetLine1_IsMaxLength);
			this.Property(o => o.StreetLine2).IsRequired(AddressMetadataConstants.StreetLine2_IsRequired).HasMaxLength(AddressMetadataConstants.StreetLine2_MaxLength, AddressMetadataConstants.StreetLine2_IsMaxLength);
			this.Property(o => o.City).IsRequired(AddressMetadataConstants.City_IsRequired).HasMaxLength(AddressMetadataConstants.City_MaxLength, AddressMetadataConstants.City_IsMaxLength);
			this.Property(o => o.PostalCode).IsRequired(AddressMetadataConstants.PostalCode_IsRequired).HasMaxLength(AddressMetadataConstants.PostalCode_MaxLength, AddressMetadataConstants.PostalCode_IsMaxLength);
			this.Property(o => o.PhoneNumber).IsRequired(AddressMetadataConstants.PhoneNumber_IsRequired).HasMaxLength(AddressMetadataConstants.PhoneNumber_MaxLength, AddressMetadataConstants.PhoneNumber_IsMaxLength);

            this.HasNavigationProperty(o => o.StateProvinceType,
                                        o => o.StateProvinceTypeId,
                                        AddressMetadataConstants.StateProvinceType_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
