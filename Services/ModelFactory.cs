using ProjectMMOConfigurator.Models;

namespace ProjectMMOConfigurator.Services
{
    public class ModelFactory
    {
        private readonly JsonFileService _jsonFileService;

        public ModelFactory(JsonFileService jsonFileService) => _jsonFileService = jsonFileService;

        public async Task<object?> CreateModelFromFileAsync(string filePath)
        {
            if (!_jsonFileService.IsJsonFile(filePath))
                throw new ArgumentException("Not a JSON file", nameof(filePath));

            var parentFolderName = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name;

            // Handle config folder special case
            if (parentFolderName.Equals("config", StringComparison.OrdinalIgnoreCase))
            {
                var fileName = Path.GetFileName(filePath);
                if (fileName.Equals("skills.json", StringComparison.OrdinalIgnoreCase))
                {
                    return await _jsonFileService.LoadAsync<SkillsConfig>(filePath);
                }
                // Other config files can be added here
                return null;
            }

            // Handle regular model types based on parent folder name
            return parentFolderName.ToLowerInvariant() switch
            {
                "biomes" => await _jsonFileService.LoadAsync<Biome>(filePath),
                "blocks" => await _jsonFileService.LoadAsync<Blocks>(filePath),
                "dimensions" => await _jsonFileService.LoadAsync<Dimension>(filePath),
                "entities" => await _jsonFileService.LoadAsync<Entity>(filePath),
                "items" => await _jsonFileService.LoadAsync<Item>(filePath),
                _ => null // No matching model
            };
        }

        public Type GetModelTypeFromFolderName(string folderName)
        {
            return folderName.ToLowerInvariant() switch
            {
                "biomes" => typeof(Biome),
                "blocks" => typeof(Blocks),
                "dimensions" => typeof(Dimension),
                "entities" => typeof(Entity),
                "items" => typeof(Item),
                "config" => typeof(SkillsConfig),
                _ => null
            };
        }
    }
}
