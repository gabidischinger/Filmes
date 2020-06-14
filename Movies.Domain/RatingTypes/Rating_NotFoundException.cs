using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.RatingTypes
{
    public class Rating_NotFoundException : AbstractRatingException
    {
        public Rating_NotFoundException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
