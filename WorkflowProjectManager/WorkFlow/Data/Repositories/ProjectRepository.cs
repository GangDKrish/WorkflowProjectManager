using Microsoft.EntityFrameworkCore;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Repositories;

public class ProjectRepository : Repository<Project>
{
    public ProjectRepository(WorkFlowDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Sprints)
            .Include(p => p.Developers)
            .ToListAsync();
    }

    public override async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Sprints)
            .Include(p => p.Developers)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
