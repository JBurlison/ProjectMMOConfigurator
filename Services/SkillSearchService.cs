using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ProjectMMOConfigurator.Models;

namespace ProjectMMOConfigurator.Services
{
    public class SkillSearchService
    {
        private readonly FileSystemService _fileSystemService;
        private readonly ModelFactory _modelFactory;

        public SkillSearchService(FileSystemService fileSystemService, ModelFactory modelFactory)
        {
            _fileSystemService = fileSystemService;
            _modelFactory = modelFactory;
        }

        public async Task<List<SkillSearchResult>> FindModelsWithSkillAsync(string directoryPath, string skillName)
        {
            var results = new List<SkillSearchResult>();
            var files = _fileSystemService.GetJsonFilesRecursively(directoryPath);

            foreach (var file in files)
            {
                try
                {
                    var model = await _modelFactory.CreateModelFromFileAsync(file);
                    if (model != null)
                    {
                        var matches = FindSkillInModel(model, skillName);
                        if (matches.Any())
                        {
                            results.Add(new SkillSearchResult
                            {
                                FilePath = file,
                                ModelType = model.GetType().Name,
                                SkillLocations = matches
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    // Skip files that can't be parsed
                }
            }

            return results;
        }

        private List<string> FindSkillInModel(object model, string skillName)
        {
            var results = new List<string>();

            // Check dictionaries with string keys for the skill name
            foreach (var prop in model.GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType && 
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    var dictType = prop.PropertyType;
                    if (dictType.GenericTypeArguments[0] == typeof(string))
                    {
                        var dict = prop.GetValue(model) as dynamic;
                        if (dict != null && dict.ContainsKey(skillName))
                        {
                            results.Add($"{prop.Name}[{skillName}]");
                        }

                        // Check for nested dictionaries
                        if (dictType.GenericTypeArguments[1].IsGenericType &&
                            dictType.GenericTypeArguments[1].GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        {
                            foreach (var key in dict.Keys)
                            {
                                var nestedDict = dict[key] as dynamic;
                                if (nestedDict != null && nestedDict.ContainsKey(skillName))
                                {
                                    results.Add($"{prop.Name}[{key}][{skillName}]");
                                }
                            }
                        }
                    }
                }
            }

            return results;
        }
    }

    public class SkillSearchResult
    {
        public string FilePath { get; set; }
        public string ModelType { get; set; }
        public List<string> SkillLocations { get; set; }
    }
}
