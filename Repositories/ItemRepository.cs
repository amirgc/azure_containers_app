using System.Collections.Concurrent;
using azure_exploration.Models;

namespace azure_exploration.Repositories;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    private readonly ConcurrentDictionary<int, Item> _items = new();
    private int _nextId = 1;

    public new Task<Item?> GetByIdAsync(int id)
    {
        _items.TryGetValue(id, out var item);
        return Task.FromResult(item);
    }

    public new Task<IEnumerable<Item>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Item>>(_items.Values.ToList());
    }

    public new Task<Item> CreateAsync(Item item)
    {
        var id = Interlocked.Increment(ref _nextId);
        item.Id = id;
        _items.TryAdd(id, item);
        return Task.FromResult(item);
    }

    public new Task<Item?> UpdateAsync(int id, Item item)
    {
        if (_items.ContainsKey(id))
        {
            item.Id = id;
            _items[id] = item;
            return Task.FromResult<Item?>(item);
        }
        return Task.FromResult<Item?>(null);
    }

    public new Task<bool> DeleteAsync(int id)
    {
        return Task.FromResult(_items.TryRemove(id, out _));
    }

    public Task<IEnumerable<Item>> GetByNameAsync(string name)
    {
        var items = _items.Values.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(items);
    }
}

