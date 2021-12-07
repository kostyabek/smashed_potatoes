using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// ReseededRoles migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class ReseededRoles : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 1, "2e6e4c4b-43d0-4a0d-9a9c-320c24260476", "Admin", "ADMIN" },
                    { 2, "07c3c173-38c0-444a-a5a6-511507b5e590", "User", "USER" },
                    { 3, "ab109c0f-d32f-4615-9ae2-b928afdc8088", "New user", "NEW USER" }
                });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
