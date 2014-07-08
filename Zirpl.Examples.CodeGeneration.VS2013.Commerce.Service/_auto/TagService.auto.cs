using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service
{
    public partial class TagService  : DbContextServiceBase<CommerceDataContext, Tag, int>, ITagService
    {
    }
}
