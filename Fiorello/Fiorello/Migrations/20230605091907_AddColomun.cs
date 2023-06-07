using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello.Migrations
{
    public partial class AddColomun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 5, 13, 19, 7, 250, DateTimeKind.Local).AddTicks(1571));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 5, 13, 19, 7, 250, DateTimeKind.Local).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 5, 13, 19, 7, 250, DateTimeKind.Local).AddTicks(1602));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sliders");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 22, 22, 31, 33, 79, DateTimeKind.Local).AddTicks(73));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 22, 22, 31, 33, 79, DateTimeKind.Local).AddTicks(135));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 22, 22, 31, 33, 79, DateTimeKind.Local).AddTicks(157));
        }
    }
}
