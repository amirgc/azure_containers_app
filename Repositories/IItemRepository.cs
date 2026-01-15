using azure_exploration.Models;

namespace azure_exploration.Repositories;

public interface IItemRepository : IRepository<Item>
{
    Task<IEnumerable<Item>> GetByNameAsync(string name);
}

