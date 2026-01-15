using azure_exploration.Managers;
using azure_exploration.Models;
using Microsoft.AspNetCore.Mvc;

namespace azure_exploration.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemManager _itemManager;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemManager itemManager, ILogger<ItemsController> logger)
    {
        _itemManager = itemManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAll()
    {
        try
        {
            var items = await _itemManager.GetAllAsync();
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving items");
            return StatusCode(500, "An error occurred while retrieving items");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetById(int id)
    {
        try
        {
            var item = await _itemManager.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound($"Item with id {id} not found");
            }
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving item {Id}", id);
            return StatusCode(500, "An error occurred while retrieving the item");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Item>>> SearchByName([FromQuery] string name)
    {
        try
        {
            var items = await _itemManager.GetByNameAsync(name);
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching items by name {Name}", name);
            return StatusCode(500, "An error occurred while searching items");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Item>> Create([FromBody] Item item)
    {
        try
        {
            var createdItem = await _itemManager.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating item");
            return StatusCode(500, "An error occurred while creating the item");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Item>> Update(int id, [FromBody] Item item)
    {
        try
        {
            var updatedItem = await _itemManager.UpdateAsync(id, item);
            if (updatedItem == null)
            {
                return NotFound($"Item with id {id} not found");
            }
            return Ok(updatedItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item {Id}", id);
            return StatusCode(500, "An error occurred while updating the item");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _itemManager.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Item with id {id} not found");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item {Id}", id);
            return StatusCode(500, "An error occurred while deleting the item");
        }
    }
}

