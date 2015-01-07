using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public interface ILogInRequest
    {
        String UserName { get; }
        String Password { get; }
    }
}
