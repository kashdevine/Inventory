using Inventory.Contracts;
using Inventory.Models.DTOs.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;

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
            var Items = new List<ItemGetResponseDTO>();
            try
            {
                _logger.LogInformation(String.Format("Attemping to items {0}", nameof(GetItems)));
                var itemsDb = await _itemRepository.GetItems();
                foreach (var item in itemsDb)
                {
                    Items.Add(item.Adapt<ItemGetResponseDTO>());
                }
                return Ok(Items);
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("Failed to get items {0}.", nameof(GetItems)));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
