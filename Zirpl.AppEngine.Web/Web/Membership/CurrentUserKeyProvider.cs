using System;
using System.Security.Principal;
using System.Web;
using Zirpl.AppEngine.Session;

namespace Zirpl.AppEngine.Web.Membership
{
        public class CurrentUserKeyProvider : ICurrentUserKeyProvider
        {

            public virtual Object GetCurrentUserKey()
            {
                Guid id = Guid.Empty;
                IPrincipal user = HttpContext.Current.User;
                if (user != null
                    && user.Identity.IsAuthenticated)
                {
                    var membershipUser = System.Web.Security.Membership.GetUser(user.Identity.Name);

                    // NOTE: this will ONLY be null in dev mode when reset DB, but this at least prevents the error from occurring
                    if (membershipUser != null)
                    {
                        id = (Guid)membershipUser.ProviderUserKey;
                    }
                }
                return id;
            }
        }
}
