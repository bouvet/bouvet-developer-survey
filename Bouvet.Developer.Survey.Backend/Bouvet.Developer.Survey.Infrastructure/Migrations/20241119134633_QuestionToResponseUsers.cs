using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuestionToResponseUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "ResponseUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ResponseUsers_QuestionId",
                table: "ResponseUsers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseUsers_Questions_QuestionId",
                table: "ResponseUsers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseUsers_Questions_QuestionId",
                table: "ResponseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ResponseUsers_QuestionId",
                table: "ResponseUsers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "ResponseUsers");
        }
    }
}
