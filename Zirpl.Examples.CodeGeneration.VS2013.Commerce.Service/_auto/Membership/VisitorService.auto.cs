using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Membership
{
    public partial class VisitorService  : DbContextServiceBase<CommerceDataContext, Visitor, int>, IVisitorService
    {
    }
}
