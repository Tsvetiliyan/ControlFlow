using ControlFlow.Core;
using ControlFlow.Core.Entities;
using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Enums.TaskEnum;
using ControlFlow.Core.Interfaces.IServices;
using ControlFlow.Core.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ControlFlow.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationUserService _applicationUserService;

    public UserTaskService(IUnitOfWork unitOfWork, IApplicationUserService applicationUserService)
    {
        _unitOfWork = unitOfWork;
        _applicationUserService = applicationUserService;
    }

    public UserTask? GetTask(Expression<Func<UserTask, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return _unitOfWork.UserTaskRepo.Get(filter, "User");
    }

    public async Task<UserTask?> GetTaskAsync(Expression<Func<UserTask, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return await _unitOfWork.UserTaskRepo.GetAsync(filter, "User");
    }
    public IEnumerable<UserTask> GetRangeTasks(Expression<Func<UserTask, bool>>? filter = null)
    {
        if (filter == null)
        {
            return this.GetAllTasks();
        }
        return _unitOfWork.UserTaskRepo.GetRange(filter, "User");
    }

    public async Task<IEnumerable<UserTask>> GetRangeTasksAsync(Expression<Func<UserTask, bool>>? filter = null)
    {
        if (filter == null)
        {
            return await this.GetAllTasksAsync();
        }
        return await _unitOfWork.UserTaskRepo.GetRangeAsync(filter, "User");
    }

    public IEnumerable<UserTask> GetAllTasks()
    {
        return _unitOfWork.UserTaskRepo.GetAll("User");
    }

    public async Task<IEnumerable<UserTask>> GetAllTasksAsync()
    {
        return await _unitOfWork.UserTaskRepo.GetAllAsync("User");
    }

    public IEnumerable<UserTask> GetTasksByStatus(CurrentTaskStatus status)
    {
        return _unitOfWork.UserTaskRepo.GetRange(t => t.TaskStatus == status, "User");
    }

    public async Task<IEnumerable<UserTask>> GetTasksByStatusAsync(CurrentTaskStatus status)
    {
        return await _unitOfWork.UserTaskRepo.GetRangeAsync(t => t.TaskStatus == status, "User");
    }

    public IEnumerable<UserTask> GetTasksByPriority(TaskPriority priority)
    {
        return _unitOfWork.UserTaskRepo.GetRange(t => t.Priority == priority, "User");
    }

    public async Task<IEnumerable<UserTask>> GetTasksByPriorityAsync(TaskPriority priority)
    {
        return await _unitOfWork.UserTaskRepo.GetRangeAsync(t => t.Priority == priority, "User");
    }

    public async Task CreateTask(UserTask task)
    {
        List<ValidationResult> taskValidation = this.ValidateTask(task);
        if (taskValidation.Count == 0)
        {
            ApplicationUser? user = await _applicationUserService.GetUserByIdAsync(task.UserId);
            if (user == null)
            {
                return;
            }
            else if (user.SubscriptionPlan == Core.Enums.SubscriptionType.Pro)
            {
                _unitOfWork.UserTaskRepo.Add(task);
                _unitOfWork.SaveChanges();
            }
            //IEnumerable<UserTask> userTasks = await GetRangeTasksAsync(t => t.UserId == user.Id);
            //if (user.SubscriptionPlan == Core.Enums.SubscriptionType.Free && userTasks.Count() < 10)
            if (user.SubscriptionPlan == Core.Enums.SubscriptionType.Free)
            {
                _unitOfWork.UserTaskRepo.Add(task);
                _unitOfWork.SaveChanges();
            }
        }
    }

    public void CreateRangeTask(IEnumerable<UserTask> tasks)
    {
        List<ValidationResult> taskValidation = this.ValidateTasks(tasks);
        if (taskValidation.Count == 0)
        {
            _unitOfWork.UserTaskRepo.AddRange(tasks);
            _unitOfWork.SaveChanges();
        }
    }

    public void UpdateTask(UserTask task)
    {
        List<ValidationResult> taskValidation = this.ValidateTask(task);
        if (taskValidation.Count == 0)
        {
            _unitOfWork.UserTaskRepo.Update(task);
            _unitOfWork.SaveChanges();
        }
    }

    public bool DeleteTask(UserTask task)
    {
        List<ValidationResult> taskValidation = this.ValidateTask(task);
        if (taskValidation.Count == 0)
        {
            _unitOfWork.UserTaskRepo.Remove(task);
            return _unitOfWork.SaveChanges() > 0;
        }
        return false;
    }

    public bool DeleteRangeTask(IEnumerable<UserTask> tasks)
    {
        List<ValidationResult> taskValidation = this.ValidateTasks(tasks);
        if (taskValidation.Count == 0)
        {
            _unitOfWork.UserTaskRepo.RemoveRange(tasks);
            return _unitOfWork.SaveChanges() > 0;
        }
        return false;
    }

    public List<ValidationResult> ValidateTask(UserTask task)
    {
        var validationErrors = new List<ValidationResult>();
        if (task == null)
        {
            validationErrors.Add(new ValidationResult("The task cannot be null"));
        }
        else if (task.DueDate < DateTime.Now && task.Id == 0)
        {
            validationErrors.Add(new ValidationResult("The due date cannot be in the past.", new[] { nameof(task.DueDate) }));
        }
        else if (string.IsNullOrWhiteSpace(task.Title))
        {
            validationErrors.Add(new ValidationResult("The title cannot be empty", new[] {nameof(task.Title)}));
        }
        return validationErrors;
    }
    public List<ValidationResult> ValidateTasks(IEnumerable<UserTask> tasks)
    {
        var validationErrors = new List<ValidationResult>();
        if (tasks.Any() || tasks == null)
        {
            validationErrors.Add(new ValidationResult("List of tasks cannot be null or empty"));
        }
        foreach (var task in tasks ?? Enumerable.Empty<UserTask>())
        {
            var taskErrors = ValidateTask(task);

            if (taskErrors != null && taskErrors.Count != 0)
            {
                validationErrors.AddRange(taskErrors);
            }
        }
        return validationErrors;
    }
}