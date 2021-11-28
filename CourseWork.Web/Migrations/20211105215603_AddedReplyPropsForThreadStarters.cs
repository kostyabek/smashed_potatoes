using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedReplyPropsForThreadStarters migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedReplyPropsForThreadStarters : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reply_reply_replies_pointed_reply_id",
                table: "reply_reply");

            migrationBuilder.DropForeignKey(
                name: "fk_reply_reply_replies_pointing_reply_id",
                table: "reply_reply");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reply_reply",
                table: "reply_reply");

            migrationBuilder.RenameTable(
                name: "reply_reply",
                newName: "reply_replies");

            migrationBuilder.RenameIndex(
                name: "ix_reply_reply_pointed_reply_id",
                table: "reply_replies",
                newName: "ix_reply_replies_pointed_reply_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_thread_starter",
                table: "replies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_reply_replies",
                table: "reply_replies",
                columns: new[] { "pointing_reply_id", "pointed_reply_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_reply_replies_replies_pointed_reply_id",
                table: "reply_replies",
                column: "pointed_reply_id",
                principalTable: "replies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reply_replies_replies_pointing_reply_id",
                table: "reply_replies",
                column: "pointing_reply_id",
                principalTable: "replies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reply_replies_replies_pointed_reply_id",
                table: "reply_replies");

            migrationBuilder.DropForeignKey(
                name: "fk_reply_replies_replies_pointing_reply_id",
                table: "reply_replies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reply_replies",
                table: "reply_replies");

            migrationBuilder.DropColumn(
                name: "is_thread_starter",
                table: "replies");

            migrationBuilder.RenameTable(
                name: "reply_replies",
                newName: "reply_reply");

            migrationBuilder.RenameIndex(
                name: "ix_reply_replies_pointed_reply_id",
                table: "reply_reply",
                newName: "ix_reply_reply_pointed_reply_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_reply_reply",
                table: "reply_reply",
                columns: new[] { "pointing_reply_id", "pointed_reply_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_reply_reply_replies_pointed_reply_id",
                table: "reply_reply",
                column: "pointed_reply_id",
                principalTable: "replies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reply_reply_replies_pointing_reply_id",
                table: "reply_reply",
                column: "pointing_reply_id",
                principalTable: "replies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
