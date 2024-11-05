using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class MultipleChoiceOptionResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "QuestionResponses");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_MultipleChoiceOptionId",
                table: "QuestionResponses");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "QuestionResponses");

            migrationBuilder.CreateTable(
                name: "MultipleChoiceOptionResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MultipleChoiceOptionId = table.Column<int>(type: "int", nullable: false),
                    QuestionResponseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceOptionResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceOptionResponse_MultipleChoiceOptions_MultipleChoiceOptionId",
                        column: x => x.MultipleChoiceOptionId,
                        principalTable: "MultipleChoiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceOptionResponse_QuestionResponses_QuestionResponseId",
                        column: x => x.QuestionResponseId,
                        principalTable: "QuestionResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceOptionResponse_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponse",
                column: "MultipleChoiceOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceOptionResponse_QuestionResponseId",
                table: "MultipleChoiceOptionResponse",
                column: "QuestionResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleChoiceOptionResponse");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "QuestionResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_MultipleChoiceOptionId",
                table: "QuestionResponses",
                column: "MultipleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "QuestionResponses",
                column: "MultipleChoiceOptionId",
                principalTable: "MultipleChoiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
