using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace welcome.Migrations
{
    public partial class doubletodecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VatPercentage",
                table: "Departments",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxPrcentage",
                table: "Departments",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VatPercentage",
                table: "Departments",
                nullable: false);

            migrationBuilder.AlterColumn<double>(
                name: "TaxPrcentage",
                table: "Departments",
                nullable: false);
        }
    }
}
