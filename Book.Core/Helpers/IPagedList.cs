namespace Books.Core.Helpers
{
    public interface IPagedList<T>
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int TotalPages { get; set; }
    }
}