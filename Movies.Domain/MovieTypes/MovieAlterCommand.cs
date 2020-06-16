using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Movies.Domain.MovieTypes
{
    public class MovieAlterCommand
    {
        [MinLength(3, ErrorMessage = "O campo Title tem tamanho mínimo de 3 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Title tem tamanho máximo de 250 caracteres.")]
        public string Title { get; set; }

        [MinLength(3, ErrorMessage = "O campo Description tem tamanho mínimo de 3 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Description tem tamanho máximo de 250 caracteres.")]
        public string Description { get; set; }

        public int? Year { get; set; }

        [MinLength(3, ErrorMessage = "O campo Genre tem tamanho mínimo de 3 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Genre tem tamanho máximo de 250 caracteres.")]
        public string Genre { get; set; }

        public Movie ToMovie()
        {
            return new Movie
            {
                Title = Title,
                Description = Description,
                Year = Year,
                Genre = Genre
            };
        }
    }
}
