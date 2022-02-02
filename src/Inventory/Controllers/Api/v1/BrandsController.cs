using Inventory.Contracts;
using Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers.Api.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            //TODO: Add Try Catch
            return Ok(await _brandRepository.GetBrands());
        }
        //TODO: Get By ID
        //TODO: Create
        //TODO: Update
        //TODO: Delete

        //TODO: Method to generate error in logs
    }
}
