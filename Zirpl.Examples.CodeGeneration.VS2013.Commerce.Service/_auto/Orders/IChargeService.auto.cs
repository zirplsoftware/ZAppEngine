using System;
using Zirpl.AppEngine.Service;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders
{
    public partial interface IChargeService  : ICompleteService<Charge, int>
    {
    }
}
