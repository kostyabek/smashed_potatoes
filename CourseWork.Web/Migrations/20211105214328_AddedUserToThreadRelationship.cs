using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedUserToThreadRelationship migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedUserToThreadRelationship : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "threads",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_threads_user_id",
                table: "threads",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_threads_users_user_id",
                table: "threads",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_threads_users_user_id",
                table: "threads");

            migrationBuilder.DropIndex(
                name: "ix_threads_user_id",
                table: "threads");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "threads");
        }
    }
}
