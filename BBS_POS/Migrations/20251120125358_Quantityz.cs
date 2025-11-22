using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS_POS.Migrations
{
    /// <inheritdoc />
    public partial class Quantityz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_tbl_product_p_id",
                table: "Quantity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quantity",
                table: "Quantity");

            migrationBuilder.RenameTable(
                name: "Quantity",
                newName: "tbl_quantity");

            migrationBuilder.RenameIndex(
                name: "IX_Quantity_p_id",
                table: "tbl_quantity",
                newName: "IX_tbl_quantity_p_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_quantity",
                table: "tbl_quantity",
                column: "q_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_quantity_tbl_product_p_id",
                table: "tbl_quantity",
                column: "p_id",
                principalTable: "tbl_product",
                principalColumn: "p_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_quantity_tbl_product_p_id",
                table: "tbl_quantity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_quantity",
                table: "tbl_quantity");

            migrationBuilder.RenameTable(
                name: "tbl_quantity",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_quantity_p_id",
                table: "Quantity",
                newName: "IX_Quantity_p_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quantity",
                table: "Quantity",
                column: "q_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_tbl_product_p_id",
                table: "Quantity",
                column: "p_id",
                principalTable: "tbl_product",
                principalColumn: "p_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
