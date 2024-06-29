using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class updateBasketAndBasketDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1720a640-bf37-43bb-af33-c218453c04d0");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1eb87887-d234-47d9-a89c-d722ed879397");

            migrationBuilder.AddColumn<string>(
                name: "ClientSecrit",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntendId",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "589cfb14-93f9-4e9e-b25d-732f111d26cf", "d43dc406-8d59-432f-b99f-dcf88916cb11", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fab3fb10-03d8-47aa-9628-99bed8c22499", "d4e06c11-c5b1-4e31-9db4-4ef9ed8389c7", "Instructor", "INSTRUCTOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "589cfb14-93f9-4e9e-b25d-732f111d26cf");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "fab3fb10-03d8-47aa-9628-99bed8c22499");

            migrationBuilder.DropColumn(
                name: "ClientSecrit",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "PaymentIntendId",
                table: "Baskets");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1720a640-bf37-43bb-af33-c218453c04d0", "eeb8f2d6-32ea-4215-a1d8-af396f7e17c2", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1eb87887-d234-47d9-a89c-d722ed879397", "91c5e603-b60c-4551-afaf-1030c2d63363", "Instructor", "INSTRUCTOR" });
        }
    }
}
