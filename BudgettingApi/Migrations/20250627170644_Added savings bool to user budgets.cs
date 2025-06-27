using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgettingApi.Migrations
{
    /// <inheritdoc />
    public partial class Addedsavingsbooltouserbudgets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_savings",
                table: "user_budget",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_savings",
                table: "user_budget");
        }
    }
}
