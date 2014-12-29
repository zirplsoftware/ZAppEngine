using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class EventQuery : NamedTypeMemberQueryBase<EventInfo, IEventQuery>, 
        IEventQuery
    {
        private readonly EventHandlerTypeEvaluator _eventTypeEvaluator;

        internal EventQuery(Type type)
            :base(type)
        {
            _memberTypeEvaluator.Event = true;
            _eventTypeEvaluator = new EventHandlerTypeEvaluator();
            _matchEvaluators.Add(_eventTypeEvaluator);
        }

        ITypeQuery<EventInfo, IEventQuery> IEventQuery.OfEventHandlerType()
        {
            return new TypeSubQuery<EventInfo, IEventQuery>(this, _eventTypeEvaluator);
        }
    }
}
