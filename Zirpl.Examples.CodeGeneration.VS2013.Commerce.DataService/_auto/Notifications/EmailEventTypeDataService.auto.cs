using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Notifications
{
    public partial class EmailEventTypeDataService : ReadOnlyDbContextDataServiceBase<CommerceDataContext, EmailEventType, byte>, IEmailEventTypeDataService
    {
    }
}
