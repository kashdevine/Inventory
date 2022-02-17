using Inventory.Contracts;
using Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Inventory.Models.DTOs.Brand;
using Microsoft.Extensions.Logging;

namespace Inventory.Controllers.Api.v1
{
    /// <summary>
    /// API Endpoints for the the brands.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(IBrandRepository brandRepository, ILogger<BrandsController> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all the brands in the db.
        /// </summary>
        /// <returns>A list of brands.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BrandGetResponseDTO>>> GetBrands()
        {
            var brands = new List<BrandGetResponseDTO>();

            try
            {
                _logger.LogInformation(String.Format("Attempting to get brands for {0}", nameof(GetBrands)));
                var Allbrands = await _brandRepository.GetBrands();

                foreach (var brand in Allbrands)
                {
                    brands.Add(brand.Adapt<BrandGetResponseDTO>());
                }

                return Ok(brands);
            }

            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Could not return brands for {0}", nameof(GetBrands)));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        /// <summary>
        /// Returns the brand specified by the id.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <returns>A brand.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandGetResponseDTO>> GetBrand(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to get brand for {0} with Id {1}", nameof(GetBrands), id));
                var brand = await _brandRepository.GetBrandById(id);

                if (brand == null)
                {
                    return NotFound();
                }

                return Ok(brand.Adapt<BrandGetResponseDTO>());

            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Could not return brand for {0} with Id {1}", nameof(GetBrand), id));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="createDTO">A BrandCreateRequestDTO</param>
        /// <returns>Newly created brand.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandGetResponseDTO>> CreateBrand(BrandCreateRequestDTO createDTO)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to create brand for {0} with name {1}", nameof(CreateBrand), createDTO.Name));
                Brand brand = createDTO.Adapt<Brand>();
                var createdBrand = await _brandRepository.CreateBrand(brand);

                return CreatedAtAction(nameof(CreateBrand), createdBrand.Adapt<BrandGetResponseDTO>());
            }

            catch(Exception e)
            {
                _logger.LogError(exception: e, String.Format("Could not return brand for {0} with name {1}", nameof(CreateBrand), createDTO.Name));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates an existing brand.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <param name="updateDTO">A BrandUpdateRequestDTO</param>
        /// <returns>Updated brand.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandGetResponseDTO>> UpdateBrand(Guid id,BrandUpdateRequestDTO updateDTO)
        {
            if (id != updateDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation(String.Format("Attempting to update brand for {0} with name {1}", nameof(CreateBrand), updateDTO.Name));
                var brand = updateDTO.Adapt<Brand>();
                var updatedBrand = await _brandRepository.UpdateBrand(brand);

                return Ok(updatedBrand.Adapt<BrandGetResponseDTO>());
            }
            catch (Exception e)
            {

                _logger.LogError(exception: e, String.Format("Could not update brand for {0} with name {1}", nameof(UpdateBrand), updateDTO.Name));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Deletes an existing brand.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <returns>A 204 response if the delete was successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to delete brand for {0} with id {1}", nameof(CreateBrand), id));
                var deleted = await _brandRepository.DeleteBrand(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch(Exception e)
            {
                _logger.LogError(exception: e, String.Format("Could not delete brand for {0} with Id {1}", nameof(GetBrand), id));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
