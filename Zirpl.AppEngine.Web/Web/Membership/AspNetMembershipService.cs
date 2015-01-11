using System;
using System.Transactions;
using System.Web.Security;
using Zirpl.AppEngine.Model.Membership;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.Membership;
using Zirpl.AppEngine.Validation;
using Zirpl.Logging;

namespace Zirpl.AppEngine.Web.Membership
{
    public abstract class AspNetMembershipService : IMembershipService, IService
    {
        //public CommerceDataContext DataContext { get; set; }
        public IValidationHelper ValidationHelper { get; set; }

        public virtual int MinimumPasswordLength
        {
            get
            {
                return System.Web.Security.Membership.MinRequiredPasswordLength;
            }
        }

        public virtual MembershipUser GetMembershipUser(String userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (!userName.IsValidEmailAddress())
            {
                throw new ArgumentException("Not a valid email address", "userName");
            }

            return System.Web.Security.Membership.GetUser(userName);
        }

        public virtual MembershipUser GetMembershipUser(Guid userId)
        {
            if (Guid.Empty.Equals(userId))
            {
                throw new ArgumentException("userId");
            }

            return System.Web.Security.Membership.GetUser(userId);
        }

        public virtual void ChangeUserName(IChangeUserNameRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            this.GetLog().DebugFormat("Changing username for user {0} to {1}", request.UserId, request.NewUserName);

            this.ValidationHelper.AssertValid(request);

            using (var transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                var membershipUser = System.Web.Security.Membership.GetUser(request.UserId);
                if (membershipUser == null)
                {
                    throw new ChangeUserNameException("Could not change username", ChangeUserNameError.UserNotFound);
                }

                //var outParam = new SqlParameter("@ReturnValue", -1);
                //outParam.Direction = ParameterDirection.Output;
                //var command = this.CreateCommand();
                //command.CommandText = "usp_ChangeUsername";
                //command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.Add(new SqlParameter("@ApplicationName", System.Web.Security.Membership.ApplicationName));
                //command.Parameters.Add(new SqlParameter("@OldUserName", membershipUser.UserName));
                //command.Parameters.Add(new SqlParameter("@NewUserName", request.NewUserName));
                //command.Parameters.Add(outParam);
                //command.ExecuteNonQuery();
                //int outValue = (int)outParam.Value

                var outValue = this.DoChangeUserName(System.Web.Security.Membership.ApplicationName, membershipUser.UserName,
                    request.NewUserName);
;
                switch (outValue)
                {
                    case 15: // success
                        break;
                    case 5: // oldemail not found
                        throw new ChangeUserNameException("Could not change username", ChangeUserNameError.UserNotFound);
                    case 10: // newemail taken
                        throw new ChangeUserNameException("Could not change username", ChangeUserNameError.NewUserNameAlreadyTaken);
                    default:
                        throw new UnexpectedCaseException("Unexpected ReturnValue", outValue);
                }

                var user = GetMembershipUser(request.NewUserName);
                user.Email = request.NewEmailAddress;
                System.Web.Security.Membership.UpdateUser(user);

                transaction.Complete();
            }

            this.GetLog().InfoFormat("Changed username for user {0} to {1}", request.UserId, request.NewUserName);
        }

        protected abstract int DoChangeUserName(String applicationName, String oldUserName, String newUserName);

        public virtual void Register(IUserRegistrationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            this.GetLog().DebugFormat("Registering user {0}", request.UserName);

            this.ValidationHelper.AssertValid(request);

            // Attempt to register the user
            MembershipCreateStatus createStatus;
            System.Web.Security.Membership.CreateUser(request.UserName, request.Password, request.EmailAddress, null, null, true, null, out createStatus);

            this.GetLog().DebugFormat("Registering user {0} result: {1}", request.UserName, createStatus.ToString());

            if (createStatus != MembershipCreateStatus.Success)
            {
                ValidationError error = this.ErrorCodeToValidationError(createStatus);
                if (error != null)
                {
                    throw error.ToValidationException("Validation Error");
                }
                throw new RegisterUserException("Could not register user", createStatus);
            }

            this.GetLog().InfoFormat("Registered user {0}", request.UserName);
        }

        private ValidationError ErrorCodeToValidationError(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                case MembershipCreateStatus.DuplicateEmail:
                    return new ValidationError("EmailAddress",
                        "A user with that e-mail address already exists. Please enter a different e-mail address.");

                case MembershipCreateStatus.InvalidPassword:
                    return new ValidationError("Password",
                        "The password provided is invalid. Please enter a valid password value.");

                case MembershipCreateStatus.InvalidUserName:
                case MembershipCreateStatus.InvalidEmail:
                    return new ValidationError("EmailAddress",
                        "The e-mail address provided is invalid. Please check the value and try again.");
                default:
                    return null;
                //return new ValidationError("", 
                //    "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
            }
        }

