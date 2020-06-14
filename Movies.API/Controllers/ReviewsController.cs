using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonWebToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application;
using Movies.Domain;
using Movies.Domain.ReviewTypes;
using Movies.Infrastructure;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [JWTAuthorize]
    [LoggingFilter]
    [ReviewsExceptionHandler]
    public class ReviewsController : ControllerBase
    {
        private readonly IEntityCrudHandler<Review> handler;

        public ReviewsController(IEntityCrudHandler<Review> handler) => this.handler = handler;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = (int)this.RouteData.Values["UserID"];
            var reviews = await handler.Listar(userID);
            return new JsonResult(reviews.Select(r => new { r.ID, MovieTitle = r.Movie.Title, r.Title }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var review = await handler.ObterUm(id, userID);
            return new JsonResult(new
            {
                review.ID,
                MovieTitle = review.Movie.Title,
                review.Title,
                review.Content,
                AddedOn = review.AddedOn.ToShortDateString(),
                LastModifiedOn = review.LastModifiedOn.ToShortDateString()
            });
        }

        [HttpPost]
        [ModelStateHandler]
        public async Task<IActionResult> Post([FromBody]ReviewCreateCommand command)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var review = command.ToReview();
            review.UserID = userID;
            await handler.Inserir(review);
            this.HttpContext.Response.StatusCode = 201;
            return new JsonResult(new { review.ID });
        }

        [HttpPut("{id}")]
        [ModelStateHandler]
        public async Task<IActionResult> Put(int id, [FromBody] ReviewAlterCommand command)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var review = command.ToReview();
            await handler.Alterar(id, review, userID);
            this.HttpContext.Response.StatusCode = 200;
            var alteredReview = await handler.ObterUm(id, userID);
            return new JsonResult(new { alteredReview.ID, alteredReview.Title, alteredReview.Content, LastModifiedOn = alteredReview.LastModifiedOn.ToShortDateString() });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Remover(id, userID);
            this.HttpContext.Response.StatusCode = 200;
            var reviews = await handler.Listar(userID);
            return new JsonResult(reviews.Select(r => new { r.ID, MovieTitle = r.Movie.Title, r.Title }));
        }
    }
}
