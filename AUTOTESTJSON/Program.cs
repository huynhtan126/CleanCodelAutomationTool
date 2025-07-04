using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AUTOTESTJSON
{
    internal class Program
    {
        #region Test API post

        //public static async Task Main(string[] args)
        //{
        //    ApiComparer comparer = new ApiComparer();

        //    // 1. Define API URL
        //    string apiUrl = "https://jsonplaceholder.typicode.com/todos/1"; // Example public API

        //    // 2. Define path to the expected JSON file
        //    // Ensure this file exists and contains valid JSON content
        //    string expectedJsonFilePath = "expectedResponse.json";

        //    // Create a sample JSON file if it doesn't exist (for demonstration purposes only)
        //    // In a real scenario, you would have your pre-defined JSON file.
        //    if (!File.Exists(expectedJsonFilePath))
        //    {
        //        // Changed "completed" to true for demonstration of difference
        //        string sampleJsonContent = "{\n  \"userId\": 1,\n  \"id\": 1,\n  \"title\": \"delectus aut autem\",\n  \"completed\": true\n}";
        //        File.WriteAllText(expectedJsonFilePath, sampleJsonContent);
        //        Console.WriteLine($"Created sample file '{expectedJsonFilePath}'.");
        //    }

        //    // Call the API
        //    string apiResponseJson = await comparer.CallApiAsync(apiUrl);

        //    // Read the content of the JSON file
        //    string expectedJsonContent = comparer.ReadJsonFile(expectedJsonFilePath);

        //    // Compare and list differences
        //    if (apiResponseJson != null && expectedJsonContent != null)
        //    {
        //        _ = comparer.CompareAndListDifferences(apiResponseJson, expectedJsonContent);
        //    }
        //    else
        //    {
        //        Console.WriteLine("\nComparison could not be performed due to errors calling API or reading JSON file.");
        //    }

        //    Console.WriteLine("\nPress any key to exit...");
        //    _ = Console.ReadKey();
        //}

        #endregion

        public class ChatApiClient
        {
            #region Get expected json Input number

            //public static async Task Main(string[] args)
            //{
            //    Console.WriteLine("Enter a row number excel to update expected json");

            //    StringBuilder numericInput = new StringBuilder();
            //    ConsoleKeyInfo keyInfo;

            //    do
            //    {
            //        keyInfo = Console.ReadKey(true); // Read key without displaying it

            //        // Check if the key is a digit (0-9)
            //        if (char.IsDigit(keyInfo.KeyChar))
            //        {
            //            numericInput.Append(keyInfo.KeyChar);
            //            Console.Write(keyInfo.KeyChar); // Display the digit back to the console
            //        }
            //        // Handle Backspace
            //        else if (keyInfo.Key == ConsoleKey.Backspace && numericInput.Length > 0)
            //        {
            //            numericInput.Remove(numericInput.Length - 1, 1);
            //            Console.Write("\b \b"); // Move cursor back, overwrite with space, move back again
            //        }
            //        // Ignore other keys except Enter
            //    } while (keyInfo.Key != ConsoleKey.Enter);

            //    Console.WriteLine();
            //    var number = 3;
            //    if (numericInput.Length > 0)
            //    {
            //        if (int.TryParse(numericInput.ToString(), out number))
            //        {
            //            //Console.WriteLine($"You entered the number: {number}");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Error: Could not parse the entered number.");
            //            Console.WriteLine("\nPress any key to exit.");
            //            Console.ReadKey(true);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No number was entered.");
            //        Console.WriteLine("\nPress any key to exit.");
            //        Console.ReadKey(true);
            //    }


            //    var minvalue = number;
            //    var hangTangdan = number - 1;
            //    #region Initail

            //    var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //    var fileor = new FileInfo(pathfolder);
            //    pathfolder = fileor.Directory.ToString();
            //    var thongtinfile = pathfolder + "\\TemplateReport.xlsx";
            //    string FileApiUrl = pathfolder + "\\URLTest.txt";
            //    var apiUrl = File.ReadAllText(FileApiUrl).Trim();

            //    var folderJson = pathfolder + @"\ExpectJSON\AiHolding\AiCad-sf\";

            //    #endregion

            //    if (!File.Exists(thongtinfile)) { Console.WriteLine("Not found template file."); Console.ReadKey(); return; }
            //    ExcelPackage package = new ExcelPackage(new FileInfo(thongtinfile));
            //    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            //    // get number of rows and columns in the sheet
            //    var maxvalue = worksheet.Dimension.Rows;
            //    var previousNumber = "unknow";
            //    for (int i = minvalue; i <= minvalue; i++)
            //    {
            //        try
            //        {
            //            hangTangdan++;
            //            var IsPass = worksheet.Cells[hangTangdan, 12].Value.ToString();
            //            if (IsPass != "Pass") continue;
            //            var TCNumber = worksheet.Cells[hangTangdan, 2].Value;
            //            if (TCNumber != null)
            //            {
            //                previousNumber = TCNumber.ToString();

            //            }
            //            else
            //            {
            //                Console.ReadKey();
            //                continue;
            //            }



            //            var testCaseName = "TC" + previousNumber;

            //            var question = worksheet.Cells[hangTangdan, 4].Value?.ToString();
            //            var listRequest = new List<string>();
            //            listRequest.Add(question);
            //            //listRequest.Add("fe9e982f-fbf0-41c3-90c2-da103767f7e1");
            //            listRequest.Add("AiHoldings");

            //            //string requestBody = "{\"question\":\"" + question + "\",\"thread_id\":\"fe9e982f-fbf0-41c3-90c2-da103767f7e1\",\"project\":\"AiHoldings\"}";

            //            string requestBody = File.ReadAllText(pathfolder + "\\FormatBodyRequest.txt");
            //            for (int j = 0; j < listRequest.Count; j++)
            //            {
            //                requestBody = requestBody.Replace("$$J" + j, listRequest[j]);

            //            }
            //            var jsonExpect = folderJson + testCaseName + ".json";


            //            using (HttpClient client = new HttpClient())
            //            {
            //                try
            //                {
            //                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            //                    // Send POST request
            //                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            //                    // Ensure the request was successful
            //                    response.EnsureSuccessStatusCode();

            //                    // Read the entire response as a string
            //                    string apiResponseString = await response.Content.ReadAsStringAsync();


            //                    // Parse JSON from API
            //                    JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponseString);
            //                    JsonElement apiRoot = apiJsonDocument.RootElement;
            //                    File.WriteAllText(jsonExpect, apiRoot.GetRawText().Trim());
            //                    Console.WriteLine(jsonExpect);
            //                    Process.Start(jsonExpect);

            //                }
            //                catch (HttpRequestException e)
            //                {
            //                    Console.WriteLine(testCaseName + $" HTTP Error: {e.Message}");
            //                }
            //                catch (System.Text.Json.JsonException e)
            //                {
            //                    Console.WriteLine(testCaseName + $" JSON Parsing Error: {e.Message}");
            //                }
            //                catch (Exception e)
            //                {
            //                    Console.WriteLine(testCaseName + $" An error occurred: {e.Message}");
            //                }
            //                finally
            //                {
            //                    //Console.ReadKey();
            //                }
            //            }
            //        }
            //        catch (Exception EX)
            //        {


            //        }

            //        Console.ReadKey();
            //    }


            //}
            #endregion
            #region test 1 work 
            //public static async Task Main(string[] args)
            //{
            //    string apiUrl = "https://chatbot-api.tgl-cloud.com/api/v1/chat/chat/full";
            //    var testCaseName = "TC1";
            //    var folderJson = @"C:\TGL\NewCAD\SOURCE_\newcad-chatbot\ExpectJSON\AiHolding\AiCad-sf\";
            //    var question = "胴縁配置面（フレーム）を作成したいです。";


            //    string requestBody = "{\"question\":\"" + question + "\",\"thread_id\":\"fe9e982f-fbf0-41c3-90c2-da103767f7e1\",\"project\":\"AiHoldings\"}";
            //    var jsonExpect = folderJson + testCaseName + ".json";
            //    // Predefined JSON for comparison (example)
            //    string predefinedJsonString = File.ReadAllText(jsonExpect);
            //    // Predefined JSON for comparison (example)
            //    //string predefinedJsonString = "{\"response\":\"This is the expected response from the API.\", \"status\":\"success\"}";

            //    using (HttpClient client = new HttpClient())
            //    {
            //        try
            //        {
            //            StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            //            // Send POST request
            //            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            //            // Ensure the request was successful
            //            response.EnsureSuccessStatusCode();

            //            // Read the entire response as a string
            //            string apiResponseString = await response.Content.ReadAsStringAsync();
            //            //Console.WriteLine($"Response from API:\n{apiResponseString}\n");

            //            // --- JSON Comparison ---

            //            // Parse JSON from API
            //            JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponseString);
            //            JsonElement apiRoot = apiJsonDocument.RootElement;

            //            // Parse predefined JSON
            //            //JsonDocument predefinedJsonDocument = JsonDocument.Parse(predefinedJsonString);
            //            //JsonElement predefinedRoot = predefinedJsonDocument.RootElement;

            //            // Compare the two JsonElement objects
            //            // Note: The 'JsonElement.GetRawText()' method compares raw JSON strings,
            //            // which might not be sufficient if property order differs but content is the same.
            //            // For a deeper comparison, you would need to compare individual properties or deserialize into specific C# objects.

            //            StringComparer.CompareAndShowDifferencesWithZip(apiRoot.GetRawText().Trim(), predefinedJsonString.Trim(), testCaseName);

            //        }
            //        catch (HttpRequestException e)
            //        {
            //            Console.WriteLine($"HTTP Error: {e.Message}");
            //        }
            //        catch (System.Text.Json.JsonException e)
            //        {
            //            Console.WriteLine($"JSON Parsing Error: {e.Message}");
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine($"An error occurred: {e.Message}");
            //        }
            //        finally
            //        {
            //            Console.ReadKey();
            //        }
            //    }
            //}
            #endregion
            #region test excute
            //public static async Task Main(string[] args)
            //{

            //    var minvalue = 3;
            //    var isTotalTest = true;
            //    #region Initail

            //    var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //    var fileor = new FileInfo(pathfolder);
            //    pathfolder = fileor.Directory.ToString();
            //    var thongtinfile = pathfolder + "\\TemplateReport.xlsx";
            //    string FileApiUrl = pathfolder + "\\URLTest.txt";
            //    var apiUrl = File.ReadAllText(FileApiUrl).Trim();

            //    var folderJson = pathfolder + @"\ExpectJSON\AiHolding\AiCad-sf\";

            //    #endregion

            //    if (!File.Exists(thongtinfile)) { Console.WriteLine("Not found template file."); return; }
            //    ExcelPackage package = new ExcelPackage(new FileInfo(thongtinfile));
            //    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            //    // get number of rows and columns in the sheet
            //    var maxvalue = worksheet.Dimension.Rows;
            //    var hangTangdan = 2;
            //    var previousNumber = "unknow";
            //    for (int i = minvalue; i <= maxvalue; i++)
            //    {
            //        hangTangdan++;
            //        var TCNumber = worksheet.Cells[hangTangdan, 2].Value;
            //        if (TCNumber != null)
            //        {
            //            previousNumber = TCNumber.ToString();
            //        }
            //        else
            //        {
            //            continue;
            //        }
            //        var testCaseName = "TC" + previousNumber;

            //        var question = worksheet.Cells[hangTangdan, 4].Value?.ToString();
            //        var threadID = Guid.NewGuid().ToString();

            //        //string requestBody = "{\"question\":\"" + question + "\",\"thread_id\":\"" + threadID + "\",\"project\":\"AiHoldings\"}";
            //        var jsonExpect = folderJson + testCaseName + ".json";

            //        var listRequest = new List<string>();
            //        listRequest.Add(question);
            //        //listRequest.Add("fe9e982f-fbf0-41c3-90c2-da103767f7e1");
            //        listRequest.Add("AiHoldings");

            //        string requestBody = File.ReadAllText(pathfolder + "\\FormatBodyRequest.txt");
            //        for (int j = 0; j < listRequest.Count; j++)
            //        {
            //            requestBody = requestBody.Replace("$$J" + j, listRequest[j]);

            //        }
            //        //string requestBody = "{\"question\":\"" + question + "\",\"thread_id\":\"fe9e982f-fbf0-41c3-90c2-da103767f7e1\",\"project\":\"AiHoldings\"}";

            //        // Predefined JSON for comparison (example)
            //        if (!File.Exists(jsonExpect)) { continue; }
            //        string predefinedJsonString = File.ReadAllText(jsonExpect);
            //        // Predefined JSON for comparison (example)
            //        //string predefinedJsonString = "{\"response\":\"This is the expected response from the API.\", \"status\":\"success\"}";

            //        using (HttpClient client = new HttpClient())
            //        {
            //            try
            //            {
            //                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            //                // Send POST request
            //                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            //                // Ensure the request was successful
            //                response.EnsureSuccessStatusCode();

            //                // Read the entire response as a string
            //                string apiResponseString = await response.Content.ReadAsStringAsync();
            //                //Console.WriteLine($"Response from API:\n{apiResponseString}\n");

            //                // --- JSON Comparison ---

            //                // Parse JSON from API
            //                JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponseString);
            //                JsonElement apiRoot = apiJsonDocument.RootElement;

            //                // Parse predefined JSON
            //                //JsonDocument predefinedJsonDocument = JsonDocument.Parse(predefinedJsonString);
            //                //JsonElement predefinedRoot = predefinedJsonDocument.RootElement;

            //                // Compare the two JsonElement objects
            //                // Note: The 'JsonElement.GetRawText()' method compares raw JSON strings,
            //                // which might not be sufficient if property order differs but content is the same.
            //                // For a deeper comparison, you would need to compare individual properties or deserialize into specific C# objects.

            //                if (StringComparer.CompareAndShowDifferencesWithZip(apiRoot.GetRawText().Trim(), predefinedJsonString.Trim(), testCaseName))
            //                {
            //                    isTotalTest = false;
            //                }

            //            }
            //            catch (HttpRequestException e)
            //            {
            //                Console.WriteLine($"HTTP Error: {e.Message}");
            //            }
            //            catch (System.Text.Json.JsonException e)
            //            {
            //                Console.WriteLine($"JSON Parsing Error: {e.Message}");
            //            }
            //            catch (Exception e)
            //            {
            //                Console.WriteLine($"An error occurred: {e.Message}");
            //            }
            //            finally
            //            {
            //                //Console.ReadKey();
            //            }
            //        }


            //    }

            //    Console.WriteLine($"Total test : " + isTotalTest);
            //}
            #endregion
            #region test all execute multi thread
            public static async Task Main(string[] args)
            {
                var minvalue = 3;
                // Use a ConcurrentBag to collect results from parallel tasks safely
                ConcurrentBag<bool> testResults = new ConcurrentBag<bool>();

                #region Initial Setup
                var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var fileor = new FileInfo(pathfolder);
                pathfolder = fileor.Directory.ToString();
                var thongtinfile = Path.Combine(pathfolder, "TemplateReport.xlsx");
                string FileApiUrl = Path.Combine(pathfolder, "URLTest.txt");

                if (!File.Exists(FileApiUrl))
                {
                    Console.WriteLine($"Error: URLTest.txt not found at {FileApiUrl}. Please create it with the API URL.");
                    Console.ReadKey(); // Pause to see error
                    return;
                }
                var apiUrl = File.ReadAllText(FileApiUrl).Trim();

                var folderJson = Path.Combine(pathfolder, @"ExpectJSON\AiHolding\AiCad-sf");
                if (!Directory.Exists(folderJson))
                {
                    Console.WriteLine($"Error: Expected JSON folder not found at {folderJson}. Please ensure it exists.");
                    Console.ReadKey(); // Pause to see error
                    return;
                }
                #endregion

                if (!File.Exists(thongtinfile))
                {
                    Console.WriteLine("Not found template file.");
                    Console.ReadKey(); // Pause to see error
                    return;
                }

                ExcelPackage package = null;
                ExcelWorksheet worksheet = null;

                try
                {
                    package = new ExcelPackage(new FileInfo(thongtinfile));
                    worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet == null)
                    {
                        Console.WriteLine("Error: No worksheet found in TemplateReport.xlsx.");
                        Console.ReadKey(); // Pause to see error
                        return;
                    }

                    var maxvalue = worksheet.Dimension.Rows;
                    var testCaseColumnNumber = 2;
                    var listAllTest = new List<Tuple<string, int>>();

                    listAllTest.Add(new Tuple<string, int> ("",3));


                    for (int k = minvalue; k <= maxvalue; k = k + 10)
                    {
                        Console.WriteLine($"Starting {k} potential API calls in parallel...");
                        List<Task> apiCallTasks = new List<Task>();
                        for (int i = k; i <= k + 10; i++)
                        {
                            // Capture loop variable for closure (crucial for async loops)
                            int currentRow = i;

                            // Read values from Excel. Use null-conditional operator and null-coalescing for robustness.
                            // Note: hangTangdan was incremented inside the loop. To match its behavior, if TCNumber
                            // is read from Cells[hangTangdan, 2] and hangTangdan started at 2 and incremented immediately,
                            // then for the first iteration (i=3), hangTangdan becomes 3, meaning Cells[3,2] is read.
                            // We'll adjust `currentRow` to reflect the actual row being processed.
                            // In your original code, `hangTangdan` starts at 2, then `hangTangdan++` makes it 3.
                            // So, `worksheet.Cells[hangTangdan, 2].Value` corresponds to `worksheet.Cells[currentRow, 2].Value` where `currentRow` starts at `minvalue`.
                            // Let's ensure we are reading from `currentRow` directly to avoid confusion.

                            var TCNumberRaw = worksheet.Cells[currentRow, testCaseColumnNumber].Value;
                            if (TCNumberRaw == null)
                            {
                                Console.WriteLine($"Skipping row {currentRow}: TCNumber (column B) is empty.");
                                continue; // Skip this row if TCNumber is null
                            }
                            var previousNumber = TCNumberRaw.ToString(); // This becomes the actual TCNumber
                            var testCaseName = "TC" + previousNumber; // Construct test case name
                    
                            var question = worksheet.Cells[currentRow, 4].Value?.ToString();

                            if (string.IsNullOrWhiteSpace(question))
                            {
                                Console.WriteLine($"Skipping row {currentRow}: Question (column D) is empty.");
                                continue; // Skip if question is empty
                            }

                            // Define the path for the expected JSON for this test case
                            var jsonExpectFilePath = Path.Combine(folderJson, $"{testCaseName}.json");

                            if (!File.Exists(jsonExpectFilePath))
                            {
                                Console.WriteLine($"Skipping row {currentRow} ({testCaseName}): Expected JSON file not found at {jsonExpectFilePath}.");
                                continue; // Skip if expected JSON file is missing
                            }

                            string predefinedJsonString = File.ReadAllText(jsonExpectFilePath).Trim();
                            var jsonExpect = folderJson + testCaseName + ".json";

                            var listRequest = new List<string>();
                            listRequest.Add(question);
                            //listRequest.Add("fe9e982f-fbf0-41c3-90c2-da103767f7e1");
                            listRequest.Add("AiHoldings");

                            string requestBody = File.ReadAllText(pathfolder + "\\FormatBodyRequest.txt");
                            for (int j = 0; j < listRequest.Count; j++)
                            {
                                requestBody = requestBody.Replace("$$J" + j, listRequest[j]);

                            }
                            // Add the task to the list
                            apiCallTasks.Add(Task.Run(async () => // Task.Run moves the async operation to a ThreadPool thread
                            {
                                using (HttpClient client = new HttpClient())
                                {
                                    try
                                    {
                                        StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                                        response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

                                        string apiResponseString = await response.Content.ReadAsStringAsync();

                                        JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponseString);
                                        JsonElement apiRoot = apiJsonDocument.RootElement;

                                        // Perform comparison and add result to concurrent bag
                                        bool testPassed = StringComparer.CompareAndShowDifferencesWithZip(apiRoot.GetRawText().Trim(), predefinedJsonString, testCaseName);
                                        testResults.Add(testPassed);
                                    }
                                    catch (HttpRequestException e)
                                    {
                                        lock (Console.Out)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"HTTP Error for {testCaseName} (Row {currentRow}): {e.Message}");
                                            Console.ResetColor();
                                        }
                                        testResults.Add(false); // Mark as failed due to HTTP error
                                    }
                                    catch (System.Text.Json.JsonException e)
                                    {
                                        lock (Console.Out)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"JSON Parsing Error for {testCaseName} (Row {currentRow}): {e.Message}");
                                            Console.ResetColor();
                                        }
                                        testResults.Add(false); // Mark as failed due to JSON parsing error
                                    }
                                    catch (Exception e)
                                    {
                                        lock (Console.Out)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"An unexpected error occurred for {testCaseName} (Row {currentRow}): {e.Message}");
                                            Console.ResetColor();
                                        }
                                        testResults.Add(false); // Mark as failed due to other error
                                    }
                                }
                            }));
                        }

                        // Wait for all tasks to complete
                        await Task.WhenAll(apiCallTasks);
                    }
                    // Determine overall test result
                    bool isTotalTestPassed = testResults.All(result => result == true);

                    Console.WriteLine("\n------------------------------------");
                    Console.WriteLine($"Total tests run: {testResults.Count}");
                    Console.ForegroundColor = isTotalTestPassed ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine($"Overall Test Result: {(isTotalTestPassed ? "PASSED" : "FAILED")}");
                    Console.ResetColor();
                    Console.WriteLine("------------------------------------");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nAn unhandled error occurred in Main execution: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    Console.ResetColor();
                }
                finally
                {
                    package?.Dispose(); // Dispose the ExcelPackage to release the file
                    Console.WriteLine("\nPress any key to exit.");
                    Console.ReadKey(); // Keep this one at the very end to pause the console.
                }
            }
            #endregion
            #region Get all execute json multi thread
            //public static async Task Main(string[] args)
            //{
            //    var minvalue = 3;
            //    // Use a ConcurrentBag to collect results from parallel tasks safely
            //    ConcurrentBag<bool> testResults = new ConcurrentBag<bool>();

            //    #region Initial Setup
            //    var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //    var fileor = new FileInfo(pathfolder);
            //    pathfolder = fileor.Directory.ToString();
            //    var thongtinfile = Path.Combine(pathfolder, "TemplateReport.xlsx");
            //    string FileApiUrl = Path.Combine(pathfolder, "URLTest.txt");

            //    if (!File.Exists(FileApiUrl))
            //    {
            //        Console.WriteLine($"Error: URLTest.txt not found at {FileApiUrl}. Please create it with the API URL.");
            //        Console.ReadKey(); // Pause to see error
            //        return;
            //    }
            //    var apiUrl = File.ReadAllText(FileApiUrl).Trim();

            //    var folderJson = Path.Combine(pathfolder, @"ExpectJSON\AiHolding\AiCad-sf");
            //    if (!Directory.Exists(folderJson))
            //    {
            //        Console.WriteLine($"Error: Expected JSON folder not found at {folderJson}. Please ensure it exists.");
            //        Console.ReadKey(); // Pause to see error
            //        return;
            //    }
            //    #endregion

            //    if (!File.Exists(thongtinfile))
            //    {
            //        Console.WriteLine("Not found template file.");
            //        Console.ReadKey(); // Pause to see error
            //        return;
            //    }

            //    ExcelPackage package = null;
            //    ExcelWorksheet worksheet = null;

            //    try
            //    {
            //        package = new ExcelPackage(new FileInfo(thongtinfile));
            //        worksheet = package.Workbook.Worksheets.FirstOrDefault();

            //        if (worksheet == null)
            //        {
            //            Console.WriteLine("Error: No worksheet found in TemplateReport.xlsx.");
            //            Console.ReadKey(); // Pause to see error
            //            return;
            //        }

            //        var maxvalue = worksheet.Dimension.Rows;

            //        // List to hold all the tasks for parallel execution
                    


            //        for (int k = minvalue; k <= maxvalue; k=k+10)
            //        {
            //            Console.WriteLine($"Starting {k } potential API calls in parallel...");
            //            List<Task> apiCallTasks = new List<Task>();
            //            for (int i = k; i <= k+10; i++)
            //            {
            //                // Capture loop variable for closure (crucial for async loops)
            //                int currentRow = i;

            //                // Read values from Excel. Use null-conditional operator and null-coalescing for robustness.
            //                // Note: hangTangdan was incremented inside the loop. To match its behavior, if TCNumber
            //                // is read from Cells[hangTangdan, 2] and hangTangdan started at 2 and incremented immediately,
            //                // then for the first iteration (i=3), hangTangdan becomes 3, meaning Cells[3,2] is read.
            //                // We'll adjust `currentRow` to reflect the actual row being processed.
            //                // In your original code, `hangTangdan` starts at 2, then `hangTangdan++` makes it 3.
            //                // So, `worksheet.Cells[hangTangdan, 2].Value` corresponds to `worksheet.Cells[currentRow, 2].Value` where `currentRow` starts at `minvalue`.
            //                // Let's ensure we are reading from `currentRow` directly to avoid confusion.

            //                var TCNumberRaw = worksheet.Cells[currentRow, 2].Value;
            //                if (TCNumberRaw == null)
            //                {
            //                    Console.WriteLine($"Skipping row {currentRow}: TCNumber (column B) is empty.");
            //                    continue; // Skip this row if TCNumber is null
            //                }
            //                var previousNumber = TCNumberRaw.ToString(); // This becomes the actual TCNumber
            //                var testCaseName = "TC" + previousNumber; // Construct test case name
            //                var question = worksheet.Cells[currentRow, 4].Value?.ToString();
            //                var IsPass = worksheet.Cells[currentRow, 12].Value?.ToString();

            //                if (IsPass != "Pass")
            //                {
            //                    continue;
            //                }

            //                if (string.IsNullOrWhiteSpace(question))
            //                {
            //                    Console.WriteLine($"Skipping row {currentRow}: Question (column D) is empty.");
            //                    continue; // Skip if question is empty
            //                }



            //                if (string.IsNullOrWhiteSpace(question))
            //                {
            //                    Console.WriteLine($"Skipping row {currentRow}: Question (column D) is empty.");
            //                    continue; // Skip if question is empty
            //                }

            //                // Define the path for the expected JSON for this test case
            //                var jsonExpectFilePath = Path.Combine(folderJson, $"{testCaseName}.json");

            //                //if (!File.Exists(jsonExpectFilePath))
            //                //{
            //                //    Console.WriteLine($"Skipping row {currentRow} ({testCaseName}): Expected JSON file not found at {jsonExpectFilePath}.");
            //                //    continue; // Skip if expected JSON file is missing
            //                //}

            //                //string predefinedJsonString = File.ReadAllText(jsonExpectFilePath).Trim();
            //                //var jsonExpect = folderJson + testCaseName + ".json";

            //                var listRequest = new List<string>();
            //                listRequest.Add(question);
            //                //listRequest.Add("fe9e982f-fbf0-41c3-90c2-da103767f7e1");
            //                listRequest.Add("AiHoldings");

            //                string requestBody = File.ReadAllText(pathfolder + "\\FormatBodyRequest.txt");
            //                for (int j = 0; j < listRequest.Count; j++)
            //                {
            //                    requestBody = requestBody.Replace("$$J" + j, listRequest[j]);

            //                }
            //                // Add the task to the list
            //                apiCallTasks.Add(Task.Run(async () => // Task.Run moves the async operation to a ThreadPool thread
            //                {
            //                    using (HttpClient client = new HttpClient())
            //                    {
            //                        try
            //                        {
            //                            StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            //                            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            //                            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

            //                            string apiResponseString = await response.Content.ReadAsStringAsync();

            //                            JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponseString);
            //                            JsonElement apiRoot = apiJsonDocument.RootElement;
            //                            File.WriteAllText(jsonExpectFilePath, apiRoot.GetRawText().Trim());
            //                            Console.WriteLine(jsonExpectFilePath);
            //                            testResults.Add(true);

            //                            // Perform comparison and add result to concurrent bag
            //                            //bool testPassed = StringComparer.CompareAndShowDifferencesWithZip(apiRoot.GetRawText().Trim(), predefinedJsonString, testCaseName);
            //                            //testResults.Add(testPassed);
            //                        }
            //                        catch (HttpRequestException e)
            //                        {
            //                            lock (Console.Out)
            //                            {
            //                                Console.ForegroundColor = ConsoleColor.Red;
            //                                Console.WriteLine($"HTTP Error for {testCaseName} (Row {currentRow}): {e.Message}");
            //                                Console.ResetColor();
            //                            }
            //                            testResults.Add(false); // Mark as failed due to HTTP error
            //                        }
            //                        catch (System.Text.Json.JsonException e)
            //                        {
            //                            lock (Console.Out)
            //                            {
            //                                Console.ForegroundColor = ConsoleColor.Red;
            //                                Console.WriteLine($"JSON Parsing Error for {testCaseName} (Row {currentRow}): {e.Message}");
            //                                Console.ResetColor();
            //                            }
            //                            testResults.Add(false); // Mark as failed due to JSON parsing error
            //                        }
            //                        catch (Exception e)
            //                        {
            //                            lock (Console.Out)
            //                            {
            //                                Console.ForegroundColor = ConsoleColor.Red;
            //                                Console.WriteLine($"An unexpected error occurred for {testCaseName} (Row {currentRow}): {e.Message}");
            //                                Console.ResetColor();
            //                            }
            //                            testResults.Add(false); // Mark as failed due to other error
            //                        }
            //                    }
            //                }));
            //            }

            //            // Wait for all tasks to complete
            //            await Task.WhenAll(apiCallTasks);


            //        }
            //        // Iterate through rows and create tasks for each API call
     

            //        // Determine overall test result
            //        bool isTotalTestPassed = testResults.All(result => result == true);

            //        Console.WriteLine("\n------------------------------------");
            //        Console.WriteLine($"Total tests run: {testResults.Count}");
            //        Console.ForegroundColor = isTotalTestPassed ? ConsoleColor.Green : ConsoleColor.Red;
            //        Console.WriteLine($"Overall Test Result: {(isTotalTestPassed ? "PASSED" : "FAILED")}");
            //        Console.ResetColor();
            //        Console.WriteLine("------------------------------------");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine($"\nAn unhandled error occurred in Main execution: {ex.Message}");
            //        Console.WriteLine(ex.StackTrace);
            //        Console.ResetColor();
            //    }
            //    finally
            //    {
            //        package?.Dispose(); // Dispose the ExcelPackage to release the file
            //        Console.WriteLine("\nPress any key to exit.");
            //        Console.ReadKey(); // Keep this one at the very end to pause the console.
            //    }
            //}
            #endregion
        }

        public class Excel
        {
            public void ReadExcel()
            {
                //var minvalue = int.Parse(min.Value.ToString());
                //var maxvalue = int.Parse(max.Value.ToString());
                var minvalue = 3;
                //var maxvalue = int.Parse(max.Value.ToString());
                var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

                var fileor = new FileInfo(pathfolder);
                pathfolder = fileor.Directory.ToString();
                var thongtinfile = pathfolder + "\\TemplateReport\\TemplateReport.xlsx";
                if (!File.Exists(thongtinfile)) { Console.WriteLine("Not found template file."); return; }
                ExcelPackage package = new ExcelPackage(new FileInfo(thongtinfile));
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                //ExcelPackage package = new ExcelPackage();
                //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                // get number of rows and columns in the sheet
                var maxvalue = worksheet.Dimension.Rows;
                var hangTangdan = 3;
                var previousNumber = "unknow";
                for (int i = minvalue; i <= maxvalue; i++)
                {
                    var TCNumber = worksheet.Cells[hangTangdan, 1].Value;
                    if (TCNumber != null)
                    {
                        previousNumber = TCNumber.ToString();
                    }


                    hangTangdan++;
                }
            }
        }

        public class StringComparer
        {
            public static void CompareAndShowDifferencesCharByChar(string s1, string s2, string testCaseName, bool caseSensitive = true)
            {
                if (s1 == null && s2 == null)
                {
                    Console.WriteLine(testCaseName + "Both strings are null.");
                    return;
                }
                if (s1 == null)
                {
                    Console.WriteLine(testCaseName + $"String 1 is null. String 2: \"{s2}\"");
                    return;
                }
                if (s2 == null)
                {
                    Console.WriteLine(testCaseName + $"String 2 is null. String 1: \"{s1}\"");
                    return;
                }

                if (!caseSensitive)
                {
                    s1 = s1.ToLowerInvariant(); // Use InvariantCulture for consistent casing across cultures
                    s2 = s2.ToLowerInvariant();
                }

                int minLength = Math.Min(s1.Length, s2.Length);
                bool foundDifference = false;

                Console.WriteLine(testCaseName + $"Comparing \"{s1}\" and \"{s2}\" (Case sensitive: {caseSensitive})");

                for (int i = 0; i < minLength; i++)
                {
                    if (s1[i] != s2[i])
                    {
                        Console.WriteLine(testCaseName + $"  Difference at index {i}: '{s1[i]}' vs '{s2[i]}'");
                        foundDifference = true;
                    }
                }

                if (s1.Length != s2.Length)
                {
                    foundDifference = true;
                    if (s1.Length > s2.Length)
                    {
                        Console.WriteLine(testCaseName + $"  String 1 is longer. Remaining characters from String 1: \"{s1.Substring(minLength)}\"");
                    }
                    else
                    {
                        Console.WriteLine(testCaseName + $"  String 2 is longer. Remaining characters from String 2: \"{s2.Substring(minLength)}\"");
                    }
                }

                if (!foundDifference)
                {
                    Console.WriteLine(testCaseName + "  Strings are identical.");
                }
            }

            // Another way using LINQ Zip for character-by-character comparison
            public static bool CompareAndShowDifferencesWithZip(string s1, string s2, string testCaseName)
            {
                //Console.WriteLine($"\nComparing with Zip: \"{s1}\" and \"{s2}\"");

                if (s1 == null || s2 == null)
                {
                    Console.WriteLine(testCaseName + "One or both strings are null.");
                    return false;
                }

                var differences = s1.Zip(s2, (c1, c2) => new { Char1 = c1, Char2 = c2, AreDifferent = c1 != c2 })
                                     .Select((pair, index) => new { Index = index, pair.Char1, pair.Char2, pair.AreDifferent })
                                     .Where(pair => pair.AreDifferent)
                                     .ToList();

                if (differences.Any())
                {
                    Console.WriteLine(testCaseName + "Differences found:");
                    foreach (var diff in differences)
                    {
                        Console.WriteLine(testCaseName + $"  Index {diff.Index}: '{diff.Char1}' (String 1) vs '{diff.Char2}' (String 2)");
                    }
                        return false;
                }

                if (s1.Length != s2.Length)
                {
                    if (s1.Length > s2.Length)
                    {
                        Console.WriteLine(testCaseName + $"  String 1 has extra characters from index {s2.Length}: \"{s1.Substring(s2.Length)}\"");
                    }
                    else
                    {
                        Console.WriteLine(testCaseName + $"  String 2 has extra characters from index {s1.Length}: \"{s2.Substring(s1.Length)}\"");
                    }
                    return false;
                }

                if (!differences.Any() && s1.Length == s2.Length)
                {
                    Console.WriteLine(testCaseName + "  Strings are identical.");
                }
                return true;
            }
        }
        public class ExcelRowData
        {
            public int RowNumber { get; set; }
            public string TCNumber { get; set; }
            public string Question { get; set; }
        }
    }
}

