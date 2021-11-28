using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedDisplayNameToUser migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedDisplayNameToUser : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_name",
                table: "users");
        }
    }
}
