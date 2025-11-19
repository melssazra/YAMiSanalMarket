using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YAMi_API.Migrations
{
    /// <inheritdoc />
    public partial class YAMi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kozmetik",
                columns: table => new
                {
                    KId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KSatisF = table.Column<int>(type: "int", nullable: false),
                    KAlis = table.Column<int>(type: "int", nullable: false),
                    KMiktari = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kozmetik", x => x.KId);
                });

            migrationBuilder.CreateTable(
                name: "MeyveVeSebze",
                columns: table => new
                {
                    MId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MSatisF = table.Column<int>(type: "int", nullable: false),
                    MAlis = table.Column<int>(type: "int", nullable: false),
                    MMiktari = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeyveVeSebze", x => x.MId);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    MusteriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MAdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MKullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MSifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MTelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAdres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.MusteriId);
                });

            migrationBuilder.CreateTable(
                name: "Pastane",
                columns: table => new
                {
                    PId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PSatisF = table.Column<int>(type: "int", nullable: false),
                    PAlis = table.Column<int>(type: "int", nullable: false),
                    PMiktari = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pastane", x => x.PId);
                });

            migrationBuilder.CreateTable(
                name: "Personel",
                columns: table => new
                {
                    PersonelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PAdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PKullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personel", x => x.PersonelId);
                });

            migrationBuilder.CreateTable(
                name: "YiyecekVeIcecek",
                columns: table => new
                {
                    YId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YSatisF = table.Column<int>(type: "int", nullable: false),
                    YAlis = table.Column<int>(type: "int", nullable: false),
                    YMiktari = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YiyecekVeIcecek", x => x.YId);
                });

            migrationBuilder.CreateTable(
                name: "Sepet",
                columns: table => new
                {
                    SepetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepet", x => x.SepetId);
                    table.ForeignKey(
                        name: "FK_Sepet_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_MusteriId",
                table: "Sepet",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kozmetik");

            migrationBuilder.DropTable(
                name: "MeyveVeSebze");

            migrationBuilder.DropTable(
                name: "Pastane");

            migrationBuilder.DropTable(
                name: "Personel");

            migrationBuilder.DropTable(
                name: "Sepet");

            migrationBuilder.DropTable(
                name: "YiyecekVeIcecek");

            migrationBuilder.DropTable(
                name: "Musteri");
        }
    }
}
