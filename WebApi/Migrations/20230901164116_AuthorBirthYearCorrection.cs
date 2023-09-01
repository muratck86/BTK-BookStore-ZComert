using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AuthorBirthYearCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "225c3a30-aed1-4e3c-a4cd-439b7539157e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4814681c-16e2-4420-bfa2-d5bc2e4ffc8d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1e27876-776c-41cf-9875-b4fb7f96290f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03782bc7-27d4-4be2-9340-10dd14783724", "1789e4b0-6150-4831-82f8-730f22885e45", "User", "USER" },
                    { "b7b1651a-5d48-482d-9903-f09d7d2beec4", "72fcba68-3cda-475a-8f80-759c747133c1", "Admin", "ADMIN" },
                    { "c547002e-89bd-4dbf-92d5-e25994b3e0a9", "ab575c6c-902f-46ee-a611-f035cde4770c", "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(1947, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03782bc7-27d4-4be2-9340-10dd14783724");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7b1651a-5d48-482d-9903-f09d7d2beec4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c547002e-89bd-4dbf-92d5-e25994b3e0a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "225c3a30-aed1-4e3c-a4cd-439b7539157e", "40dda6f5-49d3-485f-b803-7ad8ae6c50ad", "Admin", "ADMIN" },
                    { "4814681c-16e2-4420-bfa2-d5bc2e4ffc8d", "70a9a6bc-f3c4-4dbd-bf00-d4552b1dc88b", "User", "USER" },
                    { "b1e27876-776c-41cf-9875-b4fb7f96290f", "e235dfe5-df50-4234-909a-3486dddf149e", "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(197, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
