using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners
{
    public partial class PartnerDataService : DbContextDataServiceBase<CommerceDataContext, Partner, int>, IPartnerDataService
    {
    }
}
