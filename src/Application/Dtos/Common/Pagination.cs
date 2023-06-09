namespace Application.Dtos.Common;
public record Pagination<TData>(
    int Page, 
    int PageSize,
    int TotalPages, 
    List<TData> Items);
