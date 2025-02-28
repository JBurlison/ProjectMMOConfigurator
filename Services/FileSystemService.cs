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
            return !Directory.Exists(directoryPath)
                ? throw new DirectoryNotFoundException($"Directory not found: {directoryPath}")
                : (IEnumerable<string>)Directory.GetFiles(directoryPath, "*.json", SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<string> GetJsonFilesRecursively(string directoryPath)
        {
            return !Directory.Exists(directoryPath)
                ? throw new DirectoryNotFoundException($"Directory not found: {directoryPath}")
                : (IEnumerable<string>)Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);
        }

        public Dictionary<string, List<string>> GetJsonFilesByType(string directoryPath)
        {
            var result = new Dictionary<string, List<string>>();

            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                var folderName = new DirectoryInfo(directory).Name;
                var modelType = _modelFactory.GetModelTypeFromFolderName(folderName);

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
            var configPath = Path.Combine(directoryPath, "config");
            if (Directory.Exists(configPath))
            {
                var skillsPath = Path.Combine(configPath, "skills.json");
                if (File.Exists(skillsPath))
                {
                    result["config"] = [skillsPath];
                }
            }

            return result;
        }

        public async Task<Dictionary<string, List<string>>> GetJsonFilesByTypeRecursively(string directoryPath)
        {
            var result = new Dictionary<string, List<string>>();

            // Get all JSON files recursively
            var jsonFiles = GetJsonFilesRecursively(directoryPath);

            // Group files by their model type
            foreach (var filePath in jsonFiles)
            {
                try
                {
                    // Create a model to determine its type
                    var model = await _modelFactory.CreateModelFromFileAsync(filePath);
                    if (model != null)
                    {
                        var modelTypeName = model.GetType().Name;

                        if (!result.ContainsKey(modelTypeName))
                        {
                            result[modelTypeName] = [];
                        }

                        result[modelTypeName].Add(filePath);
                    }
                }
                catch (Exception)
                {
                    // Skip files that can't be loaded
                    continue;
                }
            }

            return result;
        }
    }
}
