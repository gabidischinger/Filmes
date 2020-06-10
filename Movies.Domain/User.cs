using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
