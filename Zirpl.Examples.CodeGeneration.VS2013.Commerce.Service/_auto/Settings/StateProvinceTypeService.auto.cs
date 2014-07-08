using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Settings
{
    public partial class StateProvinceTypeService  : DictionaryEntityService<CommerceDataContext, StateProvinceType, int, StateProvinceTypeEnum>, IStateProvinceTypeService
    {
    }
}
