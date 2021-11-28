using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedReporterToReplyReport migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedReporterToReplyReport : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reporter_id",
                table: "reply_reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_reply_reports_reporter_id",
                table: "reply_reports",
                column: "reporter_id");

            migrationBuilder.AddForeignKey(
                name: "fk_reply_reports_users_reporter_id",
                table: "reply_reports",
                column: "reporter_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reply_reports_users_reporter_id",
                table: "reply_reports");

            migrationBuilder.DropIndex(
                name: "ix_reply_reports_reporter_id",
                table: "reply_reports");

            migrationBuilder.DropColumn(
                name: "reporter_id",
                table: "reply_reports");
        }
    }
}
