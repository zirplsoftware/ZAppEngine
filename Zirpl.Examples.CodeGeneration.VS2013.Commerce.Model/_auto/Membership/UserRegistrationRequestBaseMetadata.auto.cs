using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public abstract partial class UserRegistrationRequestBaseMetadata
    {
        public const string EmailAddress_Name = "EmailAddress";
		public const bool EmailAddress_IsRequired = true;
		public const bool EmailAddress_IsMaxLength = false;
        public const int EmailAddress_MinLength = 3;
		public const int EmailAddress_MaxLength = 1024;

        public const string Password_Name = "Password";
		public const bool Password_IsRequired = true;
		public const bool Password_IsMaxLength = false;
        public const int Password_MinLength = 6;
		public const int Password_MaxLength = 1024;

	}
}
