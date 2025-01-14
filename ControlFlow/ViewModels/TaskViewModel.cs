using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Enums.TaskEnum;

namespace ControlFlow.ViewModels;

public class TaskViewModel
{
    public TaskViewModel()
    {

    }
    public TaskViewModel(UserTask task)
    {
        Task = task;
        TaskCategories = Enum.GetValues<TaskCategory>().Cast<TaskCategory>().ToList();
        TastStatus = Enum.GetValues<CurrentTaskStatus>().Cast<CurrentTaskStatus>().ToList();
    }
    public List<TaskCategory> TaskCategories { get; set; } = [];
    public List<CurrentTaskStatus> TastStatus { get; set; } = [];
    public UserTask Task { get; set; } = new UserTask();

}
