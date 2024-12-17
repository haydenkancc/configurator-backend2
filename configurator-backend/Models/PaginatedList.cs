using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Models
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; } = new List<T>();

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageIndex = Math.Min(pageIndex, TotalPages);

            this.Items.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
