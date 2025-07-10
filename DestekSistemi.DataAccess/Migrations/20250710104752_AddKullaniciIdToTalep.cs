using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DestekSistemi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddKullaniciIdToTalep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Talepler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Talepler");
        }
    }
}
