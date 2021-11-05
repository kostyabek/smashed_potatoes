using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CourseWork.Web.Migrations
{
    public partial class AddedBoardsThreadsAndReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    display_name = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "threads",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    board_id = table.Column<int>(type: "integer", nullable: false),
                    main_picture_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_threads", x => x.id);
                    table.ForeignKey(
                        name: "fk_threads_boards_board_id",
                        column: x => x.board_id,
                        principalTable: "boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_threads_images_main_picture_id",
                        column: x => x.main_picture_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "replies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    thread_id = table.Column<int>(type: "integer", nullable: false),
                    pic_related_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_replies", x => x.id);
                    table.ForeignKey(
                        name: "fk_replies_images_pic_related_id",
                        column: x => x.pic_related_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_replies_threads_thread_id",
                        column: x => x.thread_id,
                        principalTable: "threads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_replies_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reply_reply",
                columns: table => new
                {
                    pointing_reply_id = table.Column<int>(type: "integer", nullable: false),
                    pointed_reply_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reply_reply", x => new { x.pointing_reply_id, x.pointed_reply_id });
                    table.ForeignKey(
                        name: "fk_reply_reply_replies_pointed_reply_id",
                        column: x => x.pointed_reply_id,
                        principalTable: "replies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reply_reply_replies_pointing_reply_id",
                        column: x => x.pointing_reply_id,
                        principalTable: "replies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_boards_display_name",
                table: "boards",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_boards_name",
                table: "boards",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_replies_pic_related_id",
                table: "replies",
                column: "pic_related_id");

            migrationBuilder.CreateIndex(
                name: "ix_replies_thread_id",
                table: "replies",
                column: "thread_id");

            migrationBuilder.CreateIndex(
                name: "ix_replies_user_id",
                table: "replies",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reply_reply_pointed_reply_id",
                table: "reply_reply",
                column: "pointed_reply_id");

            migrationBuilder.CreateIndex(
                name: "ix_threads_board_id",
                table: "threads",
                column: "board_id");

            migrationBuilder.CreateIndex(
                name: "ix_threads_main_picture_id",
                table: "threads",
                column: "main_picture_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reply_reply");

            migrationBuilder.DropTable(
                name: "replies");

            migrationBuilder.DropTable(
                name: "threads");

            migrationBuilder.DropTable(
                name: "boards");
        }
    }
}
