using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _mvcproject_.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Client_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Provider_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    Complaint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Client_Id",
                        column: x => x.Client_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Provider_Id",
                        column: x => x.Provider_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Client_Id",
                table: "Orders",
                column: "Client_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Provider_Id",
                table: "Orders",
                column: "Provider_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
