using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUser_User_UserId",
                table: "ResponseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Surveys_SurveyId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_SurveyId",
                table: "Users",
                newName: "IX_Users_SurveyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUser_Users_UserId",
                table: "ResponseUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Surveys_SurveyId",
                table: "Users",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUser_Users_UserId",
                table: "ResponseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Surveys_SurveyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SurveyId",
                table: "User",
                newName: "IX_User_SurveyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUser_User_UserId",
                table: "ResponseUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Surveys_SurveyId",
                table: "User",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }
    }
}
