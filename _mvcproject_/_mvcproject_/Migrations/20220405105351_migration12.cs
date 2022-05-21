using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _mvcproject_.Migrations
{
    public partial class migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousWorkPhoto",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ProviderWorks",
                columns: table => new
                {
                    Provider_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderWorks", x => new { x.Provider_Id, x.ImgPath });
                    table.ForeignKey(
                        name: "FK_ProviderWorks_AspNetUsers_Provider_Id",
                        column: x => x.Provider_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProviderWorks");

            migrationBuilder.AddColumn<string>(
                name: "PreviousWorkPhoto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
