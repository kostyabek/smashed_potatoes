using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedTimeStampToReportModel migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedTimeStampToReportModel : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "reply_reports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "reply_reports");
        }
    }
}
