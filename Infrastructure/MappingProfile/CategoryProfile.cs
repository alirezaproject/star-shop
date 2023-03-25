using AutoMapper;
using Domain.Categories;
using EndPoint.Shared.DTOs.Categories.CategoryTypes;

namespace Infrastructure.MappingProfile;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryType, CategoryTypeDto>().ReverseMap();
        CreateMap<CategoryType, CategoryTypeListDto>()
            .ForMember(dest => dest.SubTypeCount,
                opt => opt.MapFrom(x => x.SubType.Count));
    }
}