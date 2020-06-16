using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Movies.Domain.ReviewTypes
{
    public class ReviewCreateCommand
    {
        [Required(ErrorMessage = "O campo MovieID é obrigatório.")]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "O campo Title é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Title tem tamanho mínimo de 3 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Title tem tamanho máximo de 250 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo Content é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Content tem tamanho mínimo de 3 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Content tem tamanho máximo de 250 caracteres.")]
        public string Content { get; set; }

        public Review ToReview()
        {
            return new Review
            {
                Title = Title,
                Content = Content,
                MovieID = MovieID
            };
        }
    }
}
