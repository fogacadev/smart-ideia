using SmartIdeia.Src.Modules.Ratings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.IdeaRating.Entities
{
    public class IdeaRating
    {
        public long Id { get; set; }
        public decimal Points { get; set; }
        public Rating Rating { get; set; }
        public long RatingId { get; set; }
    }
}
