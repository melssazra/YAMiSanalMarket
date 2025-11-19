using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YAMi_API.Migrations
{
    /// <inheritdoc />
    public partial class Düzenleme3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "Pastane",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EkleyenPersonel",
                table: "Pastane",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellemeTarihi",
                table: "Pastane",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuncelleyenPersonel",
                table: "Pastane",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "Pastane");

            migrationBuilder.DropColumn(
                name: "EkleyenPersonel",
                table: "Pastane");

            migrationBuilder.DropColumn(
                name: "GuncellemeTarihi",
                table: "Pastane");

            migrationBuilder.DropColumn(
                name: "GuncelleyenPersonel",
                table: "Pastane");
        }
    }
}
