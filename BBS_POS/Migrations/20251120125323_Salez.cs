using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS_POS.Migrations
{
    /// <inheritdoc />
    public partial class Salez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_tbl_product_p_id",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "tbl_sale");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_p_id",
                table: "tbl_sale",
                newName: "IX_tbl_sale_p_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_sale",
                table: "tbl_sale",
                column: "s_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_sale_tbl_product_p_id",
                table: "tbl_sale",
                column: "p_id",
                principalTable: "tbl_product",
                principalColumn: "p_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_sale_tbl_product_p_id",
                table: "tbl_sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_sale",
                table: "tbl_sale");

            migrationBuilder.RenameTable(
                name: "tbl_sale",
                newName: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_sale_p_id",
                table: "Sale",
                newName: "IX_Sale_p_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "s_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_tbl_product_p_id",
                table: "Sale",
                column: "p_id",
                principalTable: "tbl_product",
                principalColumn: "p_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
