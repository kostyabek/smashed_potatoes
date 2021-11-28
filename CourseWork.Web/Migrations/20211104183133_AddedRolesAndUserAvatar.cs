using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CourseWork.Web.Migrations
{
    /// <summary>
    /// AddedRolesAndUserAvatar migration.
    /// </summary>
    /// <seealso cref="Migration" />
    public partial class AddedRolesAndUserAvatar : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_users_user_id",
                table: "users_roles");

            migrationBuilder.AddColumn<int>(
                name: "avatar_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 1, "2e6e4c4b-43d0-4a0d-9a9c-320c24260476", "Admin", "ADMIN" },
                    { 2, "41b26433-bda8-4fd6-954e-e6b947810df2", "Moderator", "MODERATOR" },
                    { 3, "07c3c173-38c0-444a-a5a6-511507b5e590", "User", "USER" },
                    { 4, "ab109c0f-d32f-4615-9ae2-b928afdc8088", "New user", "NEW USER" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_avatar_id",
                table: "users",
                column: "avatar_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_images_avatar_id",
                table: "users",
                column: "avatar_id",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_users_user_id",
                table: "users_roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_images_avatar_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_users_user_id",
                table: "users_roles");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropIndex(
                name: "ix_users_avatar_id",
                table: "users");

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

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "avatar_id",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_users_user_id",
                table: "users_roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
