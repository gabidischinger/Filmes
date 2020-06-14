using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.ReviewTypes
{
    public abstract class AbstractReviewException : Exception
    {
        public int MovieID { get; set; }
        public int UserID { get; set; }

        public AbstractReviewException(string message, int movieID, int userID) : base(message)
        {
            this.MovieID = movieID;
            this.UserID = userID;
        }
    }
}
