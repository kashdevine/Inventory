using Inventory.Contracts;
using Inventory.Models;
using Inventory.Models.DTOs.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers.Api.v1
{
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

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetResponeDTO>>> GetCategories()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetResponeDTO>> GetCategory(Guid id)
        {
            throw new NotImplementedException();
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
