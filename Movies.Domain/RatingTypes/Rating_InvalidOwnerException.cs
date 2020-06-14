using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.RatingTypes
{
    public class Rating_InvalidOwnerException : AbstractRatingException
    {
        public Rating_InvalidOwnerException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
