using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
