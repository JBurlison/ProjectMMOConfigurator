using System.Text.Json;
using ProjectMMOConfigurator.Services;

namespace ProjectMMOConfigurator.Forms
{
    public partial class MainForm : Form
    {
        private readonly JsonFileService _jsonFileService;
        private readonly ModelFactory _modelFactory;
        private readonly FileSystemService _fileSystemService;
        private readonly SkillSearchService _skillSearchService;
        private readonly BatchEditService _batchEditService;

        private object _currentModel;
        private string _currentFilePath;
        private string _rootDirectory;
        private Dictionary<string, List<string>> _loadedFilesByType = [];

        public MainForm(JsonFileService jsonFileService, ModelFactory modelFactory,
            FileSystemService fileSystemService, SkillSearchService skillSearchService,
            BatchEditService batchEditService)
        {
            InitializeComponent();

            _jsonFileService = jsonFileService;
            _modelFactory = modelFactory;
            _fileSystemService = fileSystemService;
            _skillSearchService = skillSearchService;
            _batchEditService = batchEditService;

            // Wire up events after InitializeComponent
            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initial setup
            SetupToolStrip();
            SetupStatusStrip();
        }

        private void SetupToolStrip()
        {
            // Add buttons to toolstrip
            var toolStrip = new ToolStrip();

            var openFileButton = new ToolStripButton("Open File");
            openFileButton.Click += OpenFileButton_Click;

            var openFolderButton = new ToolStripButton("Open Folder");
            openFolderButton.Click += OpenFolderButton_Click;

            var saveButton = new ToolStripButton("Save");
            saveButton.Click += SaveButton_Click;

            var searchSkillsButton = new ToolStripButton("Search Skills");
            searchSkillsButton.Click += SearchSkillsButton_Click;

            var batchEditButton = new ToolStripButton("Batch Edit");
            batchEditButton.Click += BatchEditButton_Click;

            toolStrip.Items.AddRange(new ToolStripItem[] {
                openFileButton,
                openFolderButton,
                new ToolStripSeparator(),
                saveButton,
                new ToolStripSeparator(),
                searchSkillsButton,
                batchEditButton
            });

            Controls.Add(toolStrip);
            toolStrip.Dock = DockStyle.Top;
        }

        private void SetupStatusStrip()
        {
            var statusStrip = new StatusStrip();
            var statusLabel = new ToolStripStatusLabel();
            var progressBar = new ToolStripProgressBar();

            statusStrip.Items.AddRange(new ToolStripItem[] {
                statusLabel,
                progressBar
            });

            progressBar.Visible = false;

            Controls.Add(statusStrip);
            statusStrip.Dock = DockStyle.Bottom;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = "Select a JSON file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog.FileName);
            }
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            using var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select a folder containing JSON configuration files";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                _rootDirectory = folderBrowserDialog.SelectedPath;
                LoadFilesFromFolder(_rootDirectory);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_currentModel != null && !string.IsNullOrEmpty(_currentFilePath))
            {
                try
                {
                    _jsonFileService.SaveAsync(_currentFilePath, _currentModel).Wait();
                    _ = MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SearchSkillsButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_rootDirectory))
            {
                _ = MessageBox.Show("Please open a folder first", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var searchForm = new SearchSkillsForm(_skillSearchService, _rootDirectory);
            _ = searchForm.ShowDialog();
        }

        private void BatchEditButton_Click(object sender, EventArgs e)
        {
            if (_loadedFilesByType.Count == 0)
            {
                _ = MessageBox.Show("Please open a folder with JSON files first", "No Files Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var batchForm = new BatchEditForm(_batchEditService, _loadedFilesByType, _modelFactory);
            _ = batchForm.ShowDialog();
        }

        private async void LoadFile(string filePath)
        {
            try
            {
                ShowProgressBar(true);
                _currentModel = await _modelFactory.CreateModelFromFileAsync(filePath);
                _currentFilePath = filePath;

                DisplayModelInPropertyGrid(_currentModel);
                UpdateStatusLabel($"Loaded: {Path.GetFileName(filePath)}");

                // Add to tree view if not already present
                AddFileToTreeView(filePath, _currentModel.GetType().Name);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowProgressBar(false);
            }
        }

        private void LoadFilesFromFolder(string folderPath)
        {
            try
            {
                ShowProgressBar(true);
                UpdateStatusLabel("Loading files...");

                _loadedFilesByType = _fileSystemService.GetJsonFilesByType(folderPath);

                PopulateTreeView(_loadedFilesByType);

                UpdateStatusLabel($"Loaded {_loadedFilesByType.Values.SelectMany(x => x).Count()} files from {folderPath}");
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Error loading folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowProgressBar(false);
            }
        }

        private void DisplayModelInPropertyGrid(object model) =>
            // Implementation will depend on your specific UI layout
            // Typically you would have a PropertyGrid control defined in your form designer
            propertyGrid1.SelectedObject = model;

        private void AddFileToTreeView(string filePath, string modelType)
        {
            // Check if the model type node exists
            TreeNode? typeNode = null;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text == modelType)
                {
                    typeNode = node;
                    break;
                }
            }

            // Create model type node if it doesn't exist
            if (typeNode == null)
            {
                typeNode = new TreeNode(modelType);
                _ = treeView1.Nodes.Add(typeNode);
            }

            // Check if the file node already exists
            foreach (TreeNode node in typeNode.Nodes)
            {
                if ((string)node.Tag == filePath)
                {
                    return; // File already in tree
                }
            }

            // Add the file node
            var fileName = Path.GetFileName(filePath);
            var fileNode = new TreeNode(fileName)
            {
                Tag = filePath
            };
            _ = typeNode.Nodes.Add(fileNode);

            // Expand the type node
            typeNode.Expand();

            // Add event handler for selecting files if not already added

            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
        }

        private void PopulateTreeView(Dictionary<string, List<string>> filesByType)
        {
            treeView1.Nodes.Clear();

            foreach (var type in filesByType.Keys)
            {
                var typeNode = new TreeNode(type);
                _ = treeView1.Nodes.Add(typeNode);

                foreach (var filePath in filesByType[type])
                {
                    var fileName = Path.GetFileName(filePath);
                    var fileNode = new TreeNode(fileName)
                    {
                        Tag = filePath
                    };
                    _ = typeNode.Nodes.Add(fileNode);
                }

                // Expand the type node
                typeNode.Expand();
            }

            // Add event handler for selecting files
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Only handle file nodes (ones with a Tag)
            if (e.Node.Tag != null)
            {
                _ = (string)e.Node.Tag;
                //await LoadFileAsync(filePath);

                // Update JSON tab with formatted JSON
                if (_currentModel != null)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    jsonTextBox.Text = JsonSerializer.Serialize(_currentModel, options);
                }
            }
        }

        private void UpdateStatusLabel(string message) =>
            // Update status strip label
            statusLabel.Text = message;

        private void ShowProgressBar(bool visible)
        {
            // Show/hide progress bar in status strip
            progressBar.Visible = visible;
            progressBar.Style = ProgressBarStyle.Marquee;
        }
    }
}
