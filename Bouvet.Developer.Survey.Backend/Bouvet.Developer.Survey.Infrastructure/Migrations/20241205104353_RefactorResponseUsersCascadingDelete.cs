using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorResponseUsersCascadingDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUsers_Responses_ResponseId",
                table: "ResponseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUsers_Responses_ResponseId",
                table: "ResponseUsers",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUsers_Responses_ResponseId",
                table: "ResponseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUsers_Responses_ResponseId",
                table: "ResponseUsers",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id");
        }
    }
}
