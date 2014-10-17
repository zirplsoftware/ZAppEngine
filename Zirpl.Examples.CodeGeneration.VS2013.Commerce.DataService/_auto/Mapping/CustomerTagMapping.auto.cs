using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers
{
    public partial class CustomerTagMapping : CoreEntityMappingBase<CustomerTag, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Customer,
                                        o => o.CustomerId,
                                        CustomerTagMetadataConstants.Customer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Tag,
                                        o => o.TagId,
                                        CustomerTagMetadataConstants.Tag_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
