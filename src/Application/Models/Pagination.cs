using Core.Entities;

namespace Application.Models
{
    public class Pagination<TEntity>(IReadOnlyCollection<TEntity> items,
        int pageNumber, int pageSize, int totalItems) where TEntity : BaseEntity
    {
        public IReadOnlyCollection<TEntity> Items { get; } = items;
        public int PageNumber { get; } = pageNumber;
        public int TotalItems { get; set; } = totalItems;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / pageSize);
        public static int GetSkipRows(int pageNumber, int pageSize)
            => (pageNumber - 1) * pageSize;
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}