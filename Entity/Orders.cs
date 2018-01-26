using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise.Entity
{
    /// <summary>
    /// Orders object containing list of seating requests and corresponding
    /// seating assignments if the seats are approved.
    /// </summary>
    public class Orders
    {
        public Orders()
        {
            SeatingRequests = new List<SeatingRequest>();
            SeatingAssignments = new List<SeatingAssignment>();
        }

        public List<SeatingRequest> SeatingRequests { get; set; }

        public List<SeatingAssignment> SeatingAssignments { get; set; }
    }
}
