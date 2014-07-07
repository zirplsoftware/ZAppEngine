using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping
{
    public partial class AddressMapping : CoreEntityMappingBase<Address, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Prefix,
                                        o => o.PrefixId,
                                        AddressMetadata.Prefix_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.FirstName).IsRequired(AddressMetadata.FirstName_IsRequired).HasMaxLength(AddressMetadata.FirstName_MaxLength, AddressMetadata.FirstName_IsMaxLength);
			this.Property(o => o.MiddleName).IsRequired(AddressMetadata.MiddleName_IsRequired).HasMaxLength(AddressMetadata.MiddleName_MaxLength, AddressMetadata.MiddleName_IsMaxLength);
			this.Property(o => o.LastName).IsRequired(AddressMetadata.LastName_IsRequired).HasMaxLength(AddressMetadata.LastName_MaxLength, AddressMetadata.LastName_IsMaxLength);

            this.HasNavigationProperty(o => o.Suffix,
                                        o => o.SuffixId,
                                        AddressMetadata.Suffix_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Position).IsRequired(AddressMetadata.Position_IsRequired).HasMaxLength(AddressMetadata.Position_MaxLength, AddressMetadata.Position_IsMaxLength);
			this.Property(o => o.CompanyName).IsRequired(AddressMetadata.CompanyName_IsRequired).HasMaxLength(AddressMetadata.CompanyName_MaxLength, AddressMetadata.CompanyName_IsMaxLength);
			this.Property(o => o.StreetLine1).IsRequired(AddressMetadata.StreetLine1_IsRequired).HasMaxLength(AddressMetadata.StreetLine1_MaxLength, AddressMetadata.StreetLine1_IsMaxLength);
			this.Property(o => o.StreetLine2).IsRequired(AddressMetadata.StreetLine2_IsRequired).HasMaxLength(AddressMetadata.StreetLine2_MaxLength, AddressMetadata.StreetLine2_IsMaxLength);
			this.Property(o => o.City).IsRequired(AddressMetadata.City_IsRequired).HasMaxLength(AddressMetadata.City_MaxLength, AddressMetadata.City_IsMaxLength);
			this.Property(o => o.PostalCode).IsRequired(AddressMetadata.PostalCode_IsRequired).HasMaxLength(AddressMetadata.PostalCode_MaxLength, AddressMetadata.PostalCode_IsMaxLength);
			this.Property(o => o.PhoneNumber).IsRequired(AddressMetadata.PhoneNumber_IsRequired).HasMaxLength(AddressMetadata.PhoneNumber_MaxLength, AddressMetadata.PhoneNumber_IsMaxLength);

            this.HasNavigationProperty(o => o.StateProvinceType,
                                        o => o.StateProvinceTypeId,
                                        AddressMetadata.StateProvinceType_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
