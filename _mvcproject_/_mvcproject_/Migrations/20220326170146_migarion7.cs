using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _mvcproject_.Migrations
{
    public partial class migarion7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Services_Service_Id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Service_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Services_Service_Id",
                table: "AspNetUsers",
                column: "Service_Id",
                principalTable: "Services",
                principalColumn: "Service_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Services_Service_Id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Service_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Services_Service_Id",
                table: "AspNetUsers",
                column: "Service_Id",
                principalTable: "Services",
                principalColumn: "Service_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
