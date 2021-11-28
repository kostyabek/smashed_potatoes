using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedUserBoardSubscriptions migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedUserBoardSubscriptions : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_board_subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    board_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_board_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_board_subscriptions_boards_board_id",
                        column: x => x.board_id,
                        principalTable: "boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_board_subscriptions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_board_subscriptions_board_id",
                table: "user_board_subscriptions",
                column: "board_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_board_subscriptions_user_id",
                table: "user_board_subscriptions",
                column: "user_id");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_board_subscriptions");
        }
    }
}
