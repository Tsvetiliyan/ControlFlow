using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsReminderSentToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReminderSent",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReminderSent",
                table: "Tasks");
        }
    }
}
