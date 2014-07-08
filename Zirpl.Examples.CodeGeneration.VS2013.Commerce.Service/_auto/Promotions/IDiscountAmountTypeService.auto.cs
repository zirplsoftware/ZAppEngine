using System;
using Zirpl.AppEngine.Service;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Promotions
{
    public partial interface IDiscountAmountTypeService  : IDictionaryEntityService<DiscountAmountType, byte, DiscountAmountTypeEnum>
    {
    }
}
