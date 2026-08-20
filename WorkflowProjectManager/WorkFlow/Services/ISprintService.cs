using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Services;

public interface ISprintService
{
    Task<IEnumerable<Sprint>> GetAllSprintsAsync();

    Task<Sprint?> GetSprintByIdAsync(int id);

    Task<Sprint> CreateSprintAsync(Sprint sprint);
}
