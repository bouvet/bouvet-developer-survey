using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorResponseTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Choices_ChoiceId",
                table: "Responses");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Choices_ChoiceId",
                table: "Responses",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Choices_ChoiceId",
                table: "Responses");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Choices_ChoiceId",
                table: "Responses",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id");
        }
    }
}
