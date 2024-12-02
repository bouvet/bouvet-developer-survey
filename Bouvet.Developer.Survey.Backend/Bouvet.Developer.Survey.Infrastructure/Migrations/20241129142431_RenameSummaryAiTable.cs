using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameSummaryAiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ai_analyses_Questions_QuestionId",
                table: "ai_analyses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ai_analyses",
                table: "ai_analyses");

            migrationBuilder.RenameTable(
                name: "ai_analyses",
                newName: "AiAnalyses");

            migrationBuilder.RenameIndex(
                name: "IX_ai_analyses_QuestionId",
                table: "AiAnalyses",
                newName: "IX_AiAnalyses_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AiAnalyses",
                table: "AiAnalyses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AiAnalyses_Questions_QuestionId",
                table: "AiAnalyses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiAnalyses_Questions_QuestionId",
                table: "AiAnalyses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AiAnalyses",
                table: "AiAnalyses");

            migrationBuilder.RenameTable(
                name: "AiAnalyses",
                newName: "ai_analyses");

            migrationBuilder.RenameIndex(
                name: "IX_AiAnalyses_QuestionId",
                table: "ai_analyses",
                newName: "IX_ai_analyses_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ai_analyses",
                table: "ai_analyses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ai_analyses_Questions_QuestionId",
                table: "ai_analyses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
