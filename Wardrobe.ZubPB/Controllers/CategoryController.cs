using Microsoft.AspNetCore.Mvc;
using WardrobeInventory.Models;
using WardrobeInventory.Services;

namespace WardrobeInventory.Controllers;

public class CategoryController : Controller
{
    private readonly CategoryService _service;

    public CategoryController(IService<Category> service) => _service = (CategoryService)service;

    public async Task<List<Category>?> GetAll() => await _service.GetAllAsync();

    public async Task<Category?> Get(int id) => await _service.GetAsync(id);

    public async Task<Category> Create([FromBody] Category category) => await _service.CreateAsync( category);

    public async Task<bool> Update(int id, [FromBody] Category category) => await _service.UpdateAsync( id, category);

    public async Task<bool> Delete(int id) => await _service.DeleteAsync(id);

    public async Task<List<Category>?> GetWithClothes() => await _service.GetWithClothesAsync();
}
