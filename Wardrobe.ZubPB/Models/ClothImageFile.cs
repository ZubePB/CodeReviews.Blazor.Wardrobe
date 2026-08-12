using Microsoft.AspNetCore.Components.Forms;

namespace WardrobeInventory.Models;

public class ClothImageFile
{
    public int Id { get; set; }

    public string? Path { get; set; }

    public byte[]? Bytes { get; set; }

    public ClothImageFile(string? path, byte[]? bytes) { Path = path; Bytes = bytes; }
}