using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclayCardExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Count() == 0)
                {
                    Console.WriteLine("Please enter the full path name for a valid input file.");
                    return;
                }

                var theatreManager = new TheatreManager();

                var isGoodFile = theatreManager.ProcessInputFile(args[0]);

                if (isGoodFile)
                {
                    theatreManager.ShowSeatingChart();
                    theatreManager.ProcessOrders();
                    theatreManager.ShowProcessedOrders();
                    theatreManager.ShowSeatingChart();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
