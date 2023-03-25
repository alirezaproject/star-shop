namespace Application.Requests.Categories;

public class GetAllCategoryTypesRequest : PagedRequest
{
    public int? ParentId { get; set; }
}