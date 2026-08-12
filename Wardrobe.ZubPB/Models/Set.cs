using System.ComponentModel.DataAnnotations;

namespace WardrobeInventory.Models;

public class Set
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    [Required]
    public int UpperClothId { get; set; }

    public Cloth? UpperCloth { get; set; }

    [Required]
    public int LowerClothId { get; set; }

    public Cloth? LowerCloth { get; set; }

    [Required]
    public int ShoesId { get; set; }

    public Cloth? Shoes { get; set; }
}