using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers
{
    public partial class CustomerReferralDataService : DbContextDataServiceBase<CommerceDataContext, CustomerReferral, int>, ICustomerReferralDataService
    {
    }
}
