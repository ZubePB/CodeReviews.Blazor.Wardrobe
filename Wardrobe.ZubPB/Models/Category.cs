using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WardrobeInventory.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }

    [Required]
    public int BodyPartId { get; set; }

    public BodyPart? BodyPart { get; set; }

    [JsonIgnore]
    public ICollection<Cloth>? Clothes { get; set; }
}