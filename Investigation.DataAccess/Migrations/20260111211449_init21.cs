using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TargetCountries",
                table: "AdTargets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IncludeBlogInteractions",
                table: "AdTargets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeCompanyInteractions",
                table: "AdTargets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeInvestorInteractions",
                table: "AdTargets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludePostInteractions",
                table: "AdTargets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MinTotalLikeCount",
                table: "AdTargets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinTotalSaveCount",
                table: "AdTargets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinTotalViewCount",
                table: "AdTargets",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeBlogInteractions",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "IncludeCompanyInteractions",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "IncludeInvestorInteractions",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "IncludePostInteractions",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "MinTotalLikeCount",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "MinTotalSaveCount",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "MinTotalViewCount",
                table: "AdTargets");

            migrationBuilder.AlterColumn<string>(
                name: "TargetCountries",
                table: "AdTargets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
