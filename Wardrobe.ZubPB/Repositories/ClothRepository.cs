using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WardrobeInventory.Database;
using WardrobeInventory.Models;

namespace WardrobeInventory.Repositories;

public class ClothRepository : IRepository<Cloth>
{
    private readonly WardrobeContext _context;

    public ClothRepository(WardrobeContext context) => _context = context;

    public async Task<List<Cloth>?> GetAllAsync()
    {
        List<Cloth> clt = await _context.Clothes.Include(x => x.Category).IgnoreAutoIncludes().ToListAsync();
        return clt;
    }
    public async Task<Cloth?> GetAsync(int id) => await _context.Clothes.Include(x => x.Category).IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Cloth> CreateAsync(Cloth cloth)
    {
        EntityEntry<Cloth> entry = await _context.Clothes.AddAsync(cloth);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Cloth cloth)
    {
        _context.Clothes.Update(cloth);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Cloth? cloth = await _context.Clothes.FirstOrDefaultAsync(x => x.Id == id);
        if (cloth != null)
        {
            _context.Clothes.Remove(cloth);
            await _context.SaveChangesAsync();
        }
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