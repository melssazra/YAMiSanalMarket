using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YAMi_API.Migrations
{
    /// <inheritdoc />
    public partial class SepetGuncellemeV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrunAdet",
                table: "Sepet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UrunId",
                table: "Sepet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UrunTuru",
                table: "Sepet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrunAdet",
                table: "Sepet");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Sepet");

            migrationBuilder.DropColumn(
                name: "UrunTuru",
                table: "Sepet");
        }
    }
}
