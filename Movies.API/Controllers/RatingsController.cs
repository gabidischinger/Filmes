using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonWebToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application;
using Movies.Domain;
using Movies.Domain.RatingTypes;
using Movies.Infrastructure;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [JWTAuthorize]
    [LoggingFilter]
    [RatingsExceptionHandler]
    public class RatingsController : ControllerBase
    {
        private readonly IEntityCrudHandler<Rating> handler;

        public RatingsController(IEntityCrudHandler<Rating> handler) => this.handler = handler;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = (int)this.RouteData.Values["UserID"];
            var ratings = await handler.Listar(userID);
            return new JsonResult(ratings.Select(r => new { r.ID, MovieTitle = r.Movie.Title, r.MovieRating }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var rating = await handler.ObterUm(id, userID);
            return new JsonResult(new
            {
                rating.ID,
                MovieTitle = rating.Movie.Title,
                rating.MovieRating,
                AddedOn = rating.AddedOn.ToShortDateString(),
                LastModifiedOn = rating.LastModifiedOn.ToShortDateString()
            });
        }

        [HttpPost]
        [ModelStateHandler]
        public async Task<IActionResult> Post([FromBody]RatingCreateCommand command)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var rating = command.ToRating();
            rating.UserID = userID;
            await handler.Inserir(rating);
            this.HttpContext.Response.StatusCode = 201;
            return new JsonResult(new { rating.ID });
        }

        [HttpPut("{id}")]
        [ModelStateHandler]
        public async Task<IActionResult> Put(int id, [FromBody]RatingAlterCommand command)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var rating = command.ToRating();
            await handler.Alterar(id, rating, userID);
            this.HttpContext.Response.StatusCode = 200;
            var alteredRating = await handler.ObterUm(id, userID);
            return new JsonResult(new { alteredRating.ID, alteredRating.MovieRating, LastModifiedOn = alteredRating.LastModifiedOn.ToShortDateString() });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Remover(id, userID);
            this.HttpContext.Response.StatusCode = 200;
            var ratings = await handler.Listar(userID);
            return new JsonResult(ratings.Select(r => new { r.ID, MovieTitle = r.Movie.Title, r.MovieRating }));
        }
    }
}
