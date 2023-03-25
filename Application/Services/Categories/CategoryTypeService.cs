using Application.Extensions;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services.Categories;
using AutoMapper;
using Domain.Categories;
using EndPoint.Shared.Constants;
using EndPoint.Shared.DTOs.Categories.CategoryTypes;
using EndPoint.Shared.Extensions;
using EndPoint.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Categories;

public class CategoryTypeService : ICategoryTypeService
{
    private readonly IDataBaseContext _context;
    private readonly IMapper _mapper;

    public CategoryTypeService(IDataBaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IResult<CategoryTypeDto>> Add(CategoryTypeDto categoryTypeDto)
    {
        var model = _mapper.Map<CategoryType>(categoryTypeDto);
        _context.CategoryTypes.Add(model);
        await _context.SaveChangesAsync();
        return await Result<CategoryTypeDto>.SuccessAsync(_mapper.Map<CategoryTypeDto>(model), $"تایپ  {model.Type} با موفقیت در سیستم ثبت شد");
    }

    public async Task<IResult> Remove(int id)
    {
        var data = await _context.CategoryTypes.FindAsync(id);

        if (data == null)
            return await Result.FailAsync(ApplicationMessages.NotFound);

        _context.CategoryTypes.Remove(data);
        await _context.SaveChangesAsync();
        return await Result.SuccessAsync();
    }

    public async Task<IResult<CategoryTypeDto>> Edit(CategoryTypeDto categoryTypeDto)
    {
        var model = await _context.CategoryTypes.SingleOrDefaultAsync(x => x.Id == categoryTypeDto.Id);

        if (model == null)
            return await Result<CategoryTypeDto>.FailAsync(ApplicationMessages.NotFound);

        _mapper.Map(categoryTypeDto, model);

        await _context.SaveChangesAsync();
        return await Result<CategoryTypeDto>.SuccessAsync(_mapper.Map<CategoryTypeDto>(model), $"تایپ {model.Type} با موفقیت ویرایش شد ");

    }

    public async Task<IResult<CategoryTypeDto>> FindById(int id)
    {
        var data = await _context.CategoryTypes.FindAsync(id);
        var mappedData = _mapper.Map<CategoryTypeDto>(data);
        return await Result<CategoryTypeDto>.SuccessAsync(mappedData);
    }

    public async Task<PaginatedResult<CategoryTypeListDto>> GetList(int? parentId, int page, int pageSize)
    {
        var data =  _context.CategoryTypes
            .Where(x => x.ParentCategoryTypeId == parentId)
            .PagedResult(page,pageSize,out var totalCount);

        return await _mapper.ProjectTo<CategoryTypeListDto>(data).ToPaginatedListAsync(page,pageSize);

    }
} 