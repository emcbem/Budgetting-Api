using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgettingApi.Migrations
{
    /// <inheritdoc />
    public partial class UsersWithProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "provider_id",
                table: "user_account",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "provider_id",
                table: "user_account");
        }
    }
}
