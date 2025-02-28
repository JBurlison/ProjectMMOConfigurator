using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProjectMMOConfigurator.Services
{
    public class BatchEditService
    {
        private readonly JsonFileService _jsonFileService;
        private readonly ModelFactory _modelFactory;

        public BatchEditService(JsonFileService jsonFileService, ModelFactory modelFactory)
        {
            _jsonFileService = jsonFileService;
            _modelFactory = modelFactory;
        }

        public async Task<BatchEditResult> ApplySkillRequirementsAsync<T>(
            List<string> filePaths, 
            string propertyPath, 
            string skillName, 
            int skillLevel)
        {
            var result = new BatchEditResult();

            foreach (var filePath in filePaths)
            {
                try
                {
                    var model = await _modelFactory.CreateModelFromFileAsync(filePath);
                    if (model is T typedModel)
                    {
                        bool modified = ApplySkillRequirement(typedModel, propertyPath, skillName, skillLevel);
                        if (modified)
                        {
                            await _jsonFileService.SaveAsync(filePath, typedModel);
                            result.SuccessfulEdits.Add(filePath);
                        }
                    }
                    else
                    {
                        result.Failures.Add((filePath, "Model type mismatch"));
                    }
                }
                catch (Exception ex)
                {
                    result.Failures.Add((filePath, ex.Message));
                }
            }

            return result;
        }

        private bool ApplySkillRequirement(object model, string propertyPath, string skillName, int skillLevel)
        {
            // Parse property path (e.g., "xp_values.BLOCK_BREAK")
            var parts = propertyPath.Split('.');
            
            // Navigate through the object properties
            object currentObj = model;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var prop = currentObj.GetType().GetProperty(parts[i]);
                if (prop == null) return false;
                
                currentObj = prop.GetValue(currentObj);
                if (currentObj == null) return false;
            }

            // Get the final property (dictionary)
            var dictProp = currentObj.GetType().GetProperty(parts[parts.Length - 1]);
            if (dictProp == null) return false;
            
            var dict = dictProp.GetValue(currentObj) as dynamic;
            if (dict == null) return false;

            // Create or update the skill requirement
            if (dict.ContainsKey(skillName))
            {
                dict[skillName] = skillLevel;
            }
            else
            {
                dict.Add(skillName, skillLevel);
            }

            return true;
        }
    }

    public class BatchEditResult
    {
        public List<string> SuccessfulEdits { get; } = new List<string>();
        public List<(string FilePath, string ErrorMessage)> Failures { get; } = new List<(string FilePath, string ErrorMessage)>();
    }
}
