using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Data.Repositories;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Services;

public class SprintService : ISprintService
{
    private readonly SprintRepository _repository;

    public SprintService(WorkFlowDbContext context)
    {
        _repository = new SprintRepository(context);
    }

    public async Task<IEnumerable<Sprint>> GetAllSprintsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Sprint?> GetSprintByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Sprint> CreateSprintAsync(Sprint sprint)
    {
        return await _repository.AddAsync(sprint);
    }
}

