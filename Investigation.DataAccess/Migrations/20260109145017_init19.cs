using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BannerImage_Title",
                table: "BannerImages");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BannerImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "BannerImages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BannerImage_ControllerName",
                table: "BannerImages",
                column: "ControllerName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BannerImage_ControllerName",
                table: "BannerImages");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "BannerImages");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BannerImages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannerImage_Title",
                table: "BannerImages",
                column: "Title",
                unique: true);
        }
    }
}
