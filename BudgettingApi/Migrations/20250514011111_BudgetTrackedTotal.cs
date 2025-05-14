using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgettingApi.Migrations
{
    /// <inheritdoc />
    public partial class BudgetTrackedTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "current_saved_total",
                table: "user_budget",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_saved_total",
                table: "user_budget");
        }
    }
}
