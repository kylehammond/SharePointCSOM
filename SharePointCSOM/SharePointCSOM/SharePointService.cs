using System.Collections.Generic;

namespace SharePointCSOM
{
    public static class SharePointService
    {
        public static void ExportListToCSV(string listTitle, string outputFilePath, string siteUrl, string[] fields)
        {
            using (var spoClient = new SharePointOnlineClient(siteUrl, Credentials.UserName, Credentials.Password))
            {
                var listItems = spoClient.GetListItemsByTitle(listTitle);
                var outputLines = new List<List<string>>();

                // build and write header
                var headerValues = new List<string> {"ID", "DisplayName"}; // add default
                headerValues.AddRange(fields); // add non-default
                outputLines.Add(headerValues);

                // build and write outputLines
                foreach (var listItem in listItems)
                {
                    var fieldValues = new List<string> {listItem[SharePointInternalFields.ID].ToString(), listItem.DisplayName}; // add default
                    foreach (var field in fields) fieldValues.Add(listItem[field]?.ToString()); // add non-default
                    outputLines.Add(fieldValues);
                }

                // create the file
                CSVService.WriteCSV(headerValues, outputLines, outputFilePath);
            }
        }
    }
}