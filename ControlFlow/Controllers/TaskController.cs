using ControlFlow.Core.Entities;
using ControlFlow.Core.Entities.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ControlFlow.Core.Enums.TaskEnum;
using System.ComponentModel.DataAnnotations;
using ControlFlow.Core.Interfaces.IServices;
using ControlFlow.ViewModels;
namespace ControlFlow.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(IUserTaskService userTaskService, UserManager<ApplicationUser> userManager)
        {
            _userTaskService = userTaskService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Main()
        {
            string? userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToPage("Account/AccessDenied", new { area = "Identity" });
            }

            List<UserTask> PendingTasks = _userTaskService.GetRangeTasks(t => t.UserId == userId && t.TaskStatus == CurrentTaskStatus.Pending).ToList();
            List<UserTask> InProgressTasks = _userTaskService.GetRangeTasks(t => t.UserId == userId && t.TaskStatus == CurrentTaskStatus.InProgress).ToList();
            List<UserTask> CompletedTasks = _userTaskService.GetRangeTasks(t => t.UserId == userId && t.TaskStatus == CurrentTaskStatus.Completed).ToList();
            MainViewModel mainViewModel = new(PendingTasks, InProgressTasks, CompletedTasks);
            return View(mainViewModel);
        }
        [HttpGet]
        [Authorize]
        public IActionResult TaskDetails(int taskId)
        {
            UserTask? task = _userTaskService.GetTask(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            else if (_userManager.GetUserId(User) != task.UserId)
            {
                return Forbid("You are not authorized to modify this task.");
            }
            return View(task);
        }
        [HttpGet]
        [Authorize]
        [Route("Task/UpsertTask/{taskId?}")]
        public IActionResult UpsertTask(int? taskId)
        {
            TaskViewModel taskViewModel;
            if (taskId == null)
            {
                taskViewModel = new TaskViewModel(new UserTask());
            }
            else
            {
                UserTask? task = _userTaskService.GetTask(t => t.Id == taskId);
                if (task == null)
                {
                    return NotFound("Task not found.");
                }
                else if (_userManager.GetUserId(User) != task.UserId)
                {
                    return Forbid("You are not authorized to modify this task.");
                }
                else
                {
                    taskViewModel = new TaskViewModel(task);
                }
            }
            return View(taskViewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpsertTask(TaskViewModel taskViewModel)
        {
            ModelState.Remove("TastStatus");
            ModelState.Remove("TaskCategories");
            ModelState.Remove("Task.User");
            List<ValidationResult> taskValidation = _userTaskService.ValidateTask(taskViewModel.Task);
            if (ModelState.IsValid && taskValidation.Count == 0)
            {
                if (taskViewModel.Task.Id == 0)
                {
                    await _userTaskService.CreateTask(taskViewModel.Task);
                }
                else
                {
                    _userTaskService.UpdateTask(taskViewModel.Task);
                }
                return RedirectToAction("Main");
            }
            else
            {
                foreach (var error in taskValidation)
                {
                    ModelState.AddModelError($"Task.{error.MemberNames.First()}", error.ErrorMessage!);
                }
                return View(new TaskViewModel(taskViewModel.Task));
            }
        }
        [HttpGet]
        [Authorize]
        [Route("Task/DeleteTask/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            UserTask? task = _userTaskService.GetTask(t => t.Id == taskId);
            if (task == null)
            {
                
            }
            else if (_userManager.GetUserId(User) != task.UserId || task == null)
            {

            }
            else
            {
                _userTaskService.DeleteTask(task);
            }
            return RedirectToAction("Main");
        }
        [HttpGet]
        [Authorize]
        [Route("Task/ChangeTaskPriority/{taskId}")]
        public IActionResult ChangeTaskPriority(int taskId)
        {
            UserTask? task = _userTaskService.GetTask(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            else if (_userManager.GetUserId(User) != task.UserId)
            {
                return Forbid("You are not authorized to modify this task.");
            }
            else
            {
                task.TaskStatus = (CurrentTaskStatus)(((int)task.TaskStatus + 1) % Enum.GetValues<CurrentTaskStatus>().Length);
                _userTaskService.UpdateTask(task);
            }
            return RedirectToAction("Main");
        }
    }
}
