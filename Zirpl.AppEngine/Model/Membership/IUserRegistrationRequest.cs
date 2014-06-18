using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public interface IUserRegistrationRequest
    {
        String UserName { get; }
        String Password { get; }
        String EmailAddress { get; set; }
    }
}
