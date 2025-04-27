using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Method = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ExecutionTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004" },
                column: "AssignedAt",
                value: new DateTime(2025, 4, 6, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005" },
                column: "AssignedAt",
                value: new DateTime(2025, 4, 11, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006" },
                column: "AssignedAt",
                value: new DateTime(2025, 4, 16, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEBAJJoYCk8LPD4t7ZLbBYIjz23IMMpT/6DBVM13VglhPsAnm/dUp23Kx5JFp3YhzVg==", "cf24b196-3410-4415-b675-cea21b3d1bbd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEPo9Zpdq3p51+D46BPz1Pq6hfIcj7nnW6yLGEOTt4UbBRyZkOsIPzDKFE3kzr+FHoQ==", "cbac6059-4884-4885-b84a-16121d28c2a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440003",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEDzVvy2P+uIN7mSOTeWrGtQ/2xfJNd3lt8eDfTwnX7qiuu1xcT5pydJap+kyA8Oq4w==", "6ca0607c-fd02-4bec-90e7-2f4144f75ed6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440004",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEFtWaucrIDmrzL0TrgCbN8iokIRKnMyyxX2wd5xShDA89evcs+wOicLQmENDghKbnw==", "91afa921-f5e8-4f47-b5e8-7e12fb406119" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440005",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEGlutIPWpgfduJ5Y6BmqChmoEo4z7Puyp8uFN3RoFkuaqhx1p/XxIUSCl+RLe4h/og==", "3c5e77ff-f079-4eea-a707-ed849961cbd0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440006",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEE6KYS4B+mV9kccIeu/dIeswpr82MtSnESfI1+lQKG5CiozI6XvYQcM5zEs3QH+0TQ==", "601fc86e-0c2c-4e3e-bd92-fa3103f1e9d1" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2025, 2, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2025, 2, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2025, 3, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2025, 2, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2025, 1, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2024, 12, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839), new DateTime(2025, 3, 26, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 11, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 11, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 11, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 16, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 16, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 25, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 25, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 11, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 11, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 11, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 11, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 19, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 19, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 19, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 24, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 24, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 24, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 14, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 14, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 14, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 10, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 4, 16, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 18, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909), new DateTime(2025, 4, 17, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "SentAt",
                value: new DateTime(2025, 4, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909), new DateTime(2025, 4, 22, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "SentAt",
                value: new DateTime(2025, 4, 23, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "SentAt",
                value: new DateTime(2025, 4, 24, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "SentAt",
                value: new DateTime(2025, 4, 25, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 13, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909), new DateTime(2025, 4, 12, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 14, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909), new DateTime(2025, 4, 13, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "SentAt",
                value: new DateTime(2025, 4, 21, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 22, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909), new DateTime(2025, 4, 22, 17, 28, 57, 865, DateTimeKind.Utc).AddTicks(7909) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 25, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 30, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006" },
                column: "AssignedAt",
                value: new DateTime(2025, 4, 4, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAELfDe7m4032QN4KcIxo0Y5lkuhCGXEaM2teqNMqECQXqV5f7LWsc5/O/AeqUVJekBw==", "4d47bace-d320-471c-bd92-212f183d0dfb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEFTIcRtjQzkyyDHbb6at/Yq3BayvqWB9awd3fqYGTbfGywShHXyClCD7LlcNuTRb+A==", "c1aead48-bd61-4c86-860b-76f090034145" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440003",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAECE99+VKAO4MauK2Ksef6K0DB2ZTh0lYzxK2Sh9P4PRssWnHqi5fuU7otQvqDkxjaQ==", "829ef1da-c917-4fe1-8eaa-1b479b360641" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440004",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEFiuO0zjuTU5oyfUCyVY2KNS2ZZXQaHzMdnW+SfKJ/eQSavuKpttHURtrNTDfg6fSA==", "ef12e55f-019a-44de-aeab-6dfdbc1d595d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440005",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAECq3UEY4wQvO9wqIPKCRKZ5Ksk28qmw3Gr9ipAaeBDyH5aiAF6Ybvm5jRzfhnaCsaw==", "bf8cda80-c6bd-4dde-880e-7a7f7bb6cd1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440006",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAELIJnzo0dXS8hz7iDSN8myjgYkKpHaFiZXzj0iydDPLsbnUwMgy5oixH9wJv2ZMTZw==", "61bfa2b5-cac0-4979-802d-5841adcf8c15" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2025, 2, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2025, 2, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2025, 3, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2025, 2, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2025, 1, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2024, 12, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677), new DateTime(2025, 3, 14, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 29, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 9, 29, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 9, 29, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 4, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 4, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 4, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 13, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 13, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 11, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 11, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 11, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 11, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 7, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 7, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 7, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 12, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 12, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 12, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 2, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 2, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 2, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 10, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 4, 4, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 6, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755), new DateTime(2025, 4, 5, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "SentAt",
                value: new DateTime(2025, 4, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755), new DateTime(2025, 4, 10, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "SentAt",
                value: new DateTime(2025, 4, 11, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "SentAt",
                value: new DateTime(2025, 4, 12, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "SentAt",
                value: new DateTime(2025, 4, 13, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 1, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755), new DateTime(2025, 3, 31, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 2, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755), new DateTime(2025, 4, 1, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "SentAt",
                value: new DateTime(2025, 4, 9, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 10, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755), new DateTime(2025, 4, 10, 4, 15, 24, 106, DateTimeKind.Utc).AddTicks(3755) });
        }
    }
}
