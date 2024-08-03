using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminPasswordAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "Sellers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "Customers",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Sellers",
                newName: "Pin");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Customers",
                newName: "Pin");
        }
    }
}
