using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Membership
{
    public partial class PasswordResetLinkDataService : DbContextDataServiceBase<CommerceDataContext, PasswordResetLink, int>, IPasswordResetLinkDataService
    {
    }
}
