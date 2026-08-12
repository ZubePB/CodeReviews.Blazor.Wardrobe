using WardrobeInventory.Models;

namespace WardrobeInventory.Server.Database;

public class Dataseed : IDisposable
{
    public Dataseed() { }

    public void Dispose() => GC.SuppressFinalize(this);

    public List<BodyPart> GetBodyParts()
    {
        return new()
        {
            new() { Id = 1, Name = "UpperBody" },
            new() { Id = 2, Name = "LowerBody" },
            new() { Id = 3, Name = "Feet" },
        };
    }

    public List<Category> GetCategories()
    {
        return new()
        {
            new() { Id = 1, Name = "Shirt", BodyPartId = 1 },
            new() { Id = 2, Name = "T-Shirt", BodyPartId = 1 },
            new() { Id = 3, Name = "Pants", BodyPartId = 2 },
            new() { Id = 4, Name = "Shorts", BodyPartId = 2 },
            new() { Id = 5, Name = "Shoes", BodyPartId = 3 },
            new() { Id = 6, Name = "Sneakers", BodyPartId = 3 },
        };
    }

    public List<Cloth> GetClothes()
    {
        List<Cloth> clothes = [];
        string folderPath = "/img/test";
        string filePath;

        for (int i = 1; i < 11; i++)
        {
            filePath = $"{folderPath}/shirt/{string.Format("{0:00}", i)}.jpg";
            clothes.Add(new() { Id = i, Name = $"shirt_{string.Format("{0:00}", i)}", CategoryId = 1, Img = filePath });
        }

        for (int i = 1; i < 11; i++)
        {
            filePath = $"{folderPath}/t-shirt/{string.Format("{0:00}", i)}.jpg";
            clothes.Add(new() { Id = 10 + i, Name = $"t-shirt_{string.Format("{0:00}", i)}", CategoryId = 2, Img = filePath });
        }

        for (int i = 1; i < 11; i++)
        {
            filePath = $"{folderPath}/pants/{string.Format("{0:00}", i)}.jpg";
            clothes.Add(new() { Id = 20 + i, Name = $"pants_{string.Format("{0:00}", i)}", CategoryId = 3, Img = filePath });
        }

        for (int i = 1; i < 11; i++)
        {
            filePath = $"{folderPath}/shorts/{string.Format("{0:00}", i)}.jpg";
            clothes.Add(new() { Id = 30 + i, Name = $"shorts_{string.Format("{0:00}", i)}", CategoryId = 4, Img = filePath });
        }

        for (int i = 1; i < 11; i++)
        {
            filePath = $"{folderPath}/shoes/{string.Format("{0:00}", i)}.jpg";
            clothes.Add(new() { Id = 40 + i, Name = $"shoes_{string.Format("{0:00}", i)}", CategoryId = 5, Img = filePath });
        }

        for (int i = 1; i < 11; i++)
        {
            filePath = $"{folderPath}/sneakers/{string.Format("{0:00}", i)}.jpg";
            clothes.Add(new() { Id = 50 + i, Name = $"sneakers_{string.Format("{0:00}", i)}", CategoryId = 6, Img = filePath });
        }

        return clothes;
    }

    public List<Set> GetSets()
    {
        Random rng = new();
        List<Set> sets = [];
        for(int i = 1; i < 11;i++)
        {
            Set set = new()
            {
                Id = i,
                Name = $"Set_{i}",
                UpperClothId = rng.Next(1, 20),
                LowerClothId = rng.Next(21, 40),
                ShoesId = rng.Next(41, 60),
            };
            sets.Add(set);
        }

        return sets;
    }
}
