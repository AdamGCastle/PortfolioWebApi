using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class MultipleChoiceOptionResponse2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptionResponse_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptionResponse_QuestionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceOptionResponse",
                table: "MultipleChoiceOptionResponse");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceOptionResponse",
                newName: "MultipleChoiceOptionResponses");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceOptionResponse_QuestionResponseId",
                table: "MultipleChoiceOptionResponses",
                newName: "IX_MultipleChoiceOptionResponses_QuestionResponseId");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceOptionResponse_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponses",
                newName: "IX_MultipleChoiceOptionResponses_MultipleChoiceOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceOptionResponses",
                table: "MultipleChoiceOptionResponses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponses",
                column: "MultipleChoiceOptionId",
                principalTable: "MultipleChoiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptionResponses_QuestionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponses",
                column: "QuestionResponseId",
                principalTable: "QuestionResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptionResponses_QuestionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceOptionResponses",
                table: "MultipleChoiceOptionResponses");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceOptionResponses",
                newName: "MultipleChoiceOptionResponse");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceOptionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponse",
                newName: "IX_MultipleChoiceOptionResponse_QuestionResponseId");

            migrationBuilder.RenameIndex(
                name: "IX_MultipleChoiceOptionResponses_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponse",
                newName: "IX_MultipleChoiceOptionResponse_MultipleChoiceOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceOptionResponse",
                table: "MultipleChoiceOptionResponse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptionResponse_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponse",
                column: "MultipleChoiceOptionId",
                principalTable: "MultipleChoiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptionResponse_QuestionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponse",
                column: "QuestionResponseId",
                principalTable: "QuestionResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
