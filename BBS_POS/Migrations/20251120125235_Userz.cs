using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBS_POS.Migrations
{
    /// <inheritdoc />
    public partial class Userz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    u_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    u_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    u_usr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    u_pwd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    u_role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.u_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
