using ControlFlow.Core;
using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Enums.TaskEnum;
using ControlFlow.Core.UI.Components.SVG;
using System.Collections.Generic;

namespace ControlFlow.ViewModels;

public class MainViewModel
{
    public static readonly string[] taskListContainersHeader = { "Pending", "In Progress", "Completed" };
    public static readonly SvgIcon[] taskListContainersHeaderSVGs = { SvgLibrary.Plus, SvgLibrary.InProgress, SvgLibrary.Checkmark };
    public static readonly SvgIcon dueDateIcon = SvgLibrary.Calendar;
    public MainViewModel()
    {
        PendingTasks = new List<UserTask>();
        InProgressTasks = new List<UserTask>();
        CompletedTasks = new List<UserTask>();
        TasksListsContainer = [PendingTasks, InProgressTasks, CompletedTasks];
    }

    public MainViewModel(List<UserTask> pendingTasks, List<UserTask> inProgressTasks, List<UserTask> completedTasks)
    {
        PendingTasks = pendingTasks;
        InProgressTasks = inProgressTasks;
        CompletedTasks = completedTasks;
        TasksListsContainer = [PendingTasks, InProgressTasks, CompletedTasks];
    }
    public List<List<UserTask>> TasksListsContainer { get; set; }
    public List<UserTask> PendingTasks { get; set; }
    public List<UserTask> InProgressTasks { get; set; }
    public List<UserTask> CompletedTasks { get; set; }

}
