using Inventory.Contracts;
using Inventory.Models.DTOs.Vendor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Inventory.Models;

namespace Inventory.Controllers.Api.v1
{
    /// <summary>
    /// API endpoints for the vendors.
    /// </summary>
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

        /// <summary>
        /// Gets the vendor specified by the id.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <returns>The specified vendor.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VendorGetResponseDTO>> GetVendor(Guid id)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to get vendor for {0} with id {1}", nameof(GetVendor), id));
                var vendorResponse =await _vendorRepository.GetVendorById(id);
                if(vendorResponse == null)
                {
                    return NotFound();
                }
                return Ok(vendorResponse.Adapt<VendorGetResponseDTO>());

            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Failded to get vendor for {0} with id {1}", nameof(GetVendor), id));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new vendor.
        /// </summary>
        /// <param name="createDTO">A VendorCreateRequestDTO</param>
        /// <returns>Newly created vendor.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VendorGetResponseDTO>> CreateVendor(VendorCreateRequestDTO createDTO)
        {
            try
            {
                _logger.LogInformation(String.Format("Attempting to create vendor for {0}", nameof(CreateVendor)));
                var createResponse = await _vendorRepository.CreateVendor(createDTO.Adapt<Vendor>());

                return CreatedAtAction(nameof(CreateVendor), createResponse.Adapt<VendorGetResponseDTO>());

            }
            catch (Exception e)
            {
                _logger.LogError(exception: e, String.Format("Failded to create vendor for {0}", nameof(CreateVendor)));

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        /// <summary>
        /// Updates an existing vedor.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="updateDTO">A VendorUpdateRequestDTO</param>
        /// <returns>The updated vendor.s</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VendorGetResponseDTO>> UpdateVendor(Guid id,VendorUpdateRequestDTO updateDTO) 
        { 
            if (id != updateDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                _logger.LogInformation(String.Format("Attempting to update vendor for {0} with id {1}.", nameof(UpdateVendor), id));
                var updatedVendor = await _vendorRepository.UpdateVendor(updateDTO.Adapt<Vendor>());
                return Ok(updatedVendor.Adapt<VendorGetResponseDTO>());
            }
            catch (Exception e)
            {

                _logger.LogError(exception: e, String.Format("Failded to update vendor for {0} with id {1}", nameof(CreateVendor), id));

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Delete
        public async Task<IActionResult> DeleteVendor(Guid id) { throw new NotImplementedException(); }
    }
}