        public virtual void ResetLostPassword(IResetLostPasswordRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            this.ValidationHelper.AssertValid(request);

            MembershipUser user = System.Web.Security.Membership.GetUser(request.UserId);
            if (user == null)
            {
                throw new ArgumentException("Cannot find user", "request");
            }
            var tempPassword = user.ResetPassword();

            var changePasswordRequest = new ChangePasswordRequest()
                                            {
                                                OldPassword = tempPassword,
                                                NewPassword = request.NewPassword,
                                                UserId = request.UserId
                                            };
            this.ChangePassword(changePasswordRequest);
        }

        public virtual void ChangePassword(IChangePasswordRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            this.GetLog().DebugFormat("Changing password for user {0}", request.UserId);

            var changePasswordRequest = (ChangePasswordRequest)request;
            this.ValidationHelper.AssertValid(changePasswordRequest);

            // ChangePassword will throw an exception rather
            // than return false in certain failure scenarios.
            bool changePasswordSucceeded = false;
            MembershipUser user = System.Web.Security.Membership.GetUser(changePasswordRequest.UserId);
            if (user == null)
            {
                throw new ArgumentException("Cannot find user", "request");
            }
            changePasswordSucceeded = user.ChangePassword(changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

            if (changePasswordSucceeded)
            {
                this.GetLog().InfoFormat("Successfully changed password for user: {0}", request.UserId);
            }
            else
            {
                throw new ChangePasswordException("An error occurred attempting to change the password");
            }
        }

        public virtual string GeneratePassword()
        {
            return System.Web.Security.Membership.GeneratePassword(6, 0);
        }

        public virtual String[] GetRolesForUser(String userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (this.GetMembershipUser(userName) == null)
            {
                throw new ArgumentException("User does not exist", "userName");
            }

            return Roles.GetRolesForUser(userName);
        }

        public virtual String[] GetRolesForUser(Guid userId)
        {
            if (Guid.Empty.Equals(userId))
            {
                throw new ArgumentException("userId");
            }
            MembershipUser user = this.GetMembershipUser(userId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist", "userId");
            }
            return Roles.GetRolesForUser(user.UserName);
        }

        public virtual bool IsUserInRole(String userName, String roleName)
        {
            // NOTE: this is the preferred method because the other one takes an extra step of getting the user
            
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }
            // NOTE: commented out for performance reasons- these actually take a long time
            //if (!Roles.RoleExists(roleName))
            //{
            //    throw new ArgumentException("Role does not exist", "roleName");
            //}
            //if (this.GetUser(userName) == null)
            //{
            //    throw new ArgumentException("User does not exist", "userName");
            //}

            return Roles.IsUserInRole(userName, roleName);
        }

        public virtual bool IsUserInRole(Guid userId, String roleName)
        {
            // NOTE: this is NOT the preferred method because it takes an extra step of getting the user
            
            if (Guid.Empty.Equals(userId))
            {
                throw new ArgumentException("userId");
            }
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }
            // NOTE: commented out for performance reasons- this actually takes a long time
            //if (!Roles.RoleExists(roleName))
            //{
            //    throw new ArgumentException("Role does not exist", "roleName");
            //}

            MembershipUser user = this.GetMembershipUser(userId); 
            if (user == null)
            {
                throw new ArgumentException("User does not exist", "userId");
            }
            return Roles.IsUserInRole(user.UserName, roleName);
        }

        public virtual void AddUserToRole(String userName, String roleName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }
            if (!Roles.RoleExists(roleName))
            {
                throw new ArgumentException("Role does not exist", "roleName");
            }

            this.GetLog().DebugFormat("Adding user {0} to role {1}", userName, roleName);
            if (this.GetMembershipUser(userName) == null)
            {
                throw new ArgumentException("User does not exist", "userName");
            }
            Roles.AddUserToRole(userName, roleName);
            this.GetLog().InfoFormat("Added user {0} to role {1}", userName, roleName);
        }

        public virtual void AddUserToRole(Guid userId, String roleName)
        {
            if (Guid.Empty.Equals(userId))
            {
                throw new ArgumentException("userId");
            }
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }
            if (!Roles.RoleExists(roleName))
            {
                throw new ArgumentException("Role does not exist", "roleName");
            }

            this.GetLog().DebugFormat("Adding user {0} to role {1}", userId, roleName);
            MembershipUser user = this.GetMembershipUser(userId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist", "userId");
            }
            Roles.AddUserToRole(user.UserName, roleName);
            this.GetLog().InfoFormat("Added user {0} to role {1}", userId, roleName);
        }
    }
}


