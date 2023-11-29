using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum_descussion_ASP.NET_core_mvc.Migrations
{
    public partial class updateEnregistrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "UserModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EnregistrementModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ResponseId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnregistrementModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnregistrementModel_QuestionModel_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnregistrementModel_ResponseModel_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "ResponseModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnregistrementModel_QuestionId",
                table: "EnregistrementModel",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnregistrementModel_ResponseId",
                table: "EnregistrementModel",
                column: "ResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnregistrementModel");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "UserModel");
        }
    }
}
