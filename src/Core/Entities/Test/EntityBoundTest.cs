using Core.Interfaces;

namespace Core.Entities.Test
{
    public class EntityBoundTest : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public TestLevel Level { get; set; }
    }
}