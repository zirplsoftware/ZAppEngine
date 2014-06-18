using System;

namespace Zirpl.AppEngine.Model
{
    public interface IAuthenticatedUser
    {
        String AuthenticationToken { get; }
        String UserName { get; }
    }
}
