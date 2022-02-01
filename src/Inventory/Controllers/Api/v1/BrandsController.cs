using Inventory.Contracts;
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

        //TODO: Get
        //TODO: Get By ID
        //TODO: Create
        //TODO: Update
        //TODO: Delete
    }
}
