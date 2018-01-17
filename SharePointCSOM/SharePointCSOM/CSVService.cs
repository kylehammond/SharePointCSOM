using System.Collections.Generic;
using System.IO;
using SharePointCSOM;

public static class CSVService
{
    public static void WriteCSV(IEnumerable<string> headerValues, IEnumerable<IEnumerable<string>> outputLines,
        string outputFilePath)
    {
        using (var writer = new StreamWriter(outputFilePath))
        {
            foreach (var outputLine in outputLines) writer.WriteLine(GetFormattedLine(outputLine));
        }
    }

    public static string GetFormattedLine(IEnumerable<string> values)
    {
        var lineValues = new List<string>();
        foreach (var value in values) lineValues.Add(CSV.Quote + value + CSV.Quote);
        return string.Join(",", lineValues);
    }
}