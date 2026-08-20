using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WorkflowProjectManager.WorkFlow.Commands;
using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class ProjectViewModel : ViewModelBase
{
    private readonly IProjectService _projectService;
    private Project? _selectedProject;
    private string _newName = string.Empty;
    private string _newDescription = string.Empty;

    public ProjectViewModel(WorkFlowDbContext context)
    {
        _projectService = new ProjectService(context);
        Projects = new ObservableCollection<Project>();
        RefreshCommand = new AsyncRelayCommand(LoadAsync);
        AddCommand = new AsyncRelayCommand(AddProjectAsync);
        DeleteCommand = new RelayCommand(DeleteProject, CanDeleteProject);

        _ = LoadAsync();
    }

    public ObservableCollection<Project> Projects { get; }

    public Project? SelectedProject
    {
        get => _selectedProject;
        set
        {
            if (SetProperty(ref _selectedProject, value))
            {
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string NewName { get => _newName; set => SetProperty(ref _newName, value); }
    public string NewDescription { get => _newDescription; set => SetProperty(ref _newDescription, value); }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    private async System.Threading.Tasks.Task LoadAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        Projects.Clear();
        foreach (var project in projects)
        {
            Projects.Add(project);
        }
    }

    private async System.Threading.Tasks.Task AddProjectAsync()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            MessageBox.Show("Project name is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newProject = new Project
        {
            Name = NewName,
            Description = NewDescription,
            CreatedDate = System.DateTime.UtcNow
        };

        await _projectService.CreateProjectAsync(newProject);
        NewName = string.Empty;
        NewDescription = string.Empty;
        await LoadAsync();
        MessageBox.Show("Project added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private bool CanDeleteProject() => SelectedProject != null;

    private async void DeleteProject()
    {
        if (SelectedProject == null) return;
        var result = MessageBox.Show($"Delete '{SelectedProject.Name}'?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            await _projectService.DeleteProjectAsync(SelectedProject.Id);
            await LoadAsync();
            MessageBox.Show("Project deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
