namespace Application.Dtos.Common;
public class PaginationFilters
{
    private const int MaxPageSize = 25;

    private int _page;
    public int Page
    {
        get => _page < 1 ? 1 : _page;
        set => _page = value;
    }

    private int _pageSize;
    public int PageSize
    {
        get => _pageSize < 10 ? 10 : _pageSize > MaxPageSize ? MaxPageSize : _pageSize;
        set => _pageSize = value;
    }
}
