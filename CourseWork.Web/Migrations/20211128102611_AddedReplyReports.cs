using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedReplyReports migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedReplyReports : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "report_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_report_reasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reply_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reply_id = table.Column<int>(type: "integer", nullable: false),
                    report_reason_id = table.Column<int>(type: "integer", nullable: false),
                    explanation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reply_reports", x => x.id);
                    table.ForeignKey(
                        name: "fk_reply_reports_replies_reply_id",
                        column: x => x.reply_id,
                        principalTable: "replies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reply_reports_report_reasons_report_reason_id",
                        column: x => x.report_reason_id,
                        principalTable: "report_reasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "report_reasons",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Inappropriate" },
                    { 2, "Bullying" },
                    { 3, "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_reply_reports_reply_id",
                table: "reply_reports",
                column: "reply_id");

            migrationBuilder.CreateIndex(
                name: "ix_reply_reports_report_reason_id",
                table: "reply_reports",
                column: "report_reason_id");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reply_reports");

            migrationBuilder.DropTable(
                name: "report_reasons");
        }
    }
}
