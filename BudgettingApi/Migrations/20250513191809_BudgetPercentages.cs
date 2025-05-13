using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgettingApi.Migrations
{
    /// <inheritdoc />
    public partial class BudgetPercentages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "budget_percentage",
                table: "user_budget",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "budget_percentage",
                table: "user_budget");
        }
    }
}
