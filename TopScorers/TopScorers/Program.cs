using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class TopScorers
{
    static void Main()
    {
        // Get the directory of the executable (where the .exe is located)
        string executablePath = AppDomain.CurrentDomain.BaseDirectory;

        // Construct the path to TestData.csv using Path.Combine
        string filePath = Path.Combine(executablePath, "..", "..", "TestData.csv");

        // Normalize the path to handle any ".." or "." references
        filePath = Path.GetFullPath(filePath);

        // Read the CSV file
        string[] lines = File.ReadAllLines(filePath);

        // Parse the CSV data
        List<(string Name, int Score)> data = new List<(string, int)>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
            {
                data.Add((parts[0], score));
            }
            else
            {
                Console.WriteLine($"Invalid data: {line}");
            }
        }

        // Check if data is not empty before finding the top scorers
        if (data.Any())
        {
            // Find the top scorers
            int maxScore = data.Max(x => x.Score);
            var topScorers = data.Where(x => x.Score == maxScore).OrderBy(x => x.Name);

            // Output the results
            Console.WriteLine($"Top Scorers:");
            foreach (var scorer in topScorers)
            {
                Console.WriteLine($"{scorer.Name}: {scorer.Score}");
            }
        }
        else
        {
            Console.WriteLine("No data found. Ensure that the CSV file contains valid entries.");
        }
    }
}
