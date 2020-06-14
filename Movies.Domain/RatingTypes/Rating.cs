using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain
{
    public class Rating
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public float? MovieRating { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
