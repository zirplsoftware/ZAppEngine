using System;
using Zirpl.AppEngine.Service;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Notifications
{
    public partial interface IEmailEventService  : ICompleteService<EmailEvent, int>
    {
    }
}
