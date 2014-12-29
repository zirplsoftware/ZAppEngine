using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    public interface IEventQuery : INamedMemberQuery<EventInfo, IEventQuery>
    {
        ITypeQuery<EventInfo, IEventQuery> OfEventHandlerType();
    }
}