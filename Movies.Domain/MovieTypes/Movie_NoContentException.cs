using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.MovieTypes
{
    public class Movie_NoContentException : AbstractMovieException
    {
        public Movie_NoContentException(string message, int blogID, int userID) : base(message, blogID, userID)
        {
        }
    }
}
