using EndPoint.Shared.DTOs.Categories.CategoryTypes;
using EndPoint.Shared.Wrapper;

namespace Application.Interfaces.Services.Categories;

public interface ICategoryTypeService
{
    Task<IResult<CategoryTypeDto>> Add(CategoryTypeDto categoryTypeDto);
    Task<IResult> Remove(int id);
    Task<IResult<CategoryTypeDto>> Edit(CategoryTypeDto categoryTypeDto);
    Task<IResult<CategoryTypeDto>> FindById(int id);
    Task<PaginatedResult<CategoryTypeListDto>> GetList(int? parentId, int page, int pageSize);


}