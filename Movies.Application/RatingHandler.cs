using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movies.Domain;
using Movies.Domain.RatingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application
{
    public class RatingHandler : IEntityCrudHandler<Rating>
    {
        private readonly IApplicationDbContext db;

        public RatingHandler(IApplicationDbContext db) => this.db = db;

        public async Task<int> Alterar(int id, Rating rating, int userID)
        {
            if (rating != null)
            {
                var toAlter = await db.Ratings.SingleOrDefaultAsync(r => r.ID == id);

                if (toAlter == null)
                {
                    await Task.FromException(
                        new Rating_NotFoundException(
                            $"Rating com id {id} não foi econtrado.",
                            id, userID
                        )
                    );
                }

                if (toAlter.UserID != userID)
                {
                    await Task.FromException(
                        new Rating_InvalidOwnerException(
                            $"Rating com id {id} não pertence ao usuário {userID}.",
                            id, userID
                        )
                    );
                }

                toAlter.MovieRating = rating.MovieRating ?? toAlter.MovieRating;
                toAlter.LastModifiedOn = DateTime.Now;
            }
            else
            {
                await Task.FromException(
                        new Rating_NoContentException(
                            $"Nenhum campo enviado para alteração.",
                            id, userID
                        )
                    );
            }

            return await db.SaveChangesAsync();
        }

        public async Task<int> Inserir(Rating rating)
        {
            rating.AddedOn = DateTime.Now;
            rating.LastModifiedOn = DateTime.Now;
            db.Ratings.Add(rating);
            return await db.SaveChangesAsync();
        }

        public async Task<Rating[]> Listar(int userID)
        {
            return await db.Ratings.Where(r => r.UserID == userID).ToArrayAsync();
        }

        public async Task<Rating> ObterUm(int id, int userID)
        {
            return await db.Ratings.SingleOrDefaultAsync(r => r.ID == id);
        }

        public async Task<int> Remover(int id, int userID)
        {
            var toRemove = await db.Ratings.SingleOrDefaultAsync(r => r.ID == id);

            if (toRemove == null)
            {
                await Task.FromException(
                    new Rating_NotFoundException(
                        $"Rating com id {id} não foi econtrado.",
                        id, userID
                    )
                );
            }

            if (toRemove.UserID != userID)
            {
                await Task.FromException(
                    new Rating_InvalidOwnerException(
                        $"Rating com id {id} não pertence ao usuário {userID}.",
                        id, userID
                    )
                );
            }

            db.Ratings.Remove(toRemove);
            return await db.SaveChangesAsync();

        }
    }
}
