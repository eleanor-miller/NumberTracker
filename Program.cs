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

            var numbers = new List<int>();

            // If the file exists
            if (File.Exists("numbers.csv"))
            {
                // Create a file reader to read from numbers.csv
                var fileReader = new StreamReader("numbers.csv");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };

                var csvReader = new CsvReader(fileReader, config);

                numbers = csvReader.GetRecords<int>().ToList();

                var isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("------------------");
                    foreach (var number in numbers)
                    {
                        Console.WriteLine(number);
                    }
                    Console.WriteLine($"Our list has: {numbers.Count()} entries");
                    Console.WriteLine("------------------");
                    Console.Write("Enter a number to store, or 'quit' to end: ");
                    var input = Console.ReadLine().ToLower();

                    if (input == "quit")
                    {
                        isRunning = false;
                    }
                    else
                    {
                        var number = int.Parse(input);
                        numbers.Add(number);
                    }
                }

                var fileWriter = new StreamWriter("numbers.csv");

                var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

                csvWriter.WriteRecords(numbers);

                fileWriter.Close();


            }
        }
    }
}