using Inventory.Contracts;
using Inventory.Models;
using Inventory.Models.DTOs.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Inventory.Controllers.Api.v1
{
    /// <summary>
    /// API endpoints for the categores.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryRepository _categoryRepository;
        readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryRepository categoryRepository, ILogger<CategoriesController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A list of categories.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoryGetResponeDTO>>> GetCategories()
        {
            var categories = new List<CategoryGetResponeDTO>();
            try
            {
                _logger.LogInformation(String.Format("Attempting to get categories for {0}", nameof(GetCategories)));
                var allCategories = await _categoryRepository.GetCategories();
                foreach (var category in allCategories)
                {
                      categories.Add(category.Adapt<CategoryGetResponeDTO>());
                }

                return Ok(categories);
            }
            catch (Exception e)
            {

                _logger.LogError(exception: e, String.Format("Could not return categories for {0}", nameof(GetCategories)));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetResponeDTO>> GetCategory(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to get category for {0} with Id {1}", nameof(GetCategory), id));
                var category = await _categoryRepository.GetCategoryById(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category.Adapt<CategoryGetResponeDTO>());

            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Could not return category for {0} with Id {1}", nameof(GetCategory), id));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public async Task<ActionResult<CategoryGetResponeDTO>> CreateCategory(CategoryCreateRequestDTO categoryCreateRequestDTO)
        {
            throw new NotImplementedException();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryGetResponeDTO>> UpdateCategory(CategoryUpdateRequestDTO categoryUpdateRequestDTO)
        {
            throw new NotImplementedException();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
