using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise.Entity
{
    /// <summary>
    /// Row object designating an individual row in the theatre.
    /// </summary>
    public class Row
    {
        public Row()
        {
            Sections = new List<Section>();
        }

        public int RowNumber { get; set; }

        public List<Section> Sections { get; set; }

        public int SeatsAvailable
        {
            get { return Sections.Sum(x => x.SeatsAvailable); }
        }

        public int LargestPartySizeAvailable
        {
            get { return Sections.Max(x => x.SeatsAvailable); }
        }

        public bool IsSoldOut => Sections.All(x => x.IsSoldOut);
    }
}
