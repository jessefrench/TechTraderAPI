using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech_Trader_Server.Migrations
{
    public partial class ListingImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://i.ebayimg.com/images/g/MWcAAOSwwNVjZsIZ/s-l400.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://i.ebayimg.com/images/g/EEIAAOSw4ZhkRJyV/s-l400.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://i.ebayimg.com/images/g/qK8AAOSwdD9mcwe3/s-l400.png");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://i.ebayimg.com/images/g/D0QAAOSwpGBm2-WD/s-l400.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://i.ebayimg.com/images/g/rf0AAOSwKeRlJhsW/s-l400.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/images/macbook.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/images/rtx3080.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://example.com/images/cod-mw2.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://example.com/images/keyboard.jpg");

            migrationBuilder.UpdateData(
                table: "Listings",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://example.com/images/ps5.jpg");
        }
    }
}
