using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YAMi_API.Migrations
{
    /// <inheritdoc />
    public partial class düzeltme4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "YiyecekVeIcecek",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EkleyenPersonel",
                table: "YiyecekVeIcecek",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellemeTarihi",
                table: "YiyecekVeIcecek",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuncelleyenPersonel",
                table: "YiyecekVeIcecek",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "MeyveVeSebze",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EkleyenPersonel",
                table: "MeyveVeSebze",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellemeTarihi",
                table: "MeyveVeSebze",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuncelleyenPersonel",
                table: "MeyveVeSebze",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "Kozmetik",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EkleyenPersonel",
                table: "Kozmetik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellemeTarihi",
                table: "Kozmetik",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuncelleyenPersonel",
                table: "Kozmetik",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "YiyecekVeIcecek");

            migrationBuilder.DropColumn(
                name: "EkleyenPersonel",
                table: "YiyecekVeIcecek");

            migrationBuilder.DropColumn(
                name: "GuncellemeTarihi",
                table: "YiyecekVeIcecek");

            migrationBuilder.DropColumn(
                name: "GuncelleyenPersonel",
                table: "YiyecekVeIcecek");

            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "MeyveVeSebze");

            migrationBuilder.DropColumn(
                name: "EkleyenPersonel",
                table: "MeyveVeSebze");

            migrationBuilder.DropColumn(
                name: "GuncellemeTarihi",
                table: "MeyveVeSebze");

            migrationBuilder.DropColumn(
                name: "GuncelleyenPersonel",
                table: "MeyveVeSebze");

            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "Kozmetik");

            migrationBuilder.DropColumn(
                name: "EkleyenPersonel",
                table: "Kozmetik");

            migrationBuilder.DropColumn(
                name: "GuncellemeTarihi",
                table: "Kozmetik");

            migrationBuilder.DropColumn(
                name: "GuncelleyenPersonel",
                table: "Kozmetik");
        }
    }
}
