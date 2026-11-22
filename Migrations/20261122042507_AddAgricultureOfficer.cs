using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAgricultureOfficer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AgricultureOfficers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AgricultureOfficers",
                newName: "OfficerId");

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "AgricultureOfficers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "AgricultureOfficers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AgricultureOfficers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                table: "AgricultureOfficers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AgricultureOfficers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Office",
                table: "AgricultureOfficers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "AgricultureOfficers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AgricultureOfficers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "AgricultureOfficers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AgricultureOfficers");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "AgricultureOfficers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AgricultureOfficers");

            migrationBuilder.DropColumn(
                name: "Office",
                table: "AgricultureOfficers");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "AgricultureOfficers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AgricultureOfficers");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AgricultureOfficers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "OfficerId",
                table: "AgricultureOfficers",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "AgricultureOfficers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
