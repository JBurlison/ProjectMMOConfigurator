using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProjectMMOConfigurator.Services;

namespace ProjectMMOConfigurator.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly JsonFileService _jsonFileService;
        private readonly ModelFactory _modelFactory;
        private readonly FileSystemService _fileSystemService;
        private object _currentModel;
        private string _currentFilePath;
        private string _rootDirectory;
        private bool _isLoading;

        public MainViewModel(JsonFileService jsonFileService, ModelFactory modelFactory, FileSystemService fileSystemService)
        {
            _jsonFileService = jsonFileService;
            _modelFactory = modelFactory;
            _fileSystemService = fileSystemService;

            LoadedFiles = [];
            OpenFileCommand = new RelayCommand(OpenFile);
            OpenFolderCommand = new RelayCommand(OpenFolder);
            SaveCommand = new RelayCommand(SaveFile, CanSaveFile);
            SearchCommand = new RelayCommand(SearchSkills);
            BatchEditCommand = new RelayCommand(BatchEdit);
        }

        public ObservableCollection<FileViewModel> LoadedFiles { get; }

        public object CurrentModel
        {
            get => _currentModel;
            set
            {
                if (_currentModel != value)
                {
                    _currentModel = value;
                    OnPropertyChanged();
                    ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string CurrentFilePath
        {
            get => _currentFilePath;
            set
            {
                if (_currentFilePath != value)
                {
                    _currentFilePath = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentFileName));
                }
            }
        }

        public string? CurrentFileName => string.IsNullOrEmpty(CurrentFilePath) ? null : Path.GetFileName(CurrentFilePath);

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RootDirectory
        {
            get => _rootDirectory;
            set
            {
                if (_rootDirectory != value)
                {
                    _rootDirectory = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand OpenFileCommand { get; }
        public ICommand OpenFolderCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand BatchEditCommand { get; }

        private void OpenFile(object parameter)
        {
            // Show file dialog and load selected file
            // For now, placeholder for UI implementation
        }

        private void OpenFolder(object parameter)
        {
            // Show folder dialog and load all files
            // For now, placeholder for UI implementation
        }

        private async Task LoadFileAsync(string filePath)
        {
            IsLoading = true;
            try
            {
                CurrentModel = await _modelFactory.CreateModelFromFileAsync(filePath);
                CurrentFilePath = filePath;

                // Add to loaded files if not already present
                if (!LoadedFiles.Any(f => f.FilePath == filePath))
                {
                    LoadedFiles.Add(new FileViewModel
                    {
                        FilePath = filePath,
                        FileName = Path.GetFileName(filePath),
                        ModelType = CurrentModel.GetType().Name
                    });
                }
            }
            catch (Exception)
            {
                // Handle errors (show message to user in UI)
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void SaveFile(object parameter)
        {
            if (CurrentModel != null && !string.IsNullOrEmpty(CurrentFilePath))
            {
                try
                {
                    _jsonFileService.SaveAsync(CurrentFilePath, CurrentModel).Wait();
                    // Show success message
                }
                catch (Exception)
                {
                    // Handle errors
                }
            }
        }

        private bool CanSaveFile(object parameter) => CurrentModel != null && !string.IsNullOrEmpty(CurrentFilePath);

        private void SearchSkills(object parameter)
        {
            // Implement skill search
        }

        private void BatchEdit(object parameter)
        {
            // Implement batch edit
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class FileViewModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ModelType { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
