using Microsoft.EntityFrameworkCore;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Repositories;

public class SprintRepository : Repository<Sprint>
{
    public SprintRepository(WorkFlowDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Sprint>> GetAllAsync()
    {
        return await _dbSet
            .Include(s => s.WorkItems)
            .ToListAsync();
    }

    public override async Task<Sprint?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(s => s.WorkItems)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sprint>> GetActiveSprintsAsync()
    {
        var today = DateTime.Today;
        return await _dbSet
            .Include(s => s.WorkItems)
            .Where(s => s.StartDate <= today && s.EndDate >= today)
            .ToListAsync();
    }
}
