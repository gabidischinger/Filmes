using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.ReviewTypes
{
    public class Review_NotFoundException : AbstractReviewException
    {
        public Review_NotFoundException(string message, int movieID, int userID) : base(message, movieID, userID)
        {
        }
    }
}
