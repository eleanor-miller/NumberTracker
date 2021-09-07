# NumberTracker

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace NumberTracker
{
class Program
{
static void Main(string[] args)
{
Console.WriteLine("Welcome to Number Tracker");

            // Create a file reader to read from numbers.csv
            var fileReader = new StreamReader("numbers.csv");

            // Create a configuration that indicates this CSV file has no header
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Tell the reader not to interpret the first
                // row as a "header" since it is just the first number
                HasHeaderRecord = false,
            };

            var csvReader = new CsvReader(fileReader, config);


            // - Creates our List<int> but from *READING* the data from csvReader
            var numbers = csvReader.GetRecords<int>().ToList();

            // Controls if we are still running our loop asking for more numbers
            var isRunning = true;

            // While we are running
            while (isRunning)
            {
                // Show the list of numbers
                Console.WriteLine("------------------");
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine($"Our list has: {numbers.Count()} entries");
                Console.WriteLine("------------------");

                // Ask for a new number or the word quit to end
                Console.Write("Enter a number to store, or 'quit' to end: ");
                var input = Console.ReadLine().ToLower();

                if (input == "quit")
                {
                    // If the input is quit, turn off the flag to keep looping
                    isRunning = false;
                }
                else
                {
                    // Parse the number and add it to the list of numbers
                    var number = int.Parse(input);
                    numbers.Add(number);
                }
            }

/_ A new stream of information to WRITER  
 |  
 | Name of the file to write to
| |  
 | |
v v  
_/
var fileWriter = new StreamWriter("numbers.csv");

/\* A new stream of CSV data

                          fileWriter = stream to write to , Culture...=Rules about formatting

\*/
var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

         // where to write         what date to write
            csvWriter.WriteRecords(numbers);

         // tell the object that writes to the file, that we are done and close
            fileWriter.Close();


        }
    }

}
