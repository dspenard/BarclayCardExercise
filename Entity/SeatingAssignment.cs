using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise.Entity
{
    /// <summary>
    /// Object holding seating assignment details for a customer.
    /// </summary>
    public class SeatingAssignment
    {
        public SeatingAssignment()
        {
        }

        public Customer Customer { get; set; }

        public int TotalTickets { get; set; }

        public int Row { get; set; }

        public int Section { get; set; }

        public string AvailabilityMessage { get; set; }

        public bool HasAssignedSeats => Row > 0 && Section > 0;
    }
}
