namespace Application.Requests;

public class PagedRequest
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public string[] Orderby { get; set; } = null!;
}