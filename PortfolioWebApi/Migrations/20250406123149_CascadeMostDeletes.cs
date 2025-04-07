using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class CascadeMostDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptionResponses_QuestionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptions_Questions_QuestionId",
                table: "MultipleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_Questions_QuestionId",
                table: "QuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_SurveyResponses_SurveyResponseId",
                table: "QuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptionResponses_MultipleChoiceOptions_MultipleChoiceOptionId",
                table: "MultipleChoiceOptionResponses",
                column: "MultipleChoiceOptionId",
                principalTable: "MultipleChoiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptionResponses_QuestionResponses_QuestionResponseId",
                table: "MultipleChoiceOptionResponses",
                column: "QuestionResponseId",
                principalTable: "QuestionResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptions_Questions_QuestionId",
                table: "MultipleChoiceOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_Questions_QuestionId",
                table: "QuestionResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_SurveyResponses_SurveyResponseId",
                table: "QuestionResponses",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceOptions_Questions_QuestionId",
                table: "MultipleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_Questions_QuestionId",
                table: "QuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_SurveyResponses_SurveyResponseId",
                table: "QuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceOptions_Questions_QuestionId",
                table: "MultipleChoiceOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_Questions_QuestionId",
                table: "QuestionResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_SurveyResponses_SurveyResponseId",
                table: "QuestionResponses",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
