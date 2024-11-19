using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tech_Trader_Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Zip = table.Column<string>(type: "text", nullable: false),
                    IsSeller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ConditionId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Sold = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listings_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ListingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedListings_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Desktops" },
                    { 2, "Laptops" },
                    { 3, "Monitors" },
                    { 4, "Gaming Consoles" },
                    { 5, "PC Parts" },
                    { 6, "PC Accessories" },
                    { 7, "Gaming Console Accessories" },
                    { 8, "Video Games" },
                    { 9, "Movies" },
                    { 10, "TV Shows" }
                });

            migrationBuilder.InsertData(
                table: "Conditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Used" },
                    { 3, "Open-Box" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "SellerId", "SentAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Hi, is this item still available?", 1, new DateTime(2024, 11, 18, 10, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, "Yes, it's available. Would you like to arrange a pickup?", 2, new DateTime(2024, 11, 18, 10, 35, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "Can you provide more details about the condition?", 1, new DateTime(2024, 11, 18, 11, 15, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "When are you available to meet?", 3, new DateTime(2024, 11, 18, 12, 45, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, "I'm interested. Is the price negotiable?", 3, new DateTime(2024, 11, 18, 13, 10, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Cash", 0 },
                    { 2, "Venmo", 0 },
                    { 3, "PayPal", 0 },
                    { 4, "Apple Pay", 0 },
                    { 5, "Cash App", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Email", "FirstName", "ImageUrl", "IsSeller", "LastName", "State", "Uid", "Zip" },
                values: new object[,]
                {
                    { 1, "San Francisco", "alice.johnson@example.com", "Alice", "https://example.com/images/alice.jpg", true, "Johnson", "CA", "PzXnYW3LbJkfVT92QLrCoM87F15N4", "94103" },
                    { 2, "Austin", "bob.smith@example.com", "Bob", "https://example.com/images/bob.jpg", true, "Smith", "TX", "ZpkMvJ2YWbxELQ39VTfXrK8C76M4", "73301" },
                    { 3, "Seattle", "cathy.lee@example.com", "Cathy", "https://example.com/images/cathy.jpg", true, "Lee", "WA", "QlmPnKY2WvcrLJ37TZfXoC8F65N9", "98101" }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "CategoryId", "ConditionId", "CreatedOn", "Description", "ImageUrl", "Name", "Price", "SellerId", "Sold" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2024, 11, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), "Brand new MacBook Pro with M1 chip, 16GB RAM, and 1TB SSD.", "https://example.com/images/macbook.jpg", "MacBook Pro 16-inch", 2200.00m, 1, false },
                    { 2, 5, 2, new DateTime(2024, 11, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Lightly used RTX 3080 graphics card, excellent condition.", "https://example.com/images/rtx3080.jpg", "NVIDIA RTX 3080 GPU", 650.00m, 2, false },
                    { 3, 8, 3, new DateTime(2024, 11, 12, 16, 45, 0, 0, DateTimeKind.Unspecified), "Open-box PS5 game, no scratches on the disc.", "https://example.com/images/cod-mw2.jpg", "Call of Duty: Modern Warfare II", 40.00m, 3, false },
                    { 4, 6, 1, new DateTime(2024, 11, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "New mechanical keyboard with RGB lighting.", "https://example.com/images/keyboard.jpg", "Razer BlackWidow Keyboard", 120.00m, 2, true },
                    { 5, 4, 2, new DateTime(2024, 11, 8, 9, 15, 0, 0, DateTimeKind.Unspecified), "Used PS5 in great condition, comes with one controller.", "https://example.com/images/ps5.jpg", "PlayStation 5 Console", 450.00m, 1, false }
                });

            migrationBuilder.InsertData(
                table: "SavedListings",
                columns: new[] { "Id", "ListingId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 3 },
                    { 3, 3, 1 },
                    { 4, 5, 2 },
                    { 5, 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CategoryId",
                table: "Listings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ConditionId",
                table: "Listings",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedListings_ListingId",
                table: "SavedListings",
                column: "ListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "SavedListings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Conditions");
        }
    }
}
