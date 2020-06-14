using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.RatingTypes
{
    public abstract class AbstractRatingException : Exception
    {
        public int MovieID { get; set; }
        public int UserID { get; set; }

        public AbstractRatingException(string message, int movieID, int userID) : base(message)
        {
            this.MovieID = movieID;
            this.UserID = userID;
        }
    }
}
