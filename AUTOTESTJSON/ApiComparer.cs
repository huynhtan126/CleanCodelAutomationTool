using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class ApiComparer
{
    private readonly HttpClient _httpClient;

    public ApiComparer()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Sends a GET request to the API and returns the JSON string.
    /// </summary>
    /// <param name="apiUrl">The URL of the API.</param>
    /// <returns>The JSON string received from the API.</returns>
    public async Task<string> CallApiAsync(string apiUrl)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status code is not 2xx.
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Response:");
            Console.WriteLine(responseBody);
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error calling API: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Reads the content of a JSON file.
    /// </summary>
    /// <param name="filePath">The path to the JSON file.</param>
    /// <returns>The JSON string from the file.</returns>
    public string ReadJsonFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine($"Content of JSON file '{filePath}':");
            Console.WriteLine(fileContent);
            return fileContent;
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error reading file: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Compares two JSON strings and lists their differences.
    /// </summary>
    /// <param name="json1">The first JSON string.</param>
    /// <param name="json2">The second JSON string.</param>
    /// <returns>True if the two JSONs are identical, False otherwise.</returns>
    public bool CompareAndListDifferences(string json1, string json2)
    {
        if (json1 == null || json2 == null)
        {
            Console.WriteLine("One of the JSON strings is null or invalid for comparison.");
            return false;
        }

        try
        {
            JToken parsedJson1 = JToken.Parse(json1);
            JToken parsedJson2 = JToken.Parse(json2);

            List<string> differences = new List<string>();
            CompareTokens(parsedJson1, parsedJson2, "", differences);

            if (differences.Count == 0)
            {
                Console.WriteLine("\nResult: JSON from API and JSON from file are identical.");
                return true;
            }
            else
            {
                Console.WriteLine("\nResult: JSON from API and JSON from file are DIFFERENT at the following points:");
                foreach (var diff in differences)
                {
                    Console.WriteLine($"- {diff}");
                }
                return false;
            }
        }
        catch (JsonReaderException e)
        {
            Console.WriteLine($"Error parsing JSON: {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Recursively compares two JTokens and records differences.
    /// </summary>
    /// <param name="token1">The first JToken.</param>
    /// <param name="token2">The second JToken.</param>
    /// <param name="path">The current path in the JSON structure (e.g., "$.property", "$.array[0]").</param>
    /// <param name="differences">A list to store reported differences.</param>
    private void CompareTokens(JToken token1, JToken token2, string path, List<string> differences)
    {
        // If types are different, it's a fundamental difference
        if (token1.Type != token2.Type)
        {
            differences.Add($"Type mismatch at: {path}. Token 1 type: {token1.Type}, Token 2 type: {token2.Type}");
            return;
        }

        switch (token1.Type)
        {
            case JTokenType.Object:
                var obj1 = (JObject)token1;
                var obj2 = (JObject)token2;

                // Check for properties only present in obj1
                foreach (var prop1 in obj1.Properties())
                {
                    if (obj2[prop1.Name] == null)
                    {
                        differences.Add($"Property only exists in the first JSON: {path}.{prop1.Name}");
                    }
                }

                // Check for properties only present in obj2
                foreach (var prop2 in obj2.Properties())
                {
                    if (obj1[prop2.Name] == null)
                    {
                        differences.Add($"Property only exists in the second JSON: {path}.{prop2.Name}");
                    }
                }

                // Compare common properties recursively
                foreach (var prop1 in obj1.Properties())
                {
                    if (obj2[prop1.Name] != null)
                    {
                        CompareTokens(prop1.Value, obj2[prop1.Name], $"{path}.{prop1.Name}", differences);
                    }
                }
                break;

            case JTokenType.Array:
                var arr1 = (JArray)token1;
                var arr2 = (JArray)token2;

                // Check for array length difference
                if (arr1.Count != arr2.Count)
                {
                    differences.Add($"Array length mismatch at: {path}. Array 1 has {arr1.Count} elements, Array 2 has {arr2.Count} elements.");
                }

                // Compare elements up to the minimum count
                int minCount = Math.Min(arr1.Count, arr2.Count);
                for (int i = 0; i < minCount; i++)
                {
                    CompareTokens(arr1[i], arr2[i], $"{path}[{i}]", differences);
                }
                break;

            case JTokenType.Property:
                // JProperty values are handled when processing JObjects
                break;

            default: // JValue (String, Integer, Boolean, Null, etc.)
                // Compare primitive values
                if (!JToken.DeepEquals(token1, token2))
                {
                    differences.Add($"Value mismatch at: {path}. JSON 1: '{token1}', JSON 2: '{token2}'");
                }
                break;
        }
    }


}