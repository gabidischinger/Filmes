using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.ReviewTypes
{
    public class Review_InvalidOwnerException : AbstractReviewException
    {
        public Review_InvalidOwnerException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
