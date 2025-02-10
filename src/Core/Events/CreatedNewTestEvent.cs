using Core.Interfaces;

namespace Core.Events
{
    public class CreatedNewTestEvent(Guid id, string name) : IEvent
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}