using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmirhanAvci.WebApi.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    NetworkId = table.Column<int>(type: "int", nullable: false),
                    CoinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoinPriceAvg = table.Column<double>(type: "float", nullable: false),
                    CoinMaxSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CoinTotalSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoinCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoinListDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisibilityStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetworkName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    NetworkId = table.Column<int>(type: "int", nullable: false),
                    TokenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenPriceAvg = table.Column<double>(type: "float", nullable: false),
                    TokenMaxSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TokenTotalSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TokenCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TokenListDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisibilityStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
