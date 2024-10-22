using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResultsFromSurveyWithFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "Responses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "Responses");
        }
    }
}
