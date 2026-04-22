using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncryptedBirthdate",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedBirthdate",
                table: "AspNetUsers");
        }
    }
}
