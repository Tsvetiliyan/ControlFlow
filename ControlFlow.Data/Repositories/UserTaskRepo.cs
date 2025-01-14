using ControlFlow.Core.Entities;
using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Enums.TaskEnum;
using ControlFlow.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ControlFlow.Data.Repositories;

public class UserTaskRepo : Repository<UserTask>, IUserTaskRepo
{
    public UserTaskRepo(IdentityAppContext context) : base(context)
    {

    }

    public IEnumerable<UserTask> GetTasksByPriority(TaskPriority taskPriority)
    {
        return _dbContext.Set<UserTask>().Where(t => t.Priority == taskPriority);
    }
    public IEnumerable<UserTask> GetTasksByStatus(CurrentTaskStatus taskStatus)
    {
        return _dbContext.Set<UserTask>().Where(t => t.TaskStatus == taskStatus);
    }

    public async Task<IEnumerable<UserTask>> GetTasksByPriorityAsync(TaskPriority taskPriority)
    {
        return await _dbContext.Set<UserTask>().Where(t => t.Priority == taskPriority).ToListAsync();
    }
    public async Task<IEnumerable<UserTask>> GetTasksByStatusAsync(CurrentTaskStatus taskStatus)
    {
        return await _dbContext.Set<UserTask>().Where(t => t.TaskStatus == taskStatus).ToListAsync();
    }
}
