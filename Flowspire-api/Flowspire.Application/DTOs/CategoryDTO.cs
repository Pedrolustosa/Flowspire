namespace Flowspire.Application.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public bool IsDefault { get; set; }
    public int SortOrder { get; set; }
}