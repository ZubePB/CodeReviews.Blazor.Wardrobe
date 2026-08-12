using Microsoft.AspNetCore.Mvc;
using WardrobeInventory.Models;
using WardrobeInventory.Services;

namespace WardrobeInventory.Controllers;

public class SetController : Controller
{
    private readonly IService<Set> _service;

    public SetController(IService<Set> service) => _service = service;

    public async Task<List<Set>?> GetAll() => await _service.GetAllAsync();

    public async Task<Set?> Get(int id ) => await _service.GetAsync(id);

    public async Task<Set> Create([FromBody] Set set) => await _service.CreateAsync(set);

    public async Task<bool> Update(int id, [FromBody] Set set) => await _service.UpdateAsync(id, set);

    public async Task<bool> Delete(int id) => await _service.DeleteAsync(id);
}
