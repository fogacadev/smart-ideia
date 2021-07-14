using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Ratings.Entities
{
    public class Rating
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public RatingGroup RatingGroup { get; set; }
        public long RatingGroupId { get; set; }
        public decimal Points { get; set; }
    }
}
