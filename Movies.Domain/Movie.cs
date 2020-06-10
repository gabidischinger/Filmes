using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Domain
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int UserID { get; set; }
        public float AverageRating { 
            get { return this.Ratings.Average(r => r.MovieRating); } 
        }
        public virtual User User { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
