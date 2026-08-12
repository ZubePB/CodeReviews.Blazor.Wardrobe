using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WardrobeInventory.Database;
using WardrobeInventory.Models;

namespace WardrobeInventory.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly WardrobeContext _context;

    public CategoryRepository(WardrobeContext context) => _context = context;

    public async Task<List<Category>?> GetAllAsync()
    {
        List<Category> cats = await _context.Categories.IgnoreAutoIncludes().ToListAsync();
        return cats;
    }
    public async Task<Category?> GetAsync(int id) => await _context.Categories.IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Category> CreateAsync(Category category)
    {
        EntityEntry<Category> entry = await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if(category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ClothExists(int id) => await _context.Clothes.AnyAsync(x => x.Id == id);
    
    public async Task<List<Category>> GetWithClothesAsync()
    {
        List<Category> cats = await _context.Categories.ToListAsync();
        foreach (Category cat in cats) cat.Clothes = await _context.Clothes.Where(x => x.CategoryId == cat.Id).IgnoreQueryFilters().ToListAsync();
        return cats;
    }
}