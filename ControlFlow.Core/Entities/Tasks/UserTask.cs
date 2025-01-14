using ControlFlow.Core.Dtos;
using ControlFlow.Core.Enums.TaskEnum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlFlow.Core.Entities.Tasks;

public class UserTask
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = "";

    [StringLength(500)]
    public string Description { get; set; } = "";

    [Required]
    public TaskPriority Priority { get; set; }

    [Required]
    public TaskCategory Category { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; } = "";

    [Required]
    public ApplicationUser? User { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DueDate { get; set; }

    [Required]
    public CurrentTaskStatus TaskStatus { get; set; }

    [Required]
    public bool IsReminderSent { get; set; }

}
