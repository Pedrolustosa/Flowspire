using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas com alimentação e supermercado", true, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas com transporte, combustível e ônibus", true, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas médicas e de saúde", true, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas com moradia, aluguel e utilities", true, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Entretenimento, restaurantes e lazer", false, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cursos, livros e despesas educacionais", false, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Roupas e acessórios", false, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas com alimentação para outros clientes", true, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gastos com gadgets, assinaturas e tecnologia", false, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas com lazer, viagens e entretenimento", false, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Despesas com saúde, academia e consultas", true, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description", "IsDefault", "SortOrder", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Investimentos em educação e cursos", true, 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 23, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9782));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005" },
                column: "AssignedAt",
                value: new DateTime(2025, 3, 28, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9782));

            migrationBuilder.UpdateData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006" },
                column: "AssignedAt",
                value: new DateTime(2025, 4, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9782));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440001",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEOHoVo8odfnYJO74+Zqgn9ehoAI2Qm+jKjz9sSslEPtS9a04CgbaoA6GL8DwhFB46Q==", "ebdf372d-73ae-4beb-8a67-55b49543fd80" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440002",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEODO1bcWNNq2C4wu157+LdOWJ/dAGhNVFB5njW6kMFLChyPtyioont4do/JOyCFwSg==", "ceadc170-ca55-4d1c-834b-a8b18d06e6e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440003",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEGEPSnBJkJw1mcskXXfh7yD+E8oKpU3vPnmtbZSv1OSDQV82+JlsV19Rq2b2XlgH1A==", "0b1d8231-6a8d-47ae-9070-48384bc733b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440004",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEPyVcPUyT6hWluiBpwyG3T/KE+wELnKzwMGJj/OIXKBxK2QTLMYTGtwB0Hwsv1uc9w==", "57648e11-1c17-4095-a5de-e5eaac77ec48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440005",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEIquTehMxQbUhtxxPyROVwNf9M7E7uesUK71xcE/rzNDG8XSQ92BA1jCSVLoejaw1w==", "899e3ce0-8d09-447b-8920-f3d40b5a6353" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "550e8400-e29b-41d4-a716-446655440006",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEFviGToBmTA4Fu56Ci3PZKf8fSTiuaqAa4vudEXsgEqk2LfS1fTOcQdwujZeqNdiyw==", "a66f68e3-ea74-44c8-8686-bc9eb8494fa2" });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 2, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 2, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 3, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 2, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 5, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 1, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 6, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2024, 12, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 7, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 3, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 27, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 9, 27, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 9, 27, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 11, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 11, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 11, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 11, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 5, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 5, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 5, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 9, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 9, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "FinancialTransactions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedAt", "Date", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 4, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 4, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 4, 3, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "SentAt",
                value: new DateTime(2025, 4, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 4, 8, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5,
                column: "SentAt",
                value: new DateTime(2025, 4, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "SentAt",
                value: new DateTime(2025, 4, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "SentAt",
                value: new DateTime(2025, 4, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 3, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 3, 29, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 3, 31, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 3, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "SentAt",
                value: new DateTime(2025, 4, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ReadAt", "SentAt" },
                values: new object[] { new DateTime(2025, 4, 8, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 4, 8, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) });
        }
    }
}
