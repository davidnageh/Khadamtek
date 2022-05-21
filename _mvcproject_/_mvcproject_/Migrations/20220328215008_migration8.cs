using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _mvcproject_.Migrations
{
    public partial class migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 1,
                column: "ImgPath",
                value: "serviceslogos/carpenter.jpg");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 2,
                column: "ImgPath",
                value: "serviceslogos/courrier.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 3,
                column: "ImgPath",
                value: "serviceslogos/electronics.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 1,
                column: "ImgPath",
                value: "/serviceslogos/carpenter.jpg");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 2,
                column: "ImgPath",
                value: "/serviceslogos/courrier.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 3,
                column: "ImgPath",
                value: "/serviceslogos/electronics.jpg");
        }
    }
}
