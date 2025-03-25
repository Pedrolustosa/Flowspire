using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadAt",
                table: "Messages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 4, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2545));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2545));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2545));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEH7GAeUrQLGgmhPVP232MpBC0Py5RyfikFS0JhzQoAU4w6ZIWPK8evL1YvyrnrImZg==", "090079ad-b0a0-4cc7-8560-2fc65867b41b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEKGN42al2OAD1wyV1DOhImf1FYtMY5ugJ1BQolI+Zbrp6ZY0lCN8vr2ZiVaJMlKfKg==", "b81bace4-1f48-49bd-b9f3-971bc2d1ef34" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440003",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEBwhW39tJmfssp4Ouhe4MGc56JIU0xoBv2pb7eqNVUs3kJ+6v0EacYZVvptgFkk2CQ==", "bb7e7e16-a090-4c60-8b91-50a6908d7714" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440004",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEOgYDN1F6kF4dIEt7a/fpXQv42tf0+9dvX+bN2JIOLebpZA1yl509yM8ziv/qn1h/g==", "ea9ff4b4-5040-4b91-a563-1a04522cc76e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440005",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEPcEaZsjX4g994lAC0W9lNXBdyv4oJSyEyIpWNJhov76wlvMt2dM/qBmOBhPW1fPMQ==", "3bb4f7ae-6838-4439-9863-29c03c2df0bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440006",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAELj/uFNN53wDdqPeVSnz1iJlbtJsIwcmimvvPHulavPdHQEJO4nYPeHkFA9KX0mdYA==", "334ec97c-1ab0-487c-a6dc-631e90f44c04" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 400.00m, new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 250.00m, new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 200.00m, new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 500.00m, new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Amount", "CategoryId", "EndDate", "StartDate", "UserId" },
                values: new object[] { 300.00m, 7, new DateTime(2025, 5, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 12, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Amount", "CategoryId", "EndDate", "StartDate" },
                values: new object[] { 300.00m, 8, new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612) });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "Amount", "CategoryId", "EndDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 8, 2000.00m, 9, new DateTime(2025, 4, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 9, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), "550e8400-e29b-41d4-a716-446655440006" },
                    { 9, 150.00m, 10, new DateTime(2025, 5, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), new DateTime(2024, 12, 24, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(1612), "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { null, new DateTime(2025, 3, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { null, new DateTime(2025, 3, 15, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { null, new DateTime(2025, 3, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { null, new DateTime(2025, 3, 20, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { null, new DateTime(2025, 3, 21, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { null, new DateTime(2025, 3, 22, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "IsRead", "ReadAt", "ReceiverId", "SenderId", "SentAt" },
                values: new object[] { 7, "Obrigado pela ajuda!", false, null, "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004", new DateTime(2025, 3, 23, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 9, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 9, 23, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2024, 10, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2024, 10, 21, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2024, 10, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Amount", "CategoryId", "Date", "Description" },
                values: new object[] { -100.00m, 3, new DateTime(2024, 11, 4, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Consulta Médica" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2024, 11, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2024, 11, 23, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -90.00m, 1, new DateTime(2024, 12, 6, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Mercado", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -30.00m, 2, new DateTime(2024, 12, 12, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Táxi", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -850.00m, 4, new DateTime(2024, 12, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Aluguel", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { 500.00m, 1, new DateTime(2025, 1, 4, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Salário Extra", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -60.00m, 3, new DateTime(2025, 1, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Remédios", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -110.00m, 1, new DateTime(2025, 1, 30, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Supermercado", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -20.00m, 2, new DateTime(2025, 2, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Ônibus", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { 2300.00m, 1, new DateTime(2025, 2, 23, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Salário", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -870.00m, 4, new DateTime(2025, 3, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Aluguel", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -45.00m, 3, new DateTime(2025, 3, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Farmácia", "550e8400-e29b-41d4-a716-446655440004" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -40.00m, 5, new DateTime(2024, 9, 17, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Cinema", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -150.00m, 6, new DateTime(2024, 9, 21, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Curso Online", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { 1000.00m, 6, new DateTime(2024, 9, 22, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Freelance", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { 23, -90.00m, 7, new DateTime(2024, 10, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Roupas", "550e8400-e29b-41d4-a716-446655440005" },
                    { 24, -35.00m, 5, new DateTime(2024, 10, 22, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Cinema", "550e8400-e29b-41d4-a716-446655440005" },
                    { 25, -70.00m, 6, new DateTime(2024, 11, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Livro", "550e8400-e29b-41d4-a716-446655440005" },
                    { 26, 1100.00m, 6, new DateTime(2024, 11, 23, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Freelance", "550e8400-e29b-41d4-a716-446655440005" },
                    { 27, -60.00m, 5, new DateTime(2024, 12, 16, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Show", "550e8400-e29b-41d4-a716-446655440005" },
                    { 28, -120.00m, 7, new DateTime(2024, 12, 21, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Roupas", "550e8400-e29b-41d4-a716-446655440005" },
                    { 29, -200.00m, 6, new DateTime(2025, 1, 4, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Curso", "550e8400-e29b-41d4-a716-446655440005" },
                    { 30, -45.00m, 5, new DateTime(2025, 1, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Cinema", "550e8400-e29b-41d4-a716-446655440005" },
                    { 31, 1200.00m, 6, new DateTime(2025, 2, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Freelance", "550e8400-e29b-41d4-a716-446655440005" },
                    { 32, -80.00m, 5, new DateTime(2025, 2, 22, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Jantar", "550e8400-e29b-41d4-a716-446655440005" },
                    { 33, -100.00m, 7, new DateTime(2025, 3, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Roupas", "550e8400-e29b-41d4-a716-446655440005" },
                    { 34, -180.00m, 6, new DateTime(2025, 3, 17, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Curso Online", "550e8400-e29b-41d4-a716-446655440005" },
                    { 35, -70.00m, 8, new DateTime(2024, 9, 12, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Restaurante", "550e8400-e29b-41d4-a716-446655440006" },
                    { 36, -1500.00m, 9, new DateTime(2024, 9, 19, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Notebook", "550e8400-e29b-41d4-a716-446655440006" },
                    { 37, -50.00m, 10, new DateTime(2024, 10, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Jogo Online", "550e8400-e29b-41d4-a716-446655440006" },
                    { 38, -100.00m, 8, new DateTime(2024, 10, 21, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Supermercado", "550e8400-e29b-41d4-a716-446655440006" },
                    { 39, -800.00m, 9, new DateTime(2024, 11, 4, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Celular", "550e8400-e29b-41d4-a716-446655440006" },
                    { 40, -30.00m, 10, new DateTime(2024, 11, 17, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Cinema", "550e8400-e29b-41d4-a716-446655440006" },
                    { 41, -90.00m, 8, new DateTime(2024, 12, 9, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Restaurante", "550e8400-e29b-41d4-a716-446655440006" },
                    { 42, -300.00m, 9, new DateTime(2024, 12, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Software", "550e8400-e29b-41d4-a716-446655440006" },
                    { 43, -40.00m, 10, new DateTime(2025, 1, 12, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Jogo", "550e8400-e29b-41d4-a716-446655440006" },
                    { 44, 1800.00m, 8, new DateTime(2025, 1, 23, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Salário", "550e8400-e29b-41d4-a716-446655440006" },
                    { 45, -120.00m, 8, new DateTime(2025, 2, 4, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Supermercado", "550e8400-e29b-41d4-a716-446655440006" },
                    { 46, -150.00m, 9, new DateTime(2025, 2, 14, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Acessório Tech", "550e8400-e29b-41d4-a716-446655440006" },
                    { 47, -35.00m, 10, new DateTime(2025, 3, 12, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Cinema", "550e8400-e29b-41d4-a716-446655440006" },
                    { 48, 1900.00m, 8, new DateTime(2025, 3, 22, 1, 0, 35, 210, DateTimeKind.Utc).AddTicks(979), "Salário", "550e8400-e29b-41d4-a716-446655440006" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DropColumn(
                name: "ReadAt",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004" },
                column: "AssignedAt",
                value: new DateTime(2025, 2, 14, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5134));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005" },
                column: "AssignedAt",
                value: new DateTime(2025, 2, 19, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5134));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006" },
                column: "AssignedAt",
                value: new DateTime(2025, 2, 24, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5134));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAENKIF7gxkoyhwjzx0Y5CRudQxl8D7KtXXupapeUCg6ZUw7aKZeF7JITq52WfIMlctg==", "5eb9a427-a867-4844-b0f2-b318ba4ce273" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEIlUS4bXTv3jODtSWuP3/DYVNC7XN3WuDLhD6ZVcUX2o4niAlGGFwzI/+c5OOyrF4g==", "e6e5a48c-109f-4712-a239-89f941208eb3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440003",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEBHUGfH/RlwvjfgbQLUr7DGFbUhqRqdz4TNBF6Fj7PDUZUHElncUgTfJf52EqzDXLA==", "bff49fb9-01cf-4bf1-91a3-71049170ac83" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440004",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEGTCbw1TrPnY7AsjuOfBZdjsMGKjuCVneqjMayEpC68gzBTnOfau5mNcCpXTGgH/7g==", "15b344a1-47bf-480b-a027-73a4332ac100" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440005",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEEo0gbdxE13Tfcg3lMjAhhW1UuorzUbC4TB+4p8aJ2xB7UZMflmhICiiENV68WmYng==", "3f6675f4-c14c-44ea-a2a6-dd4e73dc310e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440006",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAENA8MXRiJVad44ANtMfglUb+UC27MAd7Evs/NpuopYg6L8aeKup38M7Y8+GkANVBOA==", "1c547459-021f-479e-81d5-58db0006da86" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 300.00m, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 200.00m, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 150.00m, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amount", "EndDate", "StartDate" },
                values: new object[] { 400.00m, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Amount", "CategoryId", "EndDate", "StartDate", "UserId" },
                values: new object[] { 250.00m, 8, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440006" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Amount", "CategoryId", "EndDate", "StartDate" },
                values: new object[] { 100.00m, 10, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 2, 24, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "SentAt",
                value: new DateTime(2025, 2, 25, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "SentAt",
                value: new DateTime(2025, 3, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                column: "SentAt",
                value: new DateTime(2025, 3, 2, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "SentAt",
                value: new DateTime(2025, 3, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "SentAt",
                value: new DateTime(2025, 3, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 12, 22, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 12, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 1, 5, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 2, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2025, 2, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2025, 1, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Amount", "CategoryId", "Date", "Description" },
                values: new object[] { -60.00m, 1, new DateTime(2025, 2, 19, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Supermercado" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date",
                value: new DateTime(2025, 2, 24, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 3, 5, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -40.00m, 5, new DateTime(2024, 12, 30, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Cinema", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -150.00m, 6, new DateTime(2025, 1, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Curso Online", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { 1000.00m, 6, new DateTime(2025, 1, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Freelance", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -90.00m, 7, new DateTime(2025, 2, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Roupas", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -35.00m, 5, new DateTime(2025, 2, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Cinema", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -60.00m, 5, new DateTime(2025, 2, 26, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Show", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { 1200.00m, 6, new DateTime(2025, 3, 5, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Freelance", "550e8400-e29b-41d4-a716-446655440005" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -70.00m, 8, new DateTime(2024, 12, 25, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Restaurante", "550e8400-e29b-41d4-a716-446655440006" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -1500.00m, 9, new DateTime(2025, 1, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Notebook", "550e8400-e29b-41d4-a716-446655440006" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -50.00m, 10, new DateTime(2025, 1, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Jogo Online", "550e8400-e29b-41d4-a716-446655440006" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -100.00m, 8, new DateTime(2025, 2, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Supermercado", "550e8400-e29b-41d4-a716-446655440006" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { -30.00m, 10, new DateTime(2025, 2, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Cinema", "550e8400-e29b-41d4-a716-446655440006" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[] { 1800.00m, 8, new DateTime(2025, 3, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Salário", "550e8400-e29b-41d4-a716-446655440006" });
        }
    }
}
