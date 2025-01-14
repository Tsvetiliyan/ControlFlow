using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Enums.TaskEnum;

namespace ControlFlow.Core.Interfaces.IRepositories;

public interface IUserTaskRepo : IRepository<UserTask>
{
    IEnumerable<UserTask> GetTasksByStatus(CurrentTaskStatus taskStatus);
    IEnumerable<UserTask> GetTasksByPriority(TaskPriority taskPriority);
    Task<IEnumerable<UserTask>> GetTasksByStatusAsync(CurrentTaskStatus taskStatus);
    Task<IEnumerable<UserTask>> GetTasksByPriorityAsync(TaskPriority taskPriority);
}
