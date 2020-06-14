using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Movies.Domain.MovieTypes
{
    public class MovieCreateCommand
    {
        [Required(ErrorMessage = "O campo Title é obrigatório.")]
        [MinLength(10, ErrorMessage = "O campo Title tem tamanho mínimo de 10 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Title tem tamanho máximo de 250 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo Description é obrigatório.")]
        [MinLength(10, ErrorMessage = "O campo Description tem tamanho mínimo de 10 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Description tem tamanho máximo de 250 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo Year é obrigatório.")]
        [MinLength(10, ErrorMessage = "O campo Year tem tamanho mínimo de 10 caracteres.")]
        [MaxLength(250, ErrorMessage = "O campo Year tem tamanho máximo de 250 caracteres.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "O campo Genre é obrigatório.")]
        [MinLength(10, ErrorMessage = "O campo Genre tem tamanho mínimo de 10 caracteres.")]
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
