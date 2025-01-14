using Microsoft.Extensions.Hosting;
using ControlFlow.Core.Enums.TaskEnum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace ControlFlow.Services;

public class EmailReminderService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EmailReminderService> _logger;

    public EmailReminderService(IServiceScopeFactory serviceScopeFactory, ILogger<EmailReminderService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SendEmailReminder();
            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }

    private async Task SendEmailReminder()
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            IUserTaskService userTaskService = scope.ServiceProvider.GetRequiredService<IUserTaskService>();
            IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            IEnumerable<UserTask> tasksToProcess = await userTaskService.GetRangeTasksAsync(task => task.TaskStatus == CurrentTaskStatus.InProgress && DateTime.Now.AddMinutes(60) >= task.DueDate && !task.IsReminderSent) ?? Enumerable.Empty<UserTask>();
            //Used for checking if the email service still works
            //Will save if just in case I need it in the future
            //IEnumerable<UserTask> tasksToProcess = await userTaskService.GetRangeTasksAsync(task => task.TaskStatus == CurrentTaskStatus.InProgress && !task.IsReminderSent) ?? Enumerable.Empty<UserTask>();

            await Task.WhenAll(tasksToProcess.Select(task => TrySendEmailAsync(emailService, userTaskService, task)));
        }
    }
    private async Task TrySendEmailAsync(IEmailService emailService, IUserTaskService userTaskService, UserTask task)
    {
        try
        {
            string subject = "Task Due Date is Coming Soon!";
            string htmlBody = $@"
            <html>
                <body>
                    <h2>Task Due Date is Coming Soon!</h2>
                    <p>The task <strong>{task.Title}</strong> is due soon! Don't forget to complete it before the deadline.
                    </p>
                </body>
            </html>";
            string textBody = $"Task Due Date is Coming Soon!\n\nThe task {task.Title} is due soon! Don't forget to complete it before the deadline.";

            lock (task)
            {
                if (task.IsReminderSent)
                {
                    _logger.LogWarning("Reminder for task {TaskId} has already been sent", task.Id);
                    return;
                }
            }
            bool isEmailSentSuccessfully = await emailService.SendEmailAsync(task.User!.Email!, subject, htmlBody, textBody);
            if (isEmailSentSuccessfully)
            {
                lock (task)
                {
                    task.IsReminderSent = true;
                }

                userTaskService.UpdateTask(task);

                _logger.LogInformation("Reminder sent for task {TaskId} to user {UserId}", task.Id, task.UserId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send reminder email for task {TaskId}", task.Id);
        }
    }
}

