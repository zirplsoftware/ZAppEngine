using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class VisitorMetadata : MetadataBase
    {
        public const string Token_Name = "Token";
		public const bool Token_IsRequired = true;

        public const string IsAnonymous_Name = "IsAnonymous";
		public const bool IsAnonymous_IsRequired = true;

        public const string IsAbandoned_Name = "IsAbandoned";
		public const bool IsAbandoned_IsRequired = true;

        public const string BotUserAgent_Name = "BotUserAgent";
		public const bool BotUserAgent_IsRequired = false;
		public const bool BotUserAgent_IsMaxLength = false;
        public const int BotUserAgent_MinLength = 0;
		public const int BotUserAgent_MaxLength = 256;

        public const string LastActivityDate_Name = "LastActivityDate";
		public const bool LastActivityDate_IsRequired = true;

        public const string ShoppingCartItems_Name = "ShoppingCartItems";
		public const bool ShoppingCartItems_IsRequired = false;

        public const string User_Name = "User";
		public const bool User_IsRequired = false;

		public const string UserId_Name = "UserId";
		public const bool UserId_IsRequired = false;

	}
}
