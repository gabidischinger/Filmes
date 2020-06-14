using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.MovieTypes;
using Movies.Domain.RatingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application
{
    class MovieHandler : IEntityCrudHandler<Movie>
    {
        private readonly IApplicationDbContext db;

        public MovieHandler(IApplicationDbContext db) => this.db = db;

        public async Task<int> Alterar(int id, Movie movie, int userID)
        {
            if (movie != null)
            {
                var toAlter = await db.Movies.SingleOrDefaultAsync(m => m.ID == id);
                var user = await db.Users.SingleOrDefaultAsync(u => u.ID == userID);
                if (toAlter == null)
                {
                    await Task.FromException(
                        new Movie_NotFoundException(
                            $"Movie com id {id} não foi econtrado.",
                            id, userID
                        )
                    );
                }

                if (toAlter != null && user.Role == "admin")
                {
                    toAlter.Title = movie.Title ?? toAlter.Title;
                    toAlter.Description = movie.Description ?? toAlter.Description;
                    toAlter.Year = movie.Year ?? toAlter.Year;
                    toAlter.Genre = movie.Genre ?? toAlter.Genre;
                    movie.LastModifiedOn = DateTime.Now;
                }
            }
            else
            {
                await Task.FromException(
                        new Movie_NoContentException(
                            $"Nenhum campo enviado para alteração.",
                            id, userID
                        )
                    );
            }

            return await db.SaveChangesAsync();

        }

        public async Task<int> Inserir(Movie movie)
        {
            movie.AddedOn = DateTime.Now;
            movie.LastModifiedOn = DateTime.Now;
            db.Movies.Add(movie);
            return await db.SaveChangesAsync();
        }

        public async Task<Movie[]> Listar(int userID)
        {
            return await db.Movies.ToArrayAsync();
        }

        public async Task<Movie> ObterUm(int id, int userID)
        {
            return await db.Movies.SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<int> Remover(int id, int userID)
        {
            var toRemove = await db.Movies.SingleOrDefaultAsync(m => m.ID == id);
            var user = await db.Users.SingleOrDefaultAsync(u => u.ID == userID);
            if (toRemove != null && user.Role == "admin" && toRemove.Reviews.Count() == 0 && toRemove.Ratings.Count() == 0)
            {
                db.Movies.Remove(toRemove);
                return await db.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }
    }
}
