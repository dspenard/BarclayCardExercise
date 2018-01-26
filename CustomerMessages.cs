using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise
{
    /// <summary>
    /// This class is merely for convenience.  These properties
    /// would normally be pulled from configuration or a table
    /// or something similar.
    /// </summary>
    public static class CustomerMessages
    {
        public static string PartyAssigned => "Enjoy the show!";

        public static string PartyTooLarge => "Sorry, we can't handle your party.";

        public static string PartySplitRequired => "Call to split party."; 
    }
}
