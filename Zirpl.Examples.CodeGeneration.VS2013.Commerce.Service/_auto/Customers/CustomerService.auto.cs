using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Customers
{
    public partial class CustomerService  : DbContextServiceBase<CommerceDataContext, Customer, int>, ICustomerService
    {
    }
}
