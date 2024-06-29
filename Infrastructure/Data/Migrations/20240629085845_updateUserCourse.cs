using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class updateUserCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CurrentLecture",
                table: "UserCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41e82eaa-e485-4edf-ace2-390d9ec44e10", "18615c1d-d7f0-4540-bbd9-87e2c7af5976", "Instructor", "INSTRUCTOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43e0570f-6669-4f62-a8ca-eb6926092a5e", "999e7d4b-d66a-42ba-9a1e-93305ae97628", "Student", "STUDENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "41e82eaa-e485-4edf-ace2-390d9ec44e10");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "43e0570f-6669-4f62-a8ca-eb6926092a5e");

            migrationBuilder.DropColumn(
                name: "CurrentLecture",
                table: "UserCourses");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cea57650-6539-41d1-a9db-ebed7ace54d7", "dd597a92-78ec-4e5b-a7a9-2e43d1981e88", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dde6cf4b-dac6-446c-a30c-71ad3686fce6", "acb1fd22-dc61-4b96-bd4d-e2b2fca66025", "Instructor", "INSTRUCTOR" });
        }
    }
}
