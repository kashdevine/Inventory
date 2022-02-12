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
        /// <summary>
        /// Gets a category specified by the id.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <returns>A Category.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryCreateRequestDTO">A CategoryCreateRequestDTO.</param>
        /// <returns>A updated category.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryGetResponeDTO>> CreateCategory(CategoryCreateRequestDTO categoryCreateRequestDTO)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to create category for {0} with name {1}", nameof(CreateCategory), categoryCreateRequestDTO.Name));
                Category category = categoryCreateRequestDTO.Adapt<Category>();
                var createdBrand = await _categoryRepository.CreateCategory(category);

                return CreatedAtAction(nameof(CreateCategory), category.Adapt<CategoryGetResponeDTO>());
            }

            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Could not return category for {0} with name {1}", nameof(CreateCategory), categoryCreateRequestDTO.Name));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates a category.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="updateDTO">A CategoryUpdateRequestDTO.</param>
        /// <returns>The updated category.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryGetResponeDTO>> UpdateCategory(Guid id, CategoryUpdateRequestDTO updateDTO)
        {
            if (id != updateDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation(String.Format("Attempting to update category for {0} with name {1}", nameof(UpdateCategory), updateDTO.Name));
                var category = updateDTO.Adapt<Category>();
                var updatedCategory = await _categoryRepository.UpdateCategory(category);

                return Ok(updatedCategory.Adapt<CategoryGetResponeDTO>());
            }
            catch (Exception e)
            {

                _logger.LogError(exception: e, String.Format("Could not update category for {0} with name {1}", nameof(UpdateCategory), updateDTO.Name));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes a category specified by the id.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <returns>A 204 response if delete is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to delete category for {0} with id {1}", nameof(UpdateCategory), id));
                var isDeleted = await _categoryRepository.DeleteCategory(id);
                if (!isDeleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception e)
            {

                _logger.LogError(exception: e, String.Format("Could not delete category for {0} with id {1}", nameof(DeleteCategory), id));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
