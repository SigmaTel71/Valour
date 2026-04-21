using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Valour.Database.Migrations
{
    /// <inheritdoc />
    public partial class MUI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "always_show_time",
                table: "user_preferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "user_preferences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "sync_language_between_devices",
                table: "user_preferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "time_format",
                table: "user_preferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "use_relative_time",
                table: "user_preferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "always_show_time",
                table: "user_preferences");

            migrationBuilder.DropColumn(
                name: "language",
                table: "user_preferences");

            migrationBuilder.DropColumn(
                name: "sync_language_between_devices",
                table: "user_preferences");

            migrationBuilder.DropColumn(
                name: "time_format",
                table: "user_preferences");

            migrationBuilder.DropColumn(
                name: "use_relative_time",
                table: "user_preferences");
        }
    }
}
