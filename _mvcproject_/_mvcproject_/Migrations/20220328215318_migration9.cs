using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _mvcproject_.Migrations
{
    public partial class migration9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 1,
                column: "ImgPath",
                value: "servicelogos/carpenter.jpg");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 2,
                column: "ImgPath",
                value: "servicelogos/courrier.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_Id",
                keyValue: 3,
                column: "ImgPath",
                value: "servicelogos/electronics.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
