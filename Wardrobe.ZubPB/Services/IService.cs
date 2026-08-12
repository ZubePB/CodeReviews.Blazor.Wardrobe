namespace WardrobeInventory.Services;

public interface IService<T>
{
    public Task<List<T>?> GetAllAsync();
    public Task<T?> GetAsync(int id);
    public Task<T> CreateAsync(T entity);
    public Task<bool> UpdateAsync(int id, T entity);
    public Task<bool> DeleteAsync(int id);
}