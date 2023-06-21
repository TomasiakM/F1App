using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;
public static class QueryableExtensions
{
    public static async Task<int> GetPageCountAsync<T>(this IQueryable<T> query, int pageSize)
    {
        var count = await query.CountAsync();

        return  (int)Math.Ceiling(count / (double)pageSize);
    }
}
