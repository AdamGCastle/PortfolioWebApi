using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class multipleAnswersPermitted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MultipleAnswersPermitted",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleAnswersPermitted",
                table: "Questions");
        }
    }
}
