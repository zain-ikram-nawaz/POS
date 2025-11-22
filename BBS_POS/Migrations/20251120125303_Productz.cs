using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS_POS.Migrations
{
    /// <inheritdoc />
    public partial class Productz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    p_sale_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_buy_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_box_size = table.Column<int>(type: "int", nullable: false),
                    p_piece_size = table.Column<int>(type: "int", nullable: false),
                    p_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    p_updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.p_id);
                });

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    q_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_id = table.Column<int>(type: "int", nullable: false),
                    box_quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantity", x => x.q_id);
                    table.ForeignKey(
                        name: "FK_Quantity_tbl_product_p_id",
                        column: x => x.p_id,
                        principalTable: "tbl_product",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    s_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    qty_box = table.Column<int>(type: "int", nullable: true),
                    qty_pieces = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    s_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    p_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.s_id);
                    table.ForeignKey(
                        name: "FK_Sale_tbl_product_p_id",
                        column: x => x.p_id,
                        principalTable: "tbl_product",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_p_id",
                table: "Quantity",
                column: "p_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_p_id",
                table: "Sale",
                column: "p_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "tbl_product");
        }
    }
}
