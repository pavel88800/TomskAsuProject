using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiApplication.Migrations
{
    public partial class AddListSkillOnPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Personid",
                table: "Skills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Personid",
                table: "Skills",
                column: "Personid");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_Personid",
                table: "Skills",
                column: "Personid",
                principalTable: "Persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_Personid",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_Personid",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Personid",
                table: "Skills");
        }
    }
}
