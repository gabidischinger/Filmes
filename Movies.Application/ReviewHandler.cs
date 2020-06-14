using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.ReviewTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application
{
    public class ReviewHandler : IEntityCrudHandler<Review>
    {
        private readonly IApplicationDbContext db;

        public ReviewHandler(IApplicationDbContext db) => this.db = db;

        public async Task<int> Alterar(int id, Review review, int userID)
        {
            if (review != null)
            {
                var toAlter = await db.Reviews.SingleOrDefaultAsync(r => r.ID == id);

                if (toAlter == null)
                {
                    await Task.FromException(
                        new Review_NotFoundException(
                            $"Rating com id {id} não foi econtrado.",
                            id, userID
                        )
                    );
                }

                if (toAlter.UserID != userID)
                {
                    await Task.FromException(
                        new Review_InvalidOwnerException(
                            $"Rating com id {id} não pertence ao usuário {userID}.",
                            id, userID
                        )
                    );
                }

                toAlter.Title = review.Title ?? toAlter.Title;
                toAlter.Content = review.Content ?? toAlter.Content;
                toAlter.LastModifiedOn = DateTime.Now;
            }
            else
            {
                await Task.FromException(
                        new Review_NoContentException(
                            $"Nenhum campo enviado para alteração.",
                            id, userID
                        )
                    );
            }
            return await db.SaveChangesAsync();
        }

        public async Task<int> Inserir(Review review)
        {
            review.AddedOn = DateTime.Now;
            review.LastModifiedOn = DateTime.Now;
            db.Reviews.Add(review);
            return await db.SaveChangesAsync();
        }

        public async Task<Review[]> Listar(int userID)
        {
            return await db.Reviews.Where(r => r.UserID == userID).ToArrayAsync();
        }

        public async Task<Review> ObterUm(int id, int userID)
        {
            return await db.Reviews.SingleOrDefaultAsync(r => r.ID == id);
        }

        public async Task<int> Remover(int id, int userID)
        {
            var toRemove = await db.Reviews.SingleOrDefaultAsync(r => r.ID == id);

            if (toRemove == null)
            {
                await Task.FromException(
                    new Review_NotFoundException(
                        $"Rating com id {id} não foi econtrado.",
                        id, userID
                    )
                );
            }

            if (toRemove.UserID != userID)
            {
                await Task.FromException(
                    new Review_InvalidOwnerException(
                        $"Rating com id {id} não pertence ao usuário {userID}.",
                        id, userID
                    )
                );
            }

            db.Reviews.Remove(toRemove);
            return await db.SaveChangesAsync();

        }
    }
}
