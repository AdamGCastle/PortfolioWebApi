using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addMultipleChoiceOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MultipleChoiceAnswerId",
                table: "QuestionResponses",
                newName: "MultipleChoiceOptionId");

            migrationBuilder.CreateTable(
                name: "MultipleChoiceOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_MultipleChoiceOptionId",
                table: "QuestionResponses",
                column: "MultipleChoiceOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceOptions_QuestionId",
                table: "MultipleChoiceOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "QuestionResponses",
                column: "MultipleChoiceOptionId",
                principalTable: "MultipleChoiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "QuestionResponses");

            migrationBuilder.DropTable(
                name: "MultipleChoiceOptions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_MultipleChoiceOptionId",
                table: "QuestionResponses");

            migrationBuilder.RenameColumn(
                name: "MultipleChoiceOptionId",
                table: "QuestionResponses",
                newName: "MultipleChoiceAnswerId");
        }
    }
}
