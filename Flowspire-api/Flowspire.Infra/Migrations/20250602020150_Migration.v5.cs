using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a510fe77-83e3-49cf-ab6d-c861f9f1b585", "AQAAAAIAAYagAAAAEBmZAjikF6P9TBsVtxXLB9IXC3A6VWug48VjzynM6s2+tBdkilCiZ7bZZwyPoIcSGw==", "1bc0378b-20dc-4889-9da6-7d8685c9bc2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b75d4991-611e-4b68-907d-514df875caf4", "AQAAAAIAAYagAAAAEI748Yf8dBYkR1bBIZJSgnlaX0F3n6KJ8z/ZPIypbqZXuN9kB3VGHY8Mkv6SGFTgIg==", "c3a3e539-bbfa-4656-b0d7-5b5ed7a660b3" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7901), new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7901) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7901), new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7901) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(8080));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954), new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954), new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954) });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Date", "NextOccurrence", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954), new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954), new DateTime(2025, 7, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954), new DateTime(2025, 6, 2, 2, 1, 49, 825, DateTimeKind.Utc).AddTicks(7954) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budgets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(2064));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81c2f749-abc7-450c-bd0a-5529c36879b5", "AQAAAAIAAYagAAAAEKigXkpp9Rh/T4RVX7xQ7Xk4mWcaH/3b6nZVUjFcV3Wk5hrWOb+eGCG2golofUkU1Q==", "72dc68b2-d38b-4d3f-8ed9-426569dc78e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ebd5e24-ee1f-428d-806a-70651c256d5f", "AQAAAAIAAYagAAAAEGMGde/8mI/Y3F3CoznwJ7XABAyWS2D0GzhhmlTYclINY/gXu/+FwELggQUy/MjkSg==", "0107079a-2c02-446a-ab6e-aa764afe3500" });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "CategoryId", "EndDate", "StartDate", "UserId", "Amount" },
                values: new object[] { 1, 1, new DateTime(2025, 6, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1698), new DateTime(2025, 4, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1698), "550e8400-e29b-41d4-a716-446655440002", 500.00m });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1577), new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1577) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1577), new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1577) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621), new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621), new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621) });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Date", "NextOccurrence", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621), new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621), new DateTime(2025, 6, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621), new DateTime(2025, 5, 4, 23, 14, 40, 432, DateTimeKind.Utc).AddTicks(1621) });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");
        }
    }
}
