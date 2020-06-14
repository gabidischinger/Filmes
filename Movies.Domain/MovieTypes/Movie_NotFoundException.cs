using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.MovieTypes
{
    public class Movie_NotFoundException : AbstractMovieException
    {
        public Movie_NotFoundException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
