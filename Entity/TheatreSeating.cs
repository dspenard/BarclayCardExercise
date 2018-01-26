using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise.Entity
{
    /// <summary>
    /// Object containing theatre layout details including info on rows of seats
    /// and corresponding seat availability.
    /// </summary>
    public class TheatreSeating
    {
        public TheatreSeating()
        {
            Rows = new List<Row>();
        }

        public List<Row> Rows { get; set; }

        public int SeatsAvailable
        {
            get { return Rows.Sum(x => x.SeatsAvailable); }
        }

        public int LargestPartySizeAvailable
        {
            get { return Rows.Max(x => x.LargestPartySizeAvailable); }
        }

        public bool IsSoldOut => SeatsAvailable == 0;
    }
}
