using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class updateSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_SectionId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SectionId",
                table: "Sections");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "50960ae3-6f7d-4f1e-9ec5-f4283ac5bc0b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "eb0c1970-501e-417b-a394-605afb1e9ab9");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Sections");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cea57650-6539-41d1-a9db-ebed7ace54d7", "dd597a92-78ec-4e5b-a7a9-2e43d1981e88", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dde6cf4b-dac6-446c-a30c-71ad3686fce6", "acb1fd22-dc61-4b96-bd4d-e2b2fca66025", "Instructor", "INSTRUCTOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cea57650-6539-41d1-a9db-ebed7ace54d7");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dde6cf4b-dac6-446c-a30c-71ad3686fce6");

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50960ae3-6f7d-4f1e-9ec5-f4283ac5bc0b", "de304f47-b274-4c70-b6a8-c683ddee683b", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb0c1970-501e-417b-a394-605afb1e9ab9", "27c849c2-19e1-4a49-a309-a37a0e0a4a3a", "Instructor", "INSTRUCTOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SectionId",
                table: "Sections",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_SectionId",
                table: "Sections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }
    }
}
