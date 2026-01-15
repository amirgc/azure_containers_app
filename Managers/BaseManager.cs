using azure_exploration.Repositories;

namespace azure_exploration.Managers;

public class BaseManager<T> : IManager<T> where T : class
{
    protected readonly IRepository<T> _repository;

    public BaseManager(IRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual Task<T?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public virtual Task<T> CreateAsync(T entity)
    {
        // Add business logic here (validation, etc.)
        return _repository.CreateAsync(entity);
    }

    public virtual Task<T?> UpdateAsync(int id, T entity)
    {
        // Add business logic here (validation, etc.)
        return _repository.UpdateAsync(id, entity);
    }

    public virtual Task<bool> DeleteAsync(int id)
    {
        // Add business logic here (check dependencies, etc.)
        return _repository.DeleteAsync(id);
    }
}

