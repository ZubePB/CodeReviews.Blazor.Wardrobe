using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WardrobeInventory.Database;
using WardrobeInventory.Models;

namespace WardrobeInventory.Repositories;

public class SetRepository : IRepository<Set>
{
    private readonly WardrobeContext _context;

    public SetRepository(WardrobeContext context) => _context = context;

    public async Task<List<Set>?> GetAllAsync() => await _context.Sets.Include(x => x.UpperCloth).Include(x => x.LowerCloth).Include(x => x.Shoes).IgnoreAutoIncludes().ToListAsync();

    public async Task<Set?> GetAsync(int id) => await _context.Sets.Include(x => x.UpperCloth).Include(x => x.LowerCloth).Include(x => x.Shoes).IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Set> CreateAsync(Set set)
    {
        set.UpperCloth = null;
        set.LowerCloth = null;
        set.Shoes = null;
        EntityEntry<Set> entry = await _context.Sets.AddAsync(set);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Set set)
    {
        _context.Sets.Update(set);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Set? set = await GetAsync(id);
        if (set != null)
        {
            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();
        }
    }
}