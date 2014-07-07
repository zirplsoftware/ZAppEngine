using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders
{
    public partial class OrderDataService : DbContextDataServiceBase<CommerceDataContext, Order, int>, IOrderDataService
    {
    }
}
