using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorResponseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_AnswerOptions_AnswerOptionId",
                table: "Responses");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropIndex(
                name: "IX_Responses_AnswerOptionId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "AnswerOptionId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Responses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnswerOptionId",
                table: "Responses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Responses",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IndexId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responses_AnswerOptionId",
                table: "Responses",
                column: "AnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_SurveyId",
                table: "AnswerOptions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_AnswerOptions_AnswerOptionId",
                table: "Responses",
                column: "AnswerOptionId",
                principalTable: "AnswerOptions",
                principalColumn: "Id");
        }
    }
}
