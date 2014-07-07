using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService
{
    public partial class NameSuffixTypeDataService : ReadOnlyDbContextDataServiceBase<CommerceDataContext, NameSuffixType, byte>, INameSuffixTypeDataService
    {
    }
}
