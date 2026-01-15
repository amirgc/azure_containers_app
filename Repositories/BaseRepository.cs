using System.Collections.Concurrent;

namespace azure_exploration.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly ConcurrentDictionary<int, T> _data = new();
    private int _nextId = 1;

    public Task<T?> GetByIdAsync(int id)
    {
        _data.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_data.Values.ToList());
    }

    public Task<T> CreateAsync(T entity)
    {
        var id = Interlocked.Increment(ref _nextId);
        // Note: In a real scenario, you'd set the ID property via reflection or a base class
        _data.TryAdd(id, entity);
        return Task.FromResult(entity);
    }

    public Task<T?> UpdateAsync(int id, T entity)
    {
        if (_data.ContainsKey(id))
        {
            _data[id] = entity;
            return Task.FromResult<T?>(entity);
        }
        return Task.FromResult<T?>(null);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return Task.FromResult(_data.TryRemove(id, out _));
    }
}

