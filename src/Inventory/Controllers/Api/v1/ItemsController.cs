using Inventory.Contracts;
using Inventory.Models.DTOs.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers.Api.v1
{
    /// <summary>
    /// API endpoints for the items.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemRepository itemRepository, ILogger<ItemsController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemGetResponseDTO>>> GetItems()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemGetResponseDTO>> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<ItemGetResponseDTO>> CreateItem(ItemCreateRequestDTO createDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult<ItemGetResponseDTO>> UpdateItem(Guid id, ItemUpdateRequestDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
