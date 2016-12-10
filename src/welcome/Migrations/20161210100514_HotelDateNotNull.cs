using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace welcome.Migrations
{
    public partial class HotelDateNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPreAuthorization",
                table: "Deposits",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HotelDate",
                table: "Deposits",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPreAuthorization",
                table: "Deposits",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HotelDate",
                table: "Deposits",
                nullable: true);
        }
    }
}
