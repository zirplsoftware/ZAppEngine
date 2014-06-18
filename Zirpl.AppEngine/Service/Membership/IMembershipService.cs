using System;
using Zirpl.AppEngine.Model.Membership;

namespace Zirpl.AppEngine.Service.Membership
{
    public interface IMembershipService
    {
        void Register(IUserRegistrationRequest request);
        //MembershipUser GetMembershipUser(String userName);
        //MembershipUser GetMembershipUser(Guid userId);
        String GeneratePassword();
        int MinimumPasswordLength { get; }
        String[] GetRolesForUser(String userName);
        String[] GetRolesForUser(Guid userId);
        bool IsUserInRole(String userName, String roleName);
        bool IsUserInRole(Guid userId, String roleName);
        void AddUserToRole(String userName, String roleName);
        void AddUserToRole(Guid userId, String roleName);
        void ChangePassword(IChangePasswordRequest request);
        void ChangeUserName(IChangeUserNameRequest request);
        void ResetLostPassword(IResetLostPasswordRequest request);
    }
}
