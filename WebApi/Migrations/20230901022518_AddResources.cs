using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f9eb5cf-3162-4f73-83c8-a5dcc5eda7ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21412b2e-5cb1-42fe-8f8d-2bde0517943e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "353dd17c-d808-490b-9e48-03aacd874cb7");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Edition",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "356b1f40-03bb-4003-abca-561cc945647c", "c1c1d5f0-ce8a-4c37-b08a-018e44b052fd", "User", "USER" },
                    { "3674f692-a9d7-4b84-93d7-a05ce978fd22", "e5f27316-3819-4a6a-9c70-8e7079035fd5", "Editor", "EDITOR" },
                    { "91c4187e-54c1-498c-bd0a-29192a5aef14", "55a31aa1-e45e-4f7d-94b6-ce4425e2238d", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "Country", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(197, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brazil", "Paulo", "Coelho" },
                    { 2, new DateTime(1949, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "France", "Amin", "Maalouf" },
                    { 3, new DateTime(1923, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turkey", "Yaşar", "Kemal" },
                    { 4, new DateTime(1960, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turkey", "Ahmet", "Ümit" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Edition", "PageCount", "Price", "ReleaseYear", "Title" },
                values: new object[] { 1, 355, 105m, 1945, "The Legend of Ararat" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Edition", "PageCount", "Price", "ReleaseYear", "Title" },
                values: new object[] { 3, 425, 145m, 2003, "Patasana" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Edition", "PageCount", "ReleaseYear", "Title" },
                values: new object[] { 5, 251, 1982, "The Alchemist" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Edition", "PageCount", "Price", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 4, 3, 388, 125m, 1992, "Veronica Decides to Die" },
                    { 5, 18, 295, 110m, 1996, "Samarkand" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mystery" },
                    { 2, "Romance" },
                    { 3, "Fiction" },
                    { 4, "Horror" },
                    { 5, "Memoir" },
                    { 6, "Biography" },
                    { 7, "Fantasy" },
                    { 8, "Poetry" },
                    { 9, "Thriller" },
                    { 10, "Humor" },
                    { 11, "Science-Fiction" },
                    { 12, "Self-Help" },
                    { 13, "Short Stories" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "356b1f40-03bb-4003-abca-561cc945647c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3674f692-a9d7-4b84-93d7-a05ce978fd22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91c4187e-54c1-498c-bd0a-29192a5aef14");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Books");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f9eb5cf-3162-4f73-83c8-a5dcc5eda7ac", "f0ba52ae-336c-4256-be4a-7f0de1bfa2f2", "Editor", "EDITOR" },
                    { "21412b2e-5cb1-42fe-8f8d-2bde0517943e", "8e6b93d8-04d7-4d8e-958a-01f20fcece4e", "Admin", "ADMIN" },
                    { "353dd17c-d808-490b-9e48-03aacd874cb7", "68916206-bcdc-443c-a40e-e82847595016", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "Title" },
                values: new object[] { 75m, "Karagöz ve Hacivat" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "Title" },
                values: new object[] { 105m, "Semerkant" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Simyacı");
        }
    }
}
