using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Membership
{
    public partial class VisitorMapping : CoreEntityMappingBase<Visitor, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Token).IsRequired(VisitorMetadata.Token_IsRequired);
			this.Property(o => o.IsAnonymous).IsRequired(VisitorMetadata.IsAnonymous_IsRequired);
			this.Property(o => o.IsAbandoned).IsRequired(VisitorMetadata.IsAbandoned_IsRequired);
			this.Property(o => o.BotUserAgent).IsRequired(VisitorMetadata.BotUserAgent_IsRequired).HasMaxLength(VisitorMetadata.BotUserAgent_MaxLength, VisitorMetadata.BotUserAgent_IsMaxLength);
			this.Property(o => o.LastActivityDate).IsRequired(VisitorMetadata.LastActivityDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.User,
                                        o => o.UserId,
                                        VisitorMetadata.User_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
