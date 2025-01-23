using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeCleanArch.Peristence.Migrations
{
    /// <inheritdoc />
    public partial class addEnumGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Employees SET Gender = 1 WHERE Gender = 'Male'");
            migrationBuilder.Sql("UPDATE Employees SET Gender = 2 WHERE Gender = 'Female'");
            migrationBuilder.Sql("UPDATE Employees SET Gender = 3 WHERE Gender = 'Other'");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
