using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Membership
{
    public partial class VisitorMapping : CoreEntityMappingBase<Visitor, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Token).IsRequired(VisitorMetadataConstants.Token_IsRequired);
			this.Property(o => o.IsAnonymous).IsRequired(VisitorMetadataConstants.IsAnonymous_IsRequired);
			this.Property(o => o.IsAbandoned).IsRequired(VisitorMetadataConstants.IsAbandoned_IsRequired);
			this.Property(o => o.BotUserAgent).IsRequired(VisitorMetadataConstants.BotUserAgent_IsRequired).HasMaxLength(VisitorMetadataConstants.BotUserAgent_MaxLength, VisitorMetadataConstants.BotUserAgent_IsMaxLength);
			this.Property(o => o.LastActivityDate).IsRequired(VisitorMetadataConstants.LastActivityDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.User,
                                        o => o.UserId,
                                        VisitorMetadataConstants.User_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
