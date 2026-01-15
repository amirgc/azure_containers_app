using azure_exploration.Models;

namespace azure_exploration.Managers;

public interface IItemManager : IManager<Item>
{
    Task<IEnumerable<Item>> GetByNameAsync(string name);
}

