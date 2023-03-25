using Application.Requests.Categories;
using EndPoint.Shared.DTOs.Categories.CategoryTypes;
using EndPoint.Shared.Wrapper;

namespace Infrastructure.Client.Manager.Categories;

public interface ICategoryTypeService :IManager
{
    Task<PaginatedResult<CategoryTypeListDto>> GetAllAsync(GetAllCategoryTypesRequest request);
}