using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WardrobeInventory.Database;
using WardrobeInventory.Models;

namespace WardrobeInventory.Services;

public class SetService
{
    private readonly WardrobeContext _context;

    public SetService(WardrobeContext context) => _context = context;

    public async Task<List<Set>?> GetAllAsync() => await _context.Sets.Include(x => x.UpperCloth).Include(x => x.LowerCloth).Include(x => x.Shoes).IgnoreAutoIncludes().ToListAsync();

    public async Task<Set?> GetAsync(int id) => await _context.Sets.Include(x => x.UpperCloth).Include(x => x.LowerCloth).Include(x => x.Shoes).IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Set> CreateAsync(Set set)
    {
        EntityEntry<Set> entry = await _context.Sets.AddAsync(set);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(int id, Set inputSet)
    {
        Set? set = await GetAsync(id);
        if (set != null)
        {
            set.ShoesId = inputSet.ShoesId;
            set.LowerClothId = inputSet.LowerClothId;
            set.UpperClothId = inputSet.UpperClothId;

            _context.Sets.Update(set);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Set? set = await GetAsync(id);
        if (set != null)
        {
            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }
}