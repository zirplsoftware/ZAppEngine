using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Orders
{
    public partial class ShoppingCartItemMetadataConstants : MetadataConstantsBase
    {
        public const string Quantity_Name = "Quantity";
		public const bool Quantity_IsRequired = true;
		public const int Quantity_MinValue = 1;
        public const int Quantity_MaxValue = int.MaxValue;

        public const string DisplayProduct_Name = "DisplayProduct";
		public const bool DisplayProduct_IsRequired = true;

		public const string DisplayProductId_Name = "DisplayProductId";
		public const bool DisplayProductId_IsRequired = true;

        public const string Visitor_Name = "Visitor";
		public const bool Visitor_IsRequired = true;

		public const string VisitorId_Name = "VisitorId";
		public const bool VisitorId_IsRequired = true;

        public const string SubscriptionChoice_Name = "SubscriptionChoice";
		public const bool SubscriptionChoice_IsRequired = false;

		public const string SubscriptionChoiceId_Name = "SubscriptionChoiceId";
		public const bool SubscriptionChoiceId_IsRequired = false;

        public const string AddedWhileAnonymous_Name = "AddedWhileAnonymous";
		public const bool AddedWhileAnonymous_IsRequired = true;

	}
}
