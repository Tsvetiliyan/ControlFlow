using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Enums.TaskEnum;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ControlFlow.Core.Interfaces.IServices;

public interface IUserTaskService
{
    UserTask? GetTask(Expression<Func<UserTask, bool>> filter);
    Task<UserTask?> GetTaskAsync(Expression<Func<UserTask, bool>> filter);
    IEnumerable<UserTask> GetRangeTasks(Expression<Func<UserTask, bool>> filter);
    Task<IEnumerable<UserTask>> GetRangeTasksAsync(Expression<Func<UserTask, bool>> filter);
    IEnumerable<UserTask> GetAllTasks();
    Task<IEnumerable<UserTask>> GetAllTasksAsync();
    IEnumerable<UserTask> GetTasksByStatus(CurrentTaskStatus status);
    Task<IEnumerable<UserTask>> GetTasksByStatusAsync(CurrentTaskStatus status);
    IEnumerable<UserTask> GetTasksByPriority(TaskPriority priority);
    Task<IEnumerable<UserTask>> GetTasksByPriorityAsync(TaskPriority priority);
    Task CreateTask(UserTask task);
    void CreateRangeTask(IEnumerable<UserTask> task);
    void UpdateTask(UserTask task);
    bool DeleteTask(UserTask task);
    bool DeleteRangeTask(IEnumerable<UserTask> task);
    List<ValidationResult> ValidateTask(UserTask task);
    List<ValidationResult> ValidateTasks(IEnumerable<UserTask> tasks);

}
