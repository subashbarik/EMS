using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UTCDateTimeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUTCDate",
                table: "EmployeeTypes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedUTCDate",
                table: "EmployeeTypes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUTCDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedUTCDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUTCDate",
                table: "Designations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedUTCDate",
                table: "Designations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUTCDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedUTCDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUTCDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedUTCDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUTCDate",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedUTCDate",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "CreatedUTCDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedUTCDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedUTCDate",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "UpdatedUTCDate",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "CreatedUTCDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UpdatedUTCDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedUTCDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedUTCDate",
                table: "Companies");
        }
    }
}
