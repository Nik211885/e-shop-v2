using Core.Interfaces;

namespace Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        private List<IEvent>? _events;

        public void RaiseEvent(IEvent @event)
        {
            _events ??= [];
            _events.Add(@event);
        }

        public IReadOnlyCollection<IEvent>? Events => _events;
        public void ClearEvents()=> _events?.Clear();
    }
}