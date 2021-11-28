using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// ChangedImageFilePathToFileName migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class ChangedImageFilePathToFileName : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_path",
                table: "images",
                newName: "file_name");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "images",
                newName: "file_path");
        }
    }
}
