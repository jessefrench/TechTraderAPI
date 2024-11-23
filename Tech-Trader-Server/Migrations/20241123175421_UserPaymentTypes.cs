using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech_Trader_Server.Migrations
{
    public partial class UserPaymentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentTypes");

            migrationBuilder.CreateTable(
                name: "PaymentTypeUser",
                columns: table => new
                {
                    PaymentTypesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeUser", x => new { x.PaymentTypesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PaymentTypeUser_PaymentTypes_PaymentTypesId",
                        column: x => x.PaymentTypesId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTypeUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_SellerId",
                table: "Listings",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypeUser_UsersId",
                table: "PaymentTypeUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_SellerId",
                table: "Listings",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_SellerId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "PaymentTypeUser");

            migrationBuilder.DropIndex(
                name: "IX_Listings_SellerId",
                table: "Listings");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PaymentTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
