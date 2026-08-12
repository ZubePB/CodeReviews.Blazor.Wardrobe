using WardrobeInventory.Models;
using WardrobeInventory.Repositories;

namespace WardrobeInventory.Services;

public class CategoryService : IService<Category>
{
    private readonly CategoryRepository _repository;

    public CategoryService(IRepository<Category> repository) => _repository = (CategoryRepository)repository;

    public async Task<List<Category>?> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Category?> GetAsync(int id) => await _repository.GetAsync(id);

    public async Task<Category> CreateAsync(Category category) => await _repository.CreateAsync(category);

    public async Task<bool> UpdateAsync(int id, Category inputCategory)
    {
        Category? category = await _repository.GetAsync(id);
        if (category != null)
        {
            category.Name = inputCategory.Name;
            await _repository.UpdateAsync(category);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<List<Category>?> GetWithClothesAsync() => await _repository.GetWithClothesAsync();
}