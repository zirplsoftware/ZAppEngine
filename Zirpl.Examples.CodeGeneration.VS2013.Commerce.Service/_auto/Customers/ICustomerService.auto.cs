using System;
using Zirpl.AppEngine.Service;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Customers
{
    public partial interface ICustomerService  : ICompleteService<Customer, int>
    {
    }
}
