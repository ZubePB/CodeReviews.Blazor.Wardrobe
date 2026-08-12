using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using WardrobeInventory.Models;
using WardrobeInventory.Services;

namespace WardrobeInventory.Controllers;

public class ClothController : Controller
{
    private readonly ClothService _service;

    public ClothController(IService<Cloth> service) => _service = (ClothService)service;

    public async Task<List<Cloth>?> GetAll() => await _service.GetAllAsync();

    public async Task<Cloth?> Get(int id) => await _service.GetAsync(id);

    [HttpPost]
    public async Task<Cloth> Create([FromBody] Cloth cloth) => await _service.CreateAsync(cloth);

    [HttpPut]
    public async Task<bool> Update(int id, [FromBody] Cloth cloth) => await _service.UpdateAsync(id, cloth);

    [HttpDelete]
    public async Task<bool> Delete(int id) => await _service.DeleteAsync(id);

    [HttpPost]
    public async Task<bool> Upload([FromBody] ClothImageFile? file)
    {
        if (file != null)
        {
            await System.IO.File.WriteAllBytesAsync(file.Path!, file.Bytes!);
            return true;
        }
        return false;
    }

    public async Task<List<Cloth>?> GetByBodyPart(int id)
    {
        List<Cloth>? clothes = await GetAll();
        if(clothes != null) clothes = clothes.Where(x => x.Category!.BodyPartId == id).ToList();
        return clothes;
    }

    public async Task<Cloth?> GetWithSets(int id) => await _service.GetWithSets(id);
}

