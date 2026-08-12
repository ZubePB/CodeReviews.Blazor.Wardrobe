using WardrobeInventory.Models;
using WardrobeInventory.Repositories;

namespace WardrobeInventory.Services;

public class ClothService : IService<Cloth>
{
    private readonly ClothRepository _repository;

    public ClothService(IRepository<Cloth> repository) => _repository = (ClothRepository)repository;

    public async Task<List<Cloth>?> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Cloth?> GetAsync(int id) => await _repository.GetAsync(id);

    public async Task<Cloth> CreateAsync(Cloth cloth) => await _repository.CreateAsync(cloth);

    public async Task<bool> UpdateAsync(int id, Cloth inputCloth)
    {
        Cloth? cloth = await _repository.GetAsync(id);
        if(cloth != null)
        {
            cloth.Name = inputCloth.Name;
            cloth.CategoryId = inputCloth.CategoryId;
            cloth.Img = inputCloth.Img;
            await _repository.UpdateAsync(cloth);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<Cloth?> GetWithSets(int id) => await _repository.GetWithSets(id);
}