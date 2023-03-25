namespace EndPoint.Shared.DTOs.Categories.CategoryTypes;

public class CategoryTypeDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int? ParentCategoryTypeId { get; set; } = default!;

}