using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum_descussion_ASP.NET_core_mvc.Migrations
{
    public partial class essai2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseModel_UserModel_UserID",
                table: "ResponseModel");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ResponseModel",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseModel_UserID",
                table: "ResponseModel",
                newName: "IX_ResponseModel_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseModel_UserModel_UserId",
                table: "ResponseModel",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseModel_UserModel_UserId",
                table: "ResponseModel");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ResponseModel",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseModel_UserId",
                table: "ResponseModel",
                newName: "IX_ResponseModel_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseModel_UserModel_UserID",
                table: "ResponseModel",
                column: "UserID",
                principalTable: "UserModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
