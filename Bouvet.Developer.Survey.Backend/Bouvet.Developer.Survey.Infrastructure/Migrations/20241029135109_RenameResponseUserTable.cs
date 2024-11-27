using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameResponseUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUser_Responses_ResponseId",
                table: "ResponseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUser_Users_UserId",
                table: "ResponseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponseUser",
                table: "ResponseUser");

            migrationBuilder.RenameTable(
                name: "ResponseUser",
                newName: "ResponseUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseUser_UserId",
                table: "ResponseUsers",
                newName: "IX_ResponseUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponseUsers",
                table: "ResponseUsers",
                columns: new[] { "ResponseId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUsers_Responses_ResponseId",
                table: "ResponseUsers",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUsers_Users_UserId",
                table: "ResponseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUsers_Responses_ResponseId",
                table: "ResponseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUsers_Users_UserId",
                table: "ResponseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponseUsers",
                table: "ResponseUsers");

            migrationBuilder.RenameTable(
                name: "ResponseUsers",
                newName: "ResponseUser");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseUsers_UserId",
                table: "ResponseUser",
                newName: "IX_ResponseUser_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponseUser",
                table: "ResponseUser",
                columns: new[] { "ResponseId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUser_Responses_ResponseId",
                table: "ResponseUser",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUser_Users_UserId",
                table: "ResponseUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
