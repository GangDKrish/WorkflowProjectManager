using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Data.Repositories;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Services;

public class ProjectService : IProjectService
{
    private readonly ProjectRepository _repository;

    public ProjectService(WorkFlowDbContext context)
    {
        _repository = new ProjectRepository(context);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        return await _repository.AddAsync(project);
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        var existing = await _repository.GetByIdAsync(project.Id);
        if (existing == null) return false;

        await _repository.UpdateAsync(project);
        return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Project>> SearchProjectsAsync(string searchText)
    {
        var allProjects = await _repository.GetAllAsync();

        if (string.IsNullOrWhiteSpace(searchText))
            return allProjects;

        return allProjects.Where(p =>
            p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
}

