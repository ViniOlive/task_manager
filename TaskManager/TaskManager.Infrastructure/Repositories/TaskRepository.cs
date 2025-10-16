using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _ctx;
        public TaskRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
            => await _ctx.Tasks.AsNoTracking().ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(int id)
            => await _ctx.Tasks.FindAsync(id);

        public async Task AddAsync(TaskItem task)
        {
            await _ctx.Tasks.AddAsync(task);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _ctx.Tasks.Update(task);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var t = await _ctx.Tasks.FindAsync(id);
            if (t != null)
            {
                _ctx.Tasks.Remove(t);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
