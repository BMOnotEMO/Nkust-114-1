using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp.Services
{
    public static class JsonDataService
    {
        private static readonly string[] CandidateArrayPropertyNames = { "data", "records", "items" };

        public static JsonRecordSet? LoadJsonRecordSet(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            JsonDocument doc = JsonDocument.Parse(json);

            if (!TryResolveArray(doc.RootElement, out JsonElement array))
                return null;

            var records = new List<Dictionary<string, string>>();
            var fieldNames = new HashSet<string>();

            foreach (var item in array.EnumerateArray())
            {
                var record = new Dictionary<string, string>();
                foreach (var prop in item.EnumerateObject())
                {
                    string value = ConvertJsonValueToString(prop.Value);
                    record[prop.Name] = value;
                    fieldNames.Add(prop.Name);
                }
                records.Add(record);
            }

            return new JsonRecordSet(filePath, records, fieldNames);
        }

        private static bool TryResolveArray(JsonElement root, out JsonElement array)
        {
            if (root.ValueKind == JsonValueKind.Array)
            {
                array = root;
                return true;
            }

            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (string name in CandidateArrayPropertyNames)
                {
                    if (root.TryGetProperty(name, out JsonElement candidate) && candidate.ValueKind == JsonValueKind.Array)
                    {
                        array = candidate;
                        return true;
                    }
                }

                foreach (JsonProperty property in root.EnumerateObject())
                {
                    if (property.Value.ValueKind == JsonValueKind.Object && TryResolveArray(property.Value, out array))
                    {
                        return true;
                    }
                }
            }

            array = default;
            return false;
        }

        private static string ConvertJsonValueToString(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString() ?? string.Empty,
                JsonValueKind.Number => element.TryGetInt64(out long number)
                    ? number.ToString()
                    : element.GetDouble().ToString(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => string.Empty,
                JsonValueKind.Array or JsonValueKind.Object => element.GetRawText(),
                _ => element.ToString()
            };
        }
    }

    public sealed class JsonRecordSet
    {
        public JsonRecordSet(string filePath, IReadOnlyList<Dictionary<string, string>> records, IReadOnlyCollection<string> fieldNames)
        {
            FilePath = filePath;
            Records = records;
            FieldNames = fieldNames;
        }

        public string FilePath { get; }
        public IReadOnlyList<Dictionary<string, string>> Records { get; }
        public IReadOnlyCollection<string> FieldNames { get; }
        public int Count => Records.Count;
    }
}