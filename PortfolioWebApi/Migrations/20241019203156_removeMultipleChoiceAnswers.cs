using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class removeMultipleChoiceAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_MultipleChoiceAnwsers_MultipleChoiceAnswerId",
                table: "QuestionResponses");

            migrationBuilder.DropTable(
                name: "MultipleChoiceAnwsers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_MultipleChoiceAnswerId",
                table: "QuestionResponses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultipleChoiceAnwsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceAnwsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceAnwsers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_MultipleChoiceAnswerId",
                table: "QuestionResponses",
                column: "MultipleChoiceAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceAnwsers_QuestionId",
                table: "MultipleChoiceAnwsers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_MultipleChoiceAnwsers_MultipleChoiceAnswerId",
                table: "QuestionResponses",
                column: "MultipleChoiceAnswerId",
                principalTable: "MultipleChoiceAnwsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
