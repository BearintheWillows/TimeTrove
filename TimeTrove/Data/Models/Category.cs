using TimeTrove.Client.Models;

namespace TimeTrove.Data.Models;

public class Category : AuditableEntity
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
    public string Color { get; set; }
    public string Icon { get; set; }
    
    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }
    
    public List<Category> ChildCategories { get; set; }
    
    public static CategoryDTO ToDto(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Active = category.Active,
            Color = category.Color,
            Icon = category.Icon
        };
    }
}