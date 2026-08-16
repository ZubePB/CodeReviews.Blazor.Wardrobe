using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WardrobeInventory.Database;
using WardrobeInventory.Models;

namespace WardrobeInventory.Services;

public class ClothService
{
    private readonly WardrobeContext _context;

    public ClothService(WardrobeContext context) => _context = context;

    public async Task<List<Cloth>?> GetAllAsync() => await _context.Clothes.Include(x => x.Category).IgnoreAutoIncludes().ToListAsync();

    public async Task<Cloth?> GetAsync(int id) => await _context.Clothes.Include(x => x.Category).IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Cloth> CreateAsync(Cloth cloth)
    {
        EntityEntry<Cloth> entry = await _context.Clothes.AddAsync(cloth);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(int id, Cloth inputCloth)
    {
        Cloth? cloth = await GetAsync(id);
        if (cloth != null)
        {
            cloth.Name = inputCloth.Name;
            cloth.CategoryId = inputCloth.CategoryId;
            cloth.Img = inputCloth.Img;
            _context.Clothes.Update(cloth);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Cloth? cloth = await _context.Clothes.FirstOrDefaultAsync(x => x.Id == id);
        if (cloth != null)
        {
            _context.Clothes.Remove(cloth);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> CategoryExists(int id) => await _context.Categories.AnyAsync(x => x.Id == id);

    public async Task<Cloth?> GetWithSets(int id)
    {
        Cloth? cloth = await GetAsync(id);
        if (cloth != null)
        {
            cloth.Sets = await _context.Sets.Where(x => x.UpperClothId == cloth.Id || x.LowerClothId == cloth.Id || x.ShoesId == cloth.Id).ToListAsync();
            foreach (Set set in cloth.Sets)
            {
                set.UpperCloth = null;
                set.LowerCloth = null;
                set.Shoes = null;
            }
            ;
        }
        return cloth;
    }
}
