using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BarclayCardExercise
{
    /// <summary>
    /// FileManager is responsible for reading an input file and parsing
    /// out a list of items for the theatre seating layout and a list of
    /// items for the seating requests.
    /// </summary>
    public class FileManager
    {
        public List<string> TheatreSeatingItems { get; set; }

        public List<string> SeatingRequestItems { get; set; }

        public bool HasValidTheatreSeatingLayout => TheatreSeatingItems != null && TheatreSeatingItems.Any();

        public bool HasValidSeatingRequests => SeatingRequestItems != null && SeatingRequestItems.Any();

        public bool IsFileProcessed { get; set; }

        public FileManager()
        {
            Init();
        }

        public bool ProcessFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                HandleErrorMessage("File name is empty.");
            }

            try
            {
                var records = File.ReadAllLines(fileName).ToList();
                ExtractSeatingItems(records);
                IsFileProcessed = true;
            }
            catch (Exception ex)
            {
                HandleErrorMessage(ex.Message);
            }

            return IsFileProcessed;
        }

        private void Init()
        {
            TheatreSeatingItems = new List<string>();
            SeatingRequestItems = new List<string>();
            IsFileProcessed = false;
        }

        private void ExtractSeatingItems(List<string> records)
        {
            var isLayoutRecord = true;

            foreach (var record in records)
            {
                // for simplicity, I'm not doing the most robust validation and I'm
                // assuming there is only one blank line in the file, and it's between
                // the seating layout records and the seating request records
                if (string.IsNullOrEmpty(record))
                {
                    isLayoutRecord = false;
                    continue;
                }

                if (isLayoutRecord)
                {
                    TheatreSeatingItems.Add(record);
                }
                else
                {
                    SeatingRequestItems.Add(record);
                }
            }
        }

        private void HandleErrorMessage(string message)
        {
            // for simplicity sake I'm just writing to console here for these messages;
            // a more robust system would do things differently

            Console.WriteLine(message);
            Init();
        }
    }
}
