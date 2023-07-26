using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRandevuSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HastaneTable",
                columns: table => new
                {
                    HastaneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastaneAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HastaneTable", x => x.HastaneID);
                });

            migrationBuilder.CreateTable(
                name: "PoliklinikTable",
                columns: table => new
                {
                    PoliklinikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliklinikAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaneID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliklinikTable", x => x.PoliklinikID);
                    table.ForeignKey(
                        name: "FK_PoliklinikTable_HastaneTable_HastaneID",
                        column: x => x.HastaneID,
                        principalTable: "HastaneTable",
                        principalColumn: "HastaneID");
                });

            migrationBuilder.CreateTable(
                name: "DoktorTable",
                columns: table => new
                {
                    DoktorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoktorAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoktorSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliklinikID = table.Column<int>(type: "int", nullable: true),
                    HastaneID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoktorTable", x => x.DoktorID);
                    table.ForeignKey(
                        name: "FK_DoktorTable_HastaneTable_HastaneID",
                        column: x => x.HastaneID,
                        principalTable: "HastaneTable",
                        principalColumn: "HastaneID");
                    table.ForeignKey(
                        name: "FK_DoktorTable_PoliklinikTable_PoliklinikID",
                        column: x => x.PoliklinikID,
                        principalTable: "PoliklinikTable",
                        principalColumn: "PoliklinikID");
                });

            migrationBuilder.CreateTable(
                name: "RandevuTable",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuGun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandevuSaat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoktorID = table.Column<int>(type: "int", nullable: true),
                    PoliklinikID = table.Column<int>(type: "int", nullable: true),
                    HastaneID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuTable", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_RandevuTable_DoktorTable_DoktorID",
                        column: x => x.DoktorID,
                        principalTable: "DoktorTable",
                        principalColumn: "DoktorID");
                    table.ForeignKey(
                        name: "FK_RandevuTable_HastaneTable_HastaneID",
                        column: x => x.HastaneID,
                        principalTable: "HastaneTable",
                        principalColumn: "HastaneID");
                    table.ForeignKey(
                        name: "FK_RandevuTable_PoliklinikTable_PoliklinikID",
                        column: x => x.PoliklinikID,
                        principalTable: "PoliklinikTable",
                        principalColumn: "PoliklinikID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoktorTable_HastaneID",
                table: "DoktorTable",
                column: "HastaneID");

            migrationBuilder.CreateIndex(
                name: "IX_DoktorTable_PoliklinikID",
                table: "DoktorTable",
                column: "PoliklinikID");

            migrationBuilder.CreateIndex(
                name: "IX_PoliklinikTable_HastaneID",
                table: "PoliklinikTable",
                column: "HastaneID");

            migrationBuilder.CreateIndex(
                name: "IX_RandevuTable_DoktorID",
                table: "RandevuTable",
                column: "DoktorID");

            migrationBuilder.CreateIndex(
                name: "IX_RandevuTable_HastaneID",
                table: "RandevuTable",
                column: "HastaneID");

            migrationBuilder.CreateIndex(
                name: "IX_RandevuTable_PoliklinikID",
                table: "RandevuTable",
                column: "PoliklinikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RandevuTable");

            migrationBuilder.DropTable(
                name: "DoktorTable");

            migrationBuilder.DropTable(
                name: "PoliklinikTable");

            migrationBuilder.DropTable(
                name: "HastaneTable");
        }
    }
}
