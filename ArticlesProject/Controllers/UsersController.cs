using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;
using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using ArticleProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace ArticlesProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of users.", Type = typeof(User[]))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetUsers()
        {
            var a =  await _service.GetUsers();
            return Ok(a);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a user by id.", Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _service.CreateUser(createRequest);
            var location = string.Format("/api/users/{0}", category.Id);
            return Created(location, category);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("{email}")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SingIn(string email, [FromBody] VerifyUserRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var jwt = new JwtSecurityToken();
            //return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));

            var category = await _service.LogIn(createRequest);

            return Ok(category);
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] CreateUserRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.UpdateUser(id, updateRequest);
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
            await _service.RemoveUser(id);
            return NoContent();
        }

        //[HttpPut]
        //[Route("{id:int:min(1)}/status/{deletedStatus:bool}")]
        //[SwaggerResponse(HttpStatusCode.NoContent, Description = "Sets deleted status for an existed product category.")]
        //[SwaggerResponse(HttpStatusCode.NotFound)]
        //[SwaggerResponse(HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> SetStatus(int id, bool deletedStatus)
        //{
        //    await _service.SetStatusAsync(id, deletedStatus);
        //    return HttpResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        //}

    }
}
