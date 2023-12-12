using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class TopScorers
{
    static void Main()
    {
        // Get the path where the program is running
        string executablePath = AppDomain.CurrentDomain.BaseDirectory;

        // Set the working directory to the project root
        Directory.SetCurrentDirectory(Path.GetDirectoryName(executablePath));

        // Construct the path to the 'TestData.csv' file
        string filePath = Path.Combine(executablePath, "TestData.csv");

        // Normalize the path to handle any relative references or navigation
        filePath = Path.GetFullPath(filePath);

        // Read the content of the CSV file into an array of lines
        string[] lines = File.ReadAllLines(filePath);

        // Parse the CSV data into a list of tuples (Name, Score)
        List<(string Name, int Score)> data = new List<(string, int)>();
        foreach (string line in lines)
        {
            // Split each line into parts using the comma as a separator
            string[] parts = line.Split(',');

            // Check if the line has the expected format with two parts
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
            {
                // Add the parsed data to the list
                data.Add((parts[0], score));
            }
            else
            {
                // Print a message for any invalid data
                Console.WriteLine($"Invalid data: {line}");
            }
        }

        // Check if the data list is not empty before finding the top scorers
        if (data.Any())
        {
            // Find the top scorers with the highest scores
            int maxScore = data.Max(x => x.Score);
            var topScorers = data.Where(x => x.Score == maxScore).OrderBy(x => x.Name);

            // Output the results showing the top scorers and their scores
            Console.WriteLine($"Top Scorers:");
            foreach (var scorer in topScorers)
            {
                Console.WriteLine($"{scorer.Name}: {scorer.Score}");
            }
        }
        else
        {
            // Print a message if no valid data is found in the CSV file
            Console.WriteLine("No data found. Ensure that the CSV file contains valid entries.");
        }
    }
}
