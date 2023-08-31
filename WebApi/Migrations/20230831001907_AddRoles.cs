using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b3c8f98-ef35-452c-9496-244a797de6e4", "3adae5f4-6b56-421f-b522-cf378f5218b7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d788e5eb-d9da-4aa2-9411-4bb78c4d9e8b", "bf827be5-5e80-477e-acfb-b3204c121e4a", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ffa1ce65-7bc8-4afd-8c7d-5fb5d3e59fdb", "0e99da07-836c-40f8-aec1-3eb180ce4b7a", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b3c8f98-ef35-452c-9496-244a797de6e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d788e5eb-d9da-4aa2-9411-4bb78c4d9e8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffa1ce65-7bc8-4afd-8c7d-5fb5d3e59fdb");
        }
    }
}
