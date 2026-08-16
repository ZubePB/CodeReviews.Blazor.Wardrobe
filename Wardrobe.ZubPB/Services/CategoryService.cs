using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WardrobeInventory.Database;
using WardrobeInventory.Models;

namespace WardrobeInventory.Services;

public class CategoryService
{
    private readonly WardrobeContext _context;

    public CategoryService(WardrobeContext context) => _context = context;

    public async Task<List<Category>?> GetAllAsync() => await _context.Categories.IgnoreAutoIncludes().ToListAsync();

    public async Task<Category?> GetAsync(int id) => await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Category> CreateAsync(Category category)
    {
        EntityEntry<Category> entry = await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(int id, Category inputCategory)
    {
        Category? category = await GetAsync(id);
        if (category != null)
        {
            category.Name = inputCategory.Name;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<List<Category>?> GetWithClothesAsync()
    {
        List<Category> cats = await _context.Categories.ToListAsync();
        foreach (Category cat in cats) cat.Clothes = await _context.Clothes.Where(x => x.CategoryId == cat.Id).IgnoreQueryFilters().ToListAsync();
        return cats;
    }
}