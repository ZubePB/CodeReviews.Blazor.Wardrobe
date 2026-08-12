using System.ComponentModel.DataAnnotations;

namespace WardrobeInventory.Models;

public class BodyPart
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public ICollection<Category>? Categories { get; set; }
}