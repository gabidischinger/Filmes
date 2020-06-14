using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.RatingTypes
{
    public class Rating_NoContentException : AbstractRatingException
    {
        public Rating_NoContentException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
