using System;


namespace SharePointCSOM
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var outputPath = @"C:\users\hammonks\desktop\projects\Goal - Documentation documentation\SW Doc Files" + DateTime.Now.ToString("_MMddyyyy_hhmm") + ".csv";
            var listTitle = "Software Documentation";
            var siteUrl = "";
            string[] fields = { };
            SharePointService.ExportListToCSV(listTitle, outputPath, siteUrl, fields);
        }
    }
}