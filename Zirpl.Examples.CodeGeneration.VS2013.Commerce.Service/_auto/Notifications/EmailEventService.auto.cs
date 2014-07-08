using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Notifications
{
    public partial class EmailEventService  : DbContextServiceBase<CommerceDataContext, EmailEvent, int>, IEmailEventService
    {
    }
}
