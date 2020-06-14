using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.ReviewTypes
{
    public class Review_NoContentException : AbstractReviewException
    {
        public Review_NoContentException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
