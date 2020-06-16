using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Movies.Domain.RatingTypes
{
    public class RatingCreateCommand
    {
        [Required(ErrorMessage = "O campo MovieID é obrigatório.")]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "O campo MovieRating é obrigatório.")]
        public float? MovieRating { get; set; }

        public Rating ToRating()
        {
            return new Rating
            {
                MovieRating = MovieRating,
                MovieID = MovieID
            };
        }
    }
}
