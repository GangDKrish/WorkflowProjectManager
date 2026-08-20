using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();

    Task<Project?> GetProjectByIdAsync(int id);

    Task<Project> CreateProjectAsync(Project project);

    Task<bool> UpdateProjectAsync(Project project);

    Task<bool> DeleteProjectAsync(int id);

    Task<IEnumerable<Project>> SearchProjectsAsync(string searchText);
}
