using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum_descussion_ASP.NET_core_mvc.Migrations
{
    public partial class essai1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRepondu",
                table: "QuestionModel",
                newName: "isResolu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isResolu",
                table: "QuestionModel",
                newName: "isRepondu");
        }
    }
}
