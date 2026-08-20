using Microsoft.EntityFrameworkCore;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Data.Repositories;

public class WorkItemRepository : Repository<WorkItem>
{
    public WorkItemRepository(WorkFlowDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<WorkItem>> GetAllAsync()
    {
        return await _dbSet
            .Include(w => w.Comments)
            .Include(w => w.AssignedDeveloper)
            .ToListAsync();
    }

    public override async Task<WorkItem?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(w => w.Comments)
            .Include(w => w.AssignedDeveloper)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<WorkItem>> GetByStatusAsync(WorkItemStatus status)
    {
        return await _dbSet
            .Include(w => w.Comments)
            .Include(w => w.AssignedDeveloper)
            .Where(w => w.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<WorkItem>> GetBySprintIdAsync(int sprintId)
    {
        return await _dbSet
            .Include(w => w.Comments)
            .Include(w => w.AssignedDeveloper)
            .Where(w => w.SprintId == sprintId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WorkItem>> GetByDeveloperIdAsync(int developerId)
    {
        return await _dbSet
            .Include(w => w.Comments)
            .Include(w => w.AssignedDeveloper)
            .Where(w => w.AssignedDeveloperId == developerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WorkItem>> GetBacklogAsync()
    {
        return await _dbSet
            .Include(w => w.Comments)
            .Include(w => w.AssignedDeveloper)
            .Where(w => w.Status == WorkItemStatus.Backlog)
            .ToListAsync();
    }
}
