using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LastSyncedCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "SurveyBlocks");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "BlockElements");

            migrationBuilder.AddColumn<string>(
                name: "QuestionId",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Questions");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSyncedAt",
                table: "SurveyBlocks",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSyncedAt",
                table: "Questions",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSyncedAt",
                table: "Choices",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSyncedAt",
                table: "BlockElements",
                type: "datetimeoffset",
                nullable: true);
        }
    }
}
