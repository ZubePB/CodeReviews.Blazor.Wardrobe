using Microsoft.AspNetCore.Mvc;
using WardrobeInventory.Models;

namespace WardrobeInventory.Repositories;

public interface IRepository<T>
{
    public Task<List<T>?> GetAllAsync();
    public Task<T?> GetAsync(int id);
    public Task<T> CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(int id);
}