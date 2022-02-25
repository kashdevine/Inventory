using Inventory.Contracts;
using Inventory.Models.DTOs.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Inventory.Models;

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

        /// <summary>
        /// Gets all vendors in the db.
        /// </summary>
        /// <returns>A list of vendors.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets the item specified by the id.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <returns>The specified item.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemGetResponseDTO>> GetItem(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to get item at {0} with id {1}", nameof(GetItem),id));
                var itemResponse =await _itemRepository.GetItemById(id);
                if (itemResponse == null)
                {
                    return NotFound();
                }
                return Ok(itemResponse.Adapt<ItemGetResponseDTO>());
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Failed to get vendor at {0} with id {1}", nameof(GetItem), id));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="createDTO">A ItemCreateRequestDTO.</param>
        /// <returns>The newly created item.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemGetResponseDTO>> CreateItem(ItemCreateRequestDTO createDTO)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to create a new item at {0}", nameof(CreateItem)));
                var itemResponse = await _itemRepository.CreateItem(createDTO.Adapt<Item>());
                return CreatedAtAction(nameof(CreateItem), itemResponse.Adapt<ItemGetResponseDTO>());
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("Failed to create a new item at {0}", nameof(CreateItem)));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="updateDTO">A ItemUpdateRequestDTO</param>
        /// <returns>The updated item.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemGetResponseDTO>> UpdateItem(Guid id, ItemUpdateRequestDTO updateDTO)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (id != updateDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation(String.Format("Attempting to update item at {0} with id {1}", nameof(UpdateItem), id));
                var updateItem = await _itemRepository.UpdateItem(updateDTO.Adapt<Item>());
                return Ok(updateItem.Adapt<ItemGetResponseDTO>());
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("Failed to update item at {0} with id {1}", nameof(UpdateItem), id));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Deletes an existing item.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <returns>A 204 response if the delete was successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to delete item at {0} with id {1}.", nameof(DeleteItem), id));
                var deleted = await _itemRepository.DeleteItem(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Failed to delete item at {0} with id {1}.", nameof(DeleteItem), id));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
