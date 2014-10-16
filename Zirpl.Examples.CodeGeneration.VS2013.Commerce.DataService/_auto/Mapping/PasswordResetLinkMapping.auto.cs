using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Membership
{
    public partial class PasswordResetLinkMapping : CoreEntityMappingBase<PasswordResetLink, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.User,
                                        o => o.UserId,
                                        PasswordResetLinkMetadataConstants.User_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Token).IsRequired(PasswordResetLinkMetadataConstants.Token_IsRequired);
			this.Property(o => o.Expires).IsRequired(PasswordResetLinkMetadataConstants.Expires_IsRequired).IsDateTime();

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
