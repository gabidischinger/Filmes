using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.MovieTypes
{
    public abstract class AbstractMovieException : Exception
    {
        public int MovieID { get; set; }
        public int UserID { get; set; }

        public AbstractMovieException(string message, int movieID, int userID) : base(message)
        {
            this.MovieID = movieID;
            this.UserID = userID;
        }
    }
}
