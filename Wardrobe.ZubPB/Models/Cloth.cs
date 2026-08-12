using System.ComponentModel.DataAnnotations;

namespace WardrobeInventory.Models;

public class Cloth
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Img { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public ICollection<Set>? Sets { get; set; }
}