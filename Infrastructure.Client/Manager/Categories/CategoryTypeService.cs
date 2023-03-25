using Application.Requests.Categories;
using EndPoint.Shared.DTOs.Categories.CategoryTypes;
using EndPoint.Shared.Wrapper;
using Infrastructure.Client.Extensions;

namespace Infrastructure.Client.Manager.Categories;

public class CategoryTypeService : ICategoryTypeService
{
    private readonly HttpClient _httpClient;

    public CategoryTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<PaginatedResult<CategoryTypeListDto>> GetAllAsync(GetAllCategoryTypesRequest request)
    {
        var respone = await _httpClient.GetAsync($"api/CategoryType/{request.ParentId}/{request.PageNumber}/{request.PageSize}");
        return await respone.ToPaginatedResult<CategoryTypeListDto>();
    }
}