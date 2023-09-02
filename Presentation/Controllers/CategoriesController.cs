using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public CategoriesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _services.CategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategoryByIdAsync([FromRoute(Name ="id")] int id)
        {
            var category = await _services.CategoryService.GetOneCategoryByIdAsync(id);

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        [ValidationFilter]
        public async Task<IActionResult> CreateOneCategory([FromBody] CategoryCreateDto category)
        {
            var result = await _services.CategoryService.CreateOneCategoryAsync(category);
            return StatusCode(201, result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Editor")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateOneCategory([FromRoute(Name ="id")] int id, [FromBody] CategoryUpdateDto category)
        {
            await _services.CategoryService.UpdateCategoryAsnc(id, category, true);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneCategoryAsync([FromRoute(Name ="id")] int id)
        {
            await _services.CategoryService.DeleteOneCategoryAsync(id);
            return Ok();
        }

        [Authorize]
        [HttpOptions]
        public IActionResult GetCategoriesOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
