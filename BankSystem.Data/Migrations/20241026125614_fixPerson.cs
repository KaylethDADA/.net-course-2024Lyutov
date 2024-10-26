using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName_MiddleName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FullName_MiddleName",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "FullName_LastName",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName_FirstName",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "FullName_LastName",
                table: "Clients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName_FirstName",
                table: "Clients",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "FullName_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "FullName_FirstName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Clients",
                newName: "FullName_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Clients",
                newName: "FullName_FirstName");

            migrationBuilder.AddColumn<string>(
                name: "FullName_MiddleName",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName_MiddleName",
                table: "Clients",
                type: "text",
                nullable: true);
        }
    }
}
