using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SeedWithCorrectRoleIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21c5a76d-69e4-4858-8b21-7bb55dae3814", null, "Customer", "CUSTOMER" },
                    { "486e87cc-6243-45b3-9977-63fbabf8edb8", null, "FinancialAdvisor", "FINANCIALADVISOR" },
                    { "c4717e83-2d66-49b7-b8cd-459ffa300769", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04be54be-a2fd-41af-8ab7-85976fe92a2a", 0, "fa4bb30c-cd21-45da-a947-b44b8b5ffa81", "admin@flowspire.com", true, "Admin User", false, null, "ADMIN@FLOWSPIRE.COM", "ADMIN@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEHW5ZbwiVf3BVg5/cep6Nzqh4Hwa1XsC26Xxzf1fcW4K6k6nRtlH3Bo4VrPLyjRw6w==", null, false, "78756207-d4ee-4353-a3f1-85fbbe60e454", false, "admin@flowspire.com" },
                    { "76081757-646f-4627-8233-91372046418b", 0, "eb2510ca-30e1-435b-8eac-cecf79e9f15a", "customer1@flowspire.com", true, "Customer One", false, null, "CUSTOMER1@FLOWSPIRE.COM", "CUSTOMER1@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAENB5rZTKXJHimcdAtb8tbtq3j+WXctQAj3k44NduTh6H/oaxiNQLm5PytQE5kgd0iQ==", null, false, "fdc4b71c-ba5f-4d75-b005-1f8621ec2f52", false, "customer1@flowspire.com" },
                    { "8889e42f-da86-43f2-8352-bd2872ae8ef3", 0, "35b25954-cc51-459f-98eb-a66eddf006c8", "advisor@flowspire.com", true, "Financial Advisor", false, null, "ADVISOR@FLOWSPIRE.COM", "ADVISOR@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEB6dOCsVvoUJ1CeTJB3cYDfMb5iXfRYeFGsX97lxT/ElG45QCQGuA/pc6p0NXRLJ/w==", null, false, "33a76f8c-500d-4d74-b29f-2f8b448b63bf", false, "advisor@flowspire.com" },
                    { "c5ea5694-b0fe-4524-92ab-2a66020eeb36", 0, "baceea9d-57ce-4686-b01d-ac7171949d53", "customer2@flowspire.com", true, "Customer Two", false, null, "CUSTOMER2@FLOWSPIRE.COM", "CUSTOMER2@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEDO+QJKdGmYy0JQY61Kv49Xmbd0MAjaKyO5w0jGsymCat1IORiJ5Y8CviR7JUmtkfA==", null, false, "773c93e1-97ac-4984-bd17-d32f9813faf3", false, "customer2@flowspire.com" }
                });

            migrationBuilder.InsertData(
                table: "AdvisorCustomers",
                columns: new[] { "AdvisorId", "CustomerId", "AssignedAt" },
                values: new object[,]
                {
                    { "8889e42f-da86-43f2-8352-bd2872ae8ef3", "76081757-646f-4627-8233-91372046418b", new DateTime(2025, 2, 23, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3610) },
                    { "8889e42f-da86-43f2-8352-bd2872ae8ef3", "c5ea5694-b0fe-4524-92ab-2a66020eeb36", new DateTime(2025, 2, 28, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3612) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c4717e83-2d66-49b7-b8cd-459ffa300769", "04be54be-a2fd-41af-8ab7-85976fe92a2a" },
                    { "21c5a76d-69e4-4858-8b21-7bb55dae3814", "76081757-646f-4627-8233-91372046418b" },
                    { "486e87cc-6243-45b3-9977-63fbabf8edb8", "8889e42f-da86-43f2-8352-bd2872ae8ef3" },
                    { "21c5a76d-69e4-4858-8b21-7bb55dae3814", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Alimentação", "76081757-646f-4627-8233-91372046418b" },
                    { 2, "Transporte", "76081757-646f-4627-8233-91372046418b" },
                    { 3, "Lazer", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" },
                    { 4, "Educação", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "IsRead", "ReceiverId", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { 1, "Olá, preciso de ajuda com meu orçamento!", false, "8889e42f-da86-43f2-8352-bd2872ae8ef3", "76081757-646f-4627-8233-91372046418b", new DateTime(2025, 3, 5, 18, 38, 51, 188, DateTimeKind.Utc).AddTicks(3548) },
                    { 2, "Claro, vamos analisar suas despesas.", true, "76081757-646f-4627-8233-91372046418b", "8889e42f-da86-43f2-8352-bd2872ae8ef3", new DateTime(2025, 3, 5, 19, 38, 51, 188, DateTimeKind.Utc).AddTicks(3555) },
                    { 3, "Qual é o melhor investimento agora?", false, "8889e42f-da86-43f2-8352-bd2872ae8ef3", "c5ea5694-b0fe-4524-92ab-2a66020eeb36", new DateTime(2025, 3, 5, 17, 38, 51, 188, DateTimeKind.Utc).AddTicks(3556) }
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "Amount", "CategoryId", "EndDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, 200.00m, 1, new DateTime(2025, 4, 4, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3379), new DateTime(2025, 2, 3, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3378), "76081757-646f-4627-8233-91372046418b" },
                    { 2, 100.00m, 3, new DateTime(2025, 4, 4, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3385), new DateTime(2025, 2, 3, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3384), "c5ea5694-b0fe-4524-92ab-2a66020eeb36" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, -50.00m, 1, new DateTime(2025, 2, 28, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3272), "Supermercado", "76081757-646f-4627-8233-91372046418b" },
                    { 2, -20.00m, 2, new DateTime(2025, 3, 2, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3290), "Uber", "76081757-646f-4627-8233-91372046418b" },
                    { 3, -30.00m, 3, new DateTime(2025, 3, 3, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3310), "Cinema", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" },
                    { 4, 1000.00m, 4, new DateTime(2025, 3, 4, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3311), "Salário", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "8889e42f-da86-43f2-8352-bd2872ae8ef3", "76081757-646f-4627-8233-91372046418b" });

            migrationBuilder.DeleteData(
                table: "AdvisorCustomers",
                keyColumns: new[] { "AdvisorId", "CustomerId" },
                keyValues: new object[] { "8889e42f-da86-43f2-8352-bd2872ae8ef3", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c4717e83-2d66-49b7-b8cd-459ffa300769", "04be54be-a2fd-41af-8ab7-85976fe92a2a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "21c5a76d-69e4-4858-8b21-7bb55dae3814", "76081757-646f-4627-8233-91372046418b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "486e87cc-6243-45b3-9977-63fbabf8edb8", "8889e42f-da86-43f2-8352-bd2872ae8ef3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "21c5a76d-69e4-4858-8b21-7bb55dae3814", "c5ea5694-b0fe-4524-92ab-2a66020eeb36" });

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c5a76d-69e4-4858-8b21-7bb55dae3814");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "486e87cc-6243-45b3-9977-63fbabf8edb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4717e83-2d66-49b7-b8cd-459ffa300769");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04be54be-a2fd-41af-8ab7-85976fe92a2a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8889e42f-da86-43f2-8352-bd2872ae8ef3");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76081757-646f-4627-8233-91372046418b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5ea5694-b0fe-4524-92ab-2a66020eeb36");
        }
    }
}
