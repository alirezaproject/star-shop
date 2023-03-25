using Domain.Attributes;

namespace Domain.Categories;

[Auditable]
public class CategoryType
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;


    #region relations

    public int? ParentCategoryTypeId { get; set; }
    public CategoryType ParentCategoryType { get; set; } = default!;

    public ICollection<CategoryType> SubType { get; set; } = default!;


    #endregion
}