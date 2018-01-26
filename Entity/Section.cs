using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise.Entity
{
    /// <summary>
    /// Section object designating an individual section in the theatre.
    /// </summary>
    public class Section
    {
        public Section()
        {
        }

        public int SectionNumber { get; set; }

        public int SeatsAvailable { get; set; }

        public bool IsSoldOut => SeatsAvailable == 0;
    }
}
