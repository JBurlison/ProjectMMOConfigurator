using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ProjectMMOConfigurator.Services
{
    public class JsonFileService
    {
        private readonly JsonSerializerOptions _options;

        public JsonFileService()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<T> LoadAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            string jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(jsonString, _options);
        }

        public async Task SaveAsync<T>(string filePath, T data)
        {
            string jsonString = JsonSerializer.Serialize(data, _options);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        public bool IsJsonFile(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase);
        }
    }
}
