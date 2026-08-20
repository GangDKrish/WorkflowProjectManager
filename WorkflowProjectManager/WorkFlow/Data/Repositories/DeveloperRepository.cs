using Microsoft.EntityFrameworkCore;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Repositories;

public class DeveloperRepository : Repository<Developer>
{
    public DeveloperRepository(WorkFlowDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Developer>> GetAllAsync()
    {
        return await _dbSet
            .Include(d => d.AssignedWorkItems)
            .ToListAsync();
    }

    public override async Task<Developer?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(d => d.AssignedWorkItems)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Developer?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .Include(d => d.AssignedWorkItems)
            .FirstOrDefaultAsync(d => d.Email == email);
    }
}
