using Inventory.Contracts;
using Inventory.Models.DTOs.Vendor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Inventory.Controllers.Api.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly ILogger<VendorsController> _logger;

        public VendorsController(IVendorRepository vendorRepository, ILogger<VendorsController> logger)
        {
            _vendorRepository = vendorRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <returns>A list of vendors.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<VendorGetResponseDTO>>> GetVendors()
        {
            var vendors = new List<VendorGetResponseDTO>();

            try
            {
                _logger.LogInformation(String.Format("Attempting to get vendors for {0}", nameof(GetVendors)));
                var vendorsDb = await _vendorRepository.GetVendors();
                foreach(var vendor in vendorsDb)
                {
                    vendors.Add(vendor.Adapt<VendorGetResponseDTO>());
                }

                return Ok(vendors);
            }
            catch (Exception e)
            {
                _logger.LogError(exception:e, String.Format("Failded to get vendors for {0}", nameof(GetVendors)));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //GetByID
        //Create
        //Update
        //Delete
    }
}
