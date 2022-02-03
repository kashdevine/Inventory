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
        /// <returns>A list of brands if there aren't any errors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
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

            }

            catch (Exception e)
            {
                _logger.LogError(exception:e, String.Format("Could not return brands for {0}", nameof(GetBrands)));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //TODO: Add Try Catch

            return Ok(brands);
        }
        //TODO: Get By ID
        //TODO: Create
        //TODO: Update
        //TODO: Delete

        //TODO: Method to generate error in logs
    }
}
