using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonWebToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application;
using Movies.Domain;
using Movies.Domain.MovieTypes;
using Movies.Infrastructure;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [JWTAuthorize]
    [LoggingFilter]
    [MoviesExceptionHandler]
    public class MoviesController : ControllerBase
    {
        private readonly IEntityCrudHandler<Movie> handler;

        public MoviesController(IEntityCrudHandler<Movie> handler) => this.handler = handler;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var movies = await handler.Listar(userID);
            return new JsonResult(movies.Select(m => new { m.ID, m.Title }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var movie = await handler.ObterUm(id, userID);
            return new JsonResult(new
            {
                movie.ID,
                movie.Title,
                movie.Description,
                movie.Year,
                movie.Genre,
                movie.AverageRating,
                TotalRatings = movie.Ratings.Count(),
                TotalReviews = movie.Reviews.Count(),
                AddedOn = movie.AddedOn.ToShortDateString(),
                LastModifiedOn = movie.LastModifiedOn.ToShortDateString()
            }); 
        }

        [HttpGet("{id}/Ratings")]
        public async Task<IActionResult> GetRatings(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var movie = await handler.ObterUm(id, userID);
            return new JsonResult(movie.Ratings.Select(r => new { r.ID, r.MovieRating }));
        }

        [HttpGet("{id}/Reviews")]
        [ModelStateHandler]
        public async Task<IActionResult> GetReviews(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var movie = await handler.ObterUm(id, userID);
            return new JsonResult(movie.Reviews.Select(r => new { r.ID, r.User.Name, r.Title, r.Content }));
        }

        [HttpPost]
        [RestrictAdmin]
        [ModelStateHandler]
        public async Task<IActionResult> Post([FromBody]MovieCreateCommand command)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var movie = command.ToMovie();
            movie.UserID = userID;
            await handler.Inserir(movie);
            this.HttpContext.Response.StatusCode = 201;
            return new JsonResult(new { movie.ID });
        }

        [HttpPut("{id}")]
        [RestrictAdmin]
        [ModelStateHandler]
        public async Task<IActionResult> Put(int id, [FromBody]MovieAlterCommand command)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            var movie = command.ToMovie();
            await handler.Alterar(id, movie, userID);
            this.HttpContext.Response.StatusCode = 200;
            var alteredMovie = await handler.ObterUm(id, userID);
            return new JsonResult(new {
                alteredMovie.ID,
                alteredMovie.Title,
                alteredMovie.Description,
                alteredMovie.Year,
                alteredMovie.Genre,
                alteredMovie.AverageRating,
                TotalRatings = alteredMovie.Ratings.Count(),
                TotalReviews = alteredMovie.Reviews.Count(),
                AddedOn = alteredMovie.AddedOn.ToShortDateString(),
                LastModifiedOn = alteredMovie.LastModifiedOn.ToShortDateString()
            });
        }

        [HttpDelete("{id}")]
        [RestrictAdmin]
        public async Task<IActionResult> Delete(int id)
        {
            var userID = int.Parse(((JWTPayload)this.HttpContext.Items["JWTPayload"]).uid);
            await handler.Remover(id, userID);
            this.HttpContext.Response.StatusCode = 200;
            var movies = await handler.Listar(userID);
            return new JsonResult(movies.Select(m => new { m.ID, m.Title }));
        }
    }
}
