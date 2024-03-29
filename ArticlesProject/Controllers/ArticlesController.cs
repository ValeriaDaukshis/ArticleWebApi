﻿using System.Net;
using System.Threading.Tasks;
using ArticleProject.Models.ArticleModels;
using ArticleProject.Services.ArticleService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace ArticlesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of articles.", Type = typeof(Article[]))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetArticles()
        {
            var articles = await _service.GetArticles();
            return Ok(articles);
        }

        [HttpGet]
        [Route("category/{categoryName}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of articles.", Type = typeof(Article[]))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetArticlesCategory(string categoryName)
        {
            var article = await _service.GetArticlesCategory(categoryName);
            return Ok(article);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a article by id.", Type = typeof(Article))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetArticle(string id)
        {
            var article = await _service.GetArticle(id);
            return Ok(article);
        }

        [HttpGet]
        [Route("{id}/cabinet")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a article by id.", Type = typeof(Article))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetArticlesByUserId(string id)
        {
            var article = await _service.GetArticlesByUserId(id);
            return Ok(article);
        }

        [HttpGet]
        [Route("{id}/comments")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a article by id.", Type = typeof(Article))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetArticleComments(string id)
        {
            var comments = await _service.GetArticleComments(id);
            return Ok(comments);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateArticle([FromBody] UpdateArticleRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _service.CreateArticle(createRequest);
            var location = string.Format("/api/articles/{0}", category.Id);
            return Created(location, category);
        }

        [HttpPost]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new user.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateArticleComment(string id, [FromBody] CreateCommentRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _service.CreateArticleComment(id, updateRequest);
            var location = string.Format("/api/articles/{0}", id);
            return Created(location, comment);
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed article.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateArticle(string id, [FromBody] UpdateArticleRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.UpdateArticle(id, updateRequest);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed article.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveArticle(string id)
        {
            await _service.RemoveArticle(id);
            return NoContent();
        }
    }
}