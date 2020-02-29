using System.Net;
using System.Threading.Tasks;
using ArticleProject.Models;
using ArticleProject.Models.CategoryModels;
using ArticleProject.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace ArticlesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of article categories.", Type = typeof(Category[]))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var a = await _service.GetCategories();
            return Ok(a);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a category by id.", Type = typeof(Category))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var a = await _service.GetCategory(id);
            return Ok(a);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] UpdateCategoryRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _service.CreateCategory(createRequest);
            var location = string.Format("/api/categories/{0}", category.Id);
            return Created(location, category);
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] UpdateCategoryRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.UpdateCategory(id, updateRequest);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteProductCategory(string id)
        {
            await _service.Removecategory(id);
            return NoContent();
        }
    }
}