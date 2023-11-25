using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum_descussion_ASP.NET_core_mvc.Migrations
{
    public partial class enregisrement_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserModel",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ResponseModel",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserModel",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ResponseModel",
                newName: "ID");
        }
    }
}
