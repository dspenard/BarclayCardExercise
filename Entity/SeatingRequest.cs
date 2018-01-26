using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise.Entity
{
    /// <summary>
    /// Object holding seating request details for a customer.
    /// </summary>
    public class SeatingRequest
    {
        public SeatingRequest() { }

        public Customer Customer { get; set; }

        public int TotalTicketsRequested { get; set; }
    }
}
