
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public partial class VisitorValidator  : DbEntityValidatorBase<Visitor>
		
    {
        public VisitorValidator()
        {
			this.RuleFor(o => o.Token).NotEmpty();
			this.RuleFor(o => o.IsAnonymous).NotNull();
			this.RuleFor(o => o.IsAbandoned).NotNull();
			this.RuleFor(o => o.BotUserAgent).Length(VisitorMetadata.BotUserAgent_MinLength, VisitorMetadata.BotUserAgent_MaxLength);
			this.RuleFor(o => o.LastActivityDate).NotEmpty();
			// unsure how to follow this for validation or even if it should with EF- Collection property: ShoppingCartItems
            this.ForeignEntityAndIdMatchIfNotNull(o => o.User, o => o.UserId,
                VisitorMetadata.User_Name, VisitorMetadata.UserId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

