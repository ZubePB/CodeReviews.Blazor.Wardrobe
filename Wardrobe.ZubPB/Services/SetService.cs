using WardrobeInventory.Models;
using WardrobeInventory.Repositories;

namespace WardrobeInventory.Services;

public class SetService : IService<Set>
{
    private readonly IRepository<Set> _repository;

    public SetService(IRepository<Set> repository) => _repository = repository;

    public async Task<List<Set>?> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Set?> GetAsync(int id) => await _repository.GetAsync(id);

    public async Task<Set> CreateAsync(Set set) => await _repository.CreateAsync(set);

    public async Task<bool> UpdateAsync(int id, Set inputSet)
    {
        Set? set = await _repository.GetAsync(id);
        if (set != null)
        {
            set.Name = inputSet.Name;
            set.LowerClothId = inputSet.LowerClothId;
            set.UpperClothId = inputSet.UpperClothId;
            set.ShoesId = inputSet.ShoesId;
            await _repository.UpdateAsync(set);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return true;
    }
}