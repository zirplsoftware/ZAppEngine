using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog
{
    public partial class DisplayProductService  : DbContextServiceBase<CommerceDataContext, DisplayProduct, int>, IDisplayProductService
    {
    }
}
