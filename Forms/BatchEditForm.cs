using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProjectMMOConfigurator.Models;
using ProjectMMOConfigurator.Services;

namespace ProjectMMOConfigurator.Forms
{
    public partial class BatchEditForm : Form
    {
        private readonly BatchEditService _batchEditService;
        private readonly Dictionary<string, List<string>> _filesByType;
        private readonly ModelFactory _modelFactory;
        
        private string _selectedType;

        public BatchEditForm(BatchEditService batchEditService, Dictionary<string, List<string>> filesByType, ModelFactory modelFactory)
        {
            InitializeComponent();
            _batchEditService = batchEditService;
            _filesByType = filesByType;
            _modelFactory = modelFactory;
            
            // Wire up events
            Load += BatchEditForm_Load;
            modelTypeComboBox.SelectedIndexChanged += ModelTypeComboBox_SelectedIndexChanged;
            applyButton.Click += ApplyButton_Click;
        }

        private void BatchEditForm_Load(object sender, EventArgs e)
        {
            // Populate model type combo box
            modelTypeComboBox.Items.AddRange(_filesByType.Keys.ToArray());
            if (modelTypeComboBox.Items.Count > 0)
            {
                modelTypeComboBox.SelectedIndex = 0;
            }
        }

        private void ModelTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedType = modelTypeComboBox.SelectedItem.ToString();
            
            // Update property path choices based on model type
            propertyComboBox.Items.Clear();
            
            switch (_selectedType.ToLowerInvariant())
            {
                case "item":
                    propertyComboBox.Items.Add("xp_values.BLOCK_BREAK");
                    propertyComboBox.Items.Add("xp_values.TOOL_BREAKING");
                    propertyComboBox.Items.Add("xp_values.CRAFT");
                    propertyComboBox.Items.Add("requirements.TOOL");
                    propertyComboBox.Items.Add("requirements.WEAPON");
                    propertyComboBox.Items.Add("requirements.USE");
                    break;
                    
                case "blocks":
                    propertyComboBox.Items.Add("xp_values.BLOCK_BREAK");
                    propertyComboBox.Items.Add("xp_values.BLOCK_PLACE");
                    propertyComboBox.Items.Add("requirements.BREAK");
                    propertyComboBox.Items.Add("requirements.PLACE");
                    break;
                    
                case "entity":
                    propertyComboBox.Items.Add("xp_values.DEATH");
                    propertyComboBox.Items.Add("xp_values.BREED");
                    propertyComboBox.Items.Add("requirements.KILL");
                    break;
                    
                case "biome":
                    propertyComboBox.Items.Add("bonus.Biome");
                    break;
                    
                case "dimension":
                    propertyComboBox.Items.Add("bonus.Dimension");
                    break;
                    
                default:
                    // No options for this type
                    break;
            }
            
            if (propertyComboBox.Items.Count > 0)
            {
                propertyComboBox.SelectedIndex = 0;
            }
        }

        private async void ApplyButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedType) || 
                propertyComboBox.SelectedItem == null || 
                string.IsNullOrWhiteSpace(skillNameTextBox.Text))
            {
                MessageBox.Show("Please select a model type, property path, and enter a skill name.", 
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int skillLevel;
            if (!int.TryParse(skillLevelTextBox.Text, out skillLevel) || skillLevel < 0)
            {
                MessageBox.Show("Please enter a valid skill level (non-negative integer).",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string propertyPath = propertyComboBox.SelectedItem.ToString();
            string skillName = skillNameTextBox.Text.Trim();
            
            // Get files of the selected type
            if (!_filesByType.ContainsKey(_selectedType))
            {
                MessageBox.Show("No files found for the selected type.",
                    "No Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            List<string> filesToEdit = _filesByType[_selectedType];
            if (filesToEdit.Count == 0)
            {
                MessageBox.Show("No files found for the selected type.",
                    "No Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to add/update skill '{skillName}' with level {skillLevel} " +
                $"to property '{propertyPath}' in {filesToEdit.Count} files?",
                "Confirm Batch Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }
            
            applyButton.Enabled = false;
            progressBar.Visible = true;
            
            try
            {
                // Use appropriate type for the generic parameter based on selected model type
                Type modelType = GetTypeFromModelName(_selectedType);
                if (modelType == null)
                {
                    MessageBox.Show("Selected model type is not supported for batch editing.",
                        "Unsupported Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Use reflection to call the generic method with the correct type
                var method = typeof(BatchEditService).GetMethod("ApplySkillRequirementsAsync");
                var genericMethod = method.MakeGenericMethod(modelType);
                
                var task = (Task<BatchEditResult>)genericMethod.Invoke(
                    _batchEditService, 
                    new object[] { filesToEdit, propertyPath, skillName, skillLevel });
                
                var result = await task;

                // Show results
                resultsTextBox.Text = $"Successfully edited {result.SuccessfulEdits.Count} files.\r\n\r\n";
                
                if (result.Failures.Any())
                {
                    resultsTextBox.Text += $"Failed to edit {result.Failures.Count} files:\r\n";
                    foreach (var (filePath, errorMessage) in result.Failures)
                    {
                        resultsTextBox.Text += $"- {Path.GetFileName(filePath)}: {errorMessage}\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during batch edit: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                applyButton.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private Type GetTypeFromModelName(string modelName)
        {
            switch (modelName.ToLowerInvariant())
            {
                case "item":
                    return typeof(Item);
                case "blocks":
                    return typeof(Blocks);
                case "entity":
                    return typeof(Entity);
                case "biome":
                    return typeof(Biome);
                case "dimension":
                    return typeof(Dimension);
                default:
                    return null;
            }
        }
    }
}
