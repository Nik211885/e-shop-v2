namespace Core.Entities
{
    public abstract class CoreEntity(Guid createdUserId) : BaseEntity
    {
        // id user has created
        public Guid CreatedAt { get; private set; } = createdUserId;
        public DateTimeOffset CreatedDateTime { get; private set; } = DateTimeOffset.UtcNow;
        // id user has updated at
        public Guid UpdatedAt { get; private set; }
        public DateTimeOffset UpdateDateTime { get; private set; }

        public void UpdateEntity(Guid updatedUserId)
        {
            UpdatedAt = updatedUserId;
            UpdateDateTime = DateTimeOffset.UtcNow;
        }
    }
}