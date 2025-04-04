using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByAccountId",
                table: "Surveys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreatedByAccountId",
                table: "Surveys",
                column: "CreatedByAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Accounts_CreatedByAccountId",
                table: "Surveys",
                column: "CreatedByAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Accounts_CreatedByAccountId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_CreatedByAccountId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "CreatedByAccountId",
                table: "Surveys");
        }
    }
}
