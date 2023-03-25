using Domain.Attributes;

namespace Domain.Categories;


[Auditable]
public class CategoryBrand
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;

}