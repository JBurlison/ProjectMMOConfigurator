using System;
using System.IO;
using System.Threading.Tasks;
using ProjectMMOConfigurator.Models;

namespace ProjectMMOConfigurator.Services
{
    public class ModelFactory
    {
        private readonly JsonFileService _jsonFileService;

        public ModelFactory(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public async Task<object> CreateModelFromFileAsync(string filePath)
        {
            if (!_jsonFileService.IsJsonFile(filePath))
                throw new ArgumentException("Not a JSON file", nameof(filePath));

            string parentFolderName = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name;
            
            // Handle config folder special case
            if (parentFolderName.Equals("config", StringComparison.OrdinalIgnoreCase))
            {
                string fileName = Path.GetFileName(filePath);
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
                "biome" => await _jsonFileService.LoadAsync<Biome>(filePath),
                "blocks" => await _jsonFileService.LoadAsync<Blocks>(filePath),
                "dimension" => await _jsonFileService.LoadAsync<Dimension>(filePath),
                "entity" => await _jsonFileService.LoadAsync<Entity>(filePath),
                "item" => await _jsonFileService.LoadAsync<Item>(filePath),
                _ => null // No matching model
            };
        }

        public Type GetModelTypeFromFolderName(string folderName)
        {
            return folderName.ToLowerInvariant() switch
            {
                "biome" => typeof(Biome),
                "blocks" => typeof(Blocks),
                "dimension" => typeof(Dimension),
                "entity" => typeof(Entity),
                "item" => typeof(Item),
                "config" => typeof(SkillsConfig),
                _ => null
            };
        }
    }
}
