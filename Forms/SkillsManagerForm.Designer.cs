using ProjectMMOConfigurator.Models;
using ProjectMMOConfigurator.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProjectMMOConfigurator.Forms
{
    public partial class SkillsManagerForm : Form
    {
        private readonly JsonFileService _jsonFileService;
        private readonly ModelFactory _modelFactory;

        private SkillsConfig _skillsConfig;
        private string _currentFilePath;

        public SkillsManagerForm(JsonFileService jsonFileService, ModelFactory modelFactory)
        {
            InitializeComponent();

            _jsonFileService = jsonFileService;
            _modelFactory = modelFactory;

            Load += SkillsManagerForm_Load;
        }

        private void InitializeComponent()
        {
            this.listSkills = new ListView();
            this.colName = new ColumnHeader();
            this.colMaxLevel = new ColumnHeader();
            this.colIcon = new ColumnHeader();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnLoad = new Button();
            this.btnSave = new Button();
            this.btnSaveAs = new Button();
            this.lblFilePath = new Label();

            this.SuspendLayout();

            // Form settings
            this.ClientSize = new Size(800, 500);
            this.Text = "Skills Manager";
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimizeBox = true;
            this.MaximizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;

            // ListView
            this.listSkills.Dock = DockStyle.None;
            this.listSkills.Location = new Point(20, 50);
            this.listSkills.Size = new Size(760, 380);
            this.listSkills.View = View.Details;
            this.listSkills.FullRowSelect = true;
            this.listSkills.MultiSelect = false;
            this.listSkills.HideSelection = false;
            this.listSkills.Columns.AddRange(new ColumnHeader[] {
                this.colName,
                this.colMaxLevel,
                this.colIcon
            });

            this.colName.Text = "Skill Name";
            this.colName.Width = 300;

            this.colMaxLevel.Text = "Max Level";
            this.colMaxLevel.Width = 100;

            this.colIcon.Text = "Icon Path";
            this.colIcon.Width = 350;

            // Buttons panel
            this.btnAdd.Location = new Point(20, 440);
            this.btnAdd.Size = new Size(100, 30);
            this.btnAdd.Text = "Add Skill";
            this.btnAdd.Click += BtnAdd_Click;

            this.btnEdit.Location = new Point(130, 440);
            this.btnEdit.Size = new Size(100, 30);
            this.btnEdit.Text = "Edit Skill";
            this.btnEdit.Click += BtnEdit_Click;

            this.btnDelete.Location = new Point(240, 440);
            this.btnDelete.Size = new Size(100, 30);
            this.btnDelete.Text = "Delete Skill";
            this.btnDelete.Click += BtnDelete_Click;

            this.btnLoad.Location = new Point(470, 440);
            this.btnLoad.Size = new Size(100, 30);
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += BtnLoad_Click;

            this.btnSave.Location = new Point(580, 440);
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += BtnSave_Click;

            this.btnSaveAs.Location = new Point(690, 440);
            this.btnSaveAs.Size = new Size(90, 30);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.Click += BtnSaveAs_Click;

            // File path label
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new Point(20, 20);
            this.lblFilePath.Size = new Size(760, 20);
            this.lblFilePath.Text = "No file loaded";

            // Add controls
            this.Controls.AddRange(new Control[] {
                this.listSkills,
                this.btnAdd,
                this.btnEdit,
                this.btnDelete,
                this.btnLoad,
                this.btnSave,
                this.btnSaveAs,
                this.lblFilePath
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SkillsManagerForm_Load(object sender, EventArgs e)
        {
            // Initialize with empty skills config
            _skillsConfig = new SkillsConfig();
            UpdateSkillsList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Create a new skill with default values
            var newSkill = new Skill
            {
                icon = "DEFAULT_ICON",
                maxLevel = 100,
                iconSize = 32,
                useTotalLevels = false,
                color = 0,
                noAfkPenalty = false,
                displayGroupName = false,
                showInList = true,
                groupfor = []
            };

            // Show the edit form
            using var form = new SkillEditForm("", newSkill, SaveSkill, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateSkillsList();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listSkills.SelectedItems.Count == 0)
            {
                _ = MessageBox.Show("Please select a skill to edit.", "No Skill Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var skillName = listSkills.SelectedItems[0].Text;
            if (_skillsConfig.skills.TryGetValue(skillName, out var skill))
            {
                // Create a copy to avoid modifying the original if user cancels
                var skillCopy = new Skill
                {
                    icon = skill.icon,
                    maxLevel = skill.maxLevel,
                    iconSize = skill.iconSize,
                    useTotalLevels = skill.useTotalLevels,
                    color = skill.color,
                    noAfkPenalty = skill.noAfkPenalty,
                    displayGroupName = skill.displayGroupName,
                    showInList = skill.showInList,
                    groupfor = new Dictionary<string, float>(skill.groupfor)
                };

                using var form = new SkillEditForm(skillName, skillCopy, SaveSkill);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    UpdateSkillsList();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listSkills.SelectedItems.Count == 0)
            {
                _ = MessageBox.Show("Please select a skill to delete.", "No Skill Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var skillName = listSkills.SelectedItems[0].Text;
            var result = MessageBox.Show($"Are you sure you want to delete the skill '{skillName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _skillsConfig.skills.Remove(skillName);
                UpdateSkillsList();
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = "Select a Skills Configuration File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadSkillsFile(openFileDialog.FileName);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                BtnSaveAs_Click(sender, e);
                return;
            }

            SaveSkillsFile(_currentFilePath);
        }

        private void BtnSaveAs_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.Title = "Save Skills Configuration";

            if (_currentFilePath != null)
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(_currentFilePath);
                saveFileDialog.FileName = Path.GetFileName(_currentFilePath);
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveSkillsFile(saveFileDialog.FileName);
            }
        }

        private void SaveSkill(string skillName, Skill skill)
        {
            // Add or update skill in the config
            _skillsConfig.skills[skillName] = skill;
        }

        private void UpdateSkillsList()
        {
            listSkills.Items.Clear();

            foreach (var (name, skill) in _skillsConfig.skills)
            {
                var item = new ListViewItem(name);
                _ = item.SubItems.Add(skill.maxLevel.ToString());
                _ = item.SubItems.Add(skill.icon);

                _ = listSkills.Items.Add(item);
            }

            // Update save button state
            btnSave.Enabled = !string.IsNullOrEmpty(_currentFilePath);
        }

        private async void LoadSkillsFile(string filePath)
        {
            try
            {
                // Try loading as a SkillsConfig first
                var model = await _modelFactory.CreateModelFromFileAsync(filePath);

                if (model is SkillsConfig skillsConfig)
                {
                    _skillsConfig = skillsConfig;
                    _currentFilePath = filePath;

                    UpdateSkillsList();
                    lblFilePath.Text = $"File: {filePath}";

                    _ = MessageBox.Show("Skills configuration loaded successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _ = MessageBox.Show("The selected file is not a valid skills configuration file.",
                        "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error loading skills file: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SaveSkillsFile(string filePath)
        {
            try
            {
                // Set the type identifier
                _skillsConfig.type = "SKILLS";

                // Save to file
                await _jsonFileService.SaveAsync(filePath, _skillsConfig);

                _currentFilePath = filePath;
                lblFilePath.Text = $"File: {filePath}";

                _ = MessageBox.Show("Skills configuration saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update buttons state
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error saving skills file: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Control declarations
        private ListView listSkills;
        private ColumnHeader colName;
        private ColumnHeader colMaxLevel;
        private ColumnHeader colIcon;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnLoad;
        private Button btnSave;
        private Button btnSaveAs;
        private Label lblFilePath;
    }
}
