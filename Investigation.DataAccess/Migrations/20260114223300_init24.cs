using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotVisibleForCompanies",
                table: "VisibilitySettings");

            migrationBuilder.DropColumn(
                name: "IsNotVisibleForInvestors",
                table: "VisibilitySettings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotVisibleForCompanies",
                table: "VisibilitySettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNotVisibleForInvestors",
                table: "VisibilitySettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
