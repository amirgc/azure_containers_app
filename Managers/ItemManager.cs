using azure_exploration.Models;
using azure_exploration.Repositories;

namespace azure_exploration.Managers;

public class ItemManager : BaseManager<Item>, IItemManager
{
    private readonly IItemRepository _itemRepository;

    public ItemManager(IItemRepository itemRepository) : base(itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public Task<IEnumerable<Item>> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Task.FromResult<IEnumerable<Item>>(new List<Item>());
        }
        return _itemRepository.GetByNameAsync(name);
    }

    public override Task<Item> CreateAsync(Item entity)
    {
        // Add business logic validation
        if (string.IsNullOrWhiteSpace(entity.Name))
        {
            throw new ArgumentException("Item name cannot be empty", nameof(entity));
        }

        entity.CreatedAt = DateTime.UtcNow;
        return base.CreateAsync(entity);
    }
}

