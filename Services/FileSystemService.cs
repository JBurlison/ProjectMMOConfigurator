using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMMOConfigurator.Services
{
    public class FileSystemService
    {
        private readonly JsonFileService _jsonFileService;
        private readonly ModelFactory _modelFactory;

        public FileSystemService(JsonFileService jsonFileService, ModelFactory modelFactory)
        {
            _jsonFileService = jsonFileService;
            _modelFactory = modelFactory;
        }

        public IEnumerable<string> GetJsonFilesInDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

            return Directory.GetFiles(directoryPath, "*.json", SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<string> GetJsonFilesRecursively(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

            return Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);
        }

        public Dictionary<string, List<string>> GetJsonFilesByType(string directoryPath)
        {
            var result = new Dictionary<string, List<string>>();
            
            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                string folderName = new DirectoryInfo(directory).Name;
                Type modelType = _modelFactory.GetModelTypeFromFolderName(folderName);
                
                if (modelType != null)
                {
                    var jsonFiles = GetJsonFilesInDirectory(directory);
                    if (jsonFiles.Any())
                    {
                        result[folderName] = jsonFiles.ToList();
                    }
                }
            }

            // Special handling for config folder
            string configPath = Path.Combine(directoryPath, "config");
            if (Directory.Exists(configPath))
            {
                string skillsPath = Path.Combine(configPath, "skills.json");
                if (File.Exists(skillsPath))
                {
                    result["config"] = new List<string> { skillsPath };
                }
            }

            return result;
        }
    }
}
