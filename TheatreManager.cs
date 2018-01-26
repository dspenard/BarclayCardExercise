using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BarclayCardExercise
{
    /// <summary>
    /// TheatreManager is the information expert object which encapsulates
    /// responsibilities for seating layout definition and seating assignment.
    /// </summary>
    public class TheatreManager
    {
        public Entity.TheatreSeating TheatreSeating { get; set; }

        public Entity.Orders Orders { get; set; }
        
        public TheatreManager()
        {
            TheatreSeating = new Entity.TheatreSeating();
            Orders = new Entity.Orders();
        }

        /// <summary>
        /// Process the given file name to extract theatre layout info
        /// and seating request info.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ProcessInputFile(string fileName)
        {
            var fileManager = new FileManager();
            fileManager.ProcessFile(fileName);

            // for simplicity sake I'm just writing to console here for these validation messages;
            // a more robust system would do things differently

            if (!fileManager.IsFileProcessed)
            {
                Console.WriteLine("File could not be processed.");
                return false;
            }

            if (!fileManager.HasValidTheatreSeatingLayout)
            {
                Console.WriteLine("File has invalid or missing theatre seating layout records.");
                return false;
            }

            if (!fileManager.HasValidSeatingRequests)
            {
                Console.WriteLine("File has invalid or missing seating request records.");
                return false;
            }

            var rowNumber = 1;
            fileManager.TheatreSeatingItems.ForEach(x => AddTheatreLayoutItem(x, rowNumber++));

            fileManager.SeatingRequestItems.ForEach(x => AddSeatingRequestItem(x));

            return true;
        }

        /// <summary>
        /// Process the customer orders (the seating requests).
        /// </summary>
        public void ProcessOrders()
        {
            foreach (var seatingRequest in Orders.SeatingRequests)
            {
                var seatingAssignment = new Entity.SeatingAssignment()
                {
                    Customer = new Entity.Customer()
                    {
                        Name = seatingRequest.Customer.Name
                    }
                };

                // check if no seats available or if theatre does not have enough seats at all
                if (TheatreSeating.IsSoldOut || TheatreSeating.SeatsAvailable < seatingRequest.TotalTicketsRequested)
                {
                    seatingAssignment.AvailabilityMessage = CustomerMessages.PartyTooLarge;
                }

                // check if any seats available in any section which can handle the entire party
                else if (TheatreSeating.LargestPartySizeAvailable < seatingRequest.TotalTicketsRequested)
                {
                    seatingAssignment.AvailabilityMessage = CustomerMessages.PartySplitRequired;
                }

                // find a section with enough seats and assign them
                else
                {
                    AssignSeats(seatingAssignment, seatingRequest.TotalTicketsRequested);
                }

                Orders.SeatingAssignments.Add(seatingAssignment);
            };
        }

        /// <summary>
        /// Write the seating assignment info to the console.
        /// </summary>
        public void ShowProcessedOrders()
        {
            Console.WriteLine("Seating Assignments:");

            Orders.SeatingAssignments.ForEach(x =>
            {
                if (x.HasAssignedSeats)
                    Console.WriteLine($"{x.Customer.Name}: Row {x.Row} Section {x.Section}, {x.TotalTickets} tickets");
                else
                    Console.WriteLine($"{x.Customer.Name}: {x.AvailabilityMessage}");
            });

            Console.WriteLine();
        }

        /// <summary>
        /// Write the seating availability info to the console.
        /// </summary>
        public void ShowSeatingChart()
        {
            Console.WriteLine("Seats Available:");

            TheatreSeating.Rows.ForEach(row =>
            {
                Console.Write($"Row {row.RowNumber}: ");

                row.Sections.ForEach(section =>
                    Console.Write($"{section.SeatsAvailable} "));

                Console.WriteLine();
            });

            Console.WriteLine();
        }

        /// <summary>
        /// For the given theatre layout record, add a row to the TheatreSeating Rows collection.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="rowNumber"></param>
        private void AddTheatreLayoutItem(string record, int rowNumber)
        {
            // for simplicity of this exercise I'm assuming the record is valid with format such as: 3 5 5 3
            var sectionSeats = record.Split(new Char[] { ' ' }).ToList();

            var row = new Entity.Row()
            {
                RowNumber = rowNumber
            };

            var sectionNumber = 1;

            sectionSeats.ForEach(
                x => row.Sections.Add(
                    new Entity.Section()
                    {
                        SectionNumber = sectionNumber++,
                        SeatsAvailable = int.Parse(x)
                    }
                )
            );

            TheatreSeating.Rows.Add(row);
        }

        /// <summary>
        /// For the given seating request record, add a seating request to the Orders SeatingRequest collection.
        /// </summary>
        /// <param name="record"></param>
        private void AddSeatingRequestItem(string record)
        {
            // for simplicity of this exercise I'm assuming the record is valid with format such as: Smith 3
            var customerItem = record.Split(new Char[] { ' ' }).ToList();

            var customer = new Entity.Customer()
            {
                Name = customerItem[0]
            };

            Orders.SeatingRequests.Add(
                new Entity.SeatingRequest()
                {
                    Customer = customer,
                    TotalTicketsRequested = int.Parse(customerItem[1])
                }
            );
        }

        /// <summary>
        /// For the given seating assignment object, find a theatre row and section that can
        /// accomodate the total number of requested tickets.
        /// </summary>
        /// <param name="seatingAssignment"></param>
        /// <param name="totalTicketsRequested"></param>
        private void AssignSeats(Entity.SeatingAssignment seatingAssignment, int totalTicketsRequested)
        {
            // get a list of possible rows with at least one section with enough seats
            var possibleRows = TheatreSeating.Rows
                .Where(x => x.LargestPartySizeAvailable >= totalTicketsRequested)
                .OrderBy(y => y.RowNumber).ToList();

            foreach (var row in possibleRows)
            {
                // find a section with the least number of seats needed
                var section = row.Sections
                    .OrderBy(x => x.SeatsAvailable)
                    .FirstOrDefault(y => y.SeatsAvailable >= totalTicketsRequested);

                // seats are available
                if (section != null)
                {
                    seatingAssignment.Row = row.RowNumber;
                    seatingAssignment.Section = section.SectionNumber;
                    seatingAssignment.TotalTickets = totalTicketsRequested;
                    seatingAssignment.AvailabilityMessage = CustomerMessages.PartyAssigned;

                    section.SeatsAvailable -= totalTicketsRequested;

                    break;
                }

                // seats are not available
                else
                {
                    seatingAssignment.AvailabilityMessage = CustomerMessages.PartySplitRequired;
                }
            }
        }
    }
}
