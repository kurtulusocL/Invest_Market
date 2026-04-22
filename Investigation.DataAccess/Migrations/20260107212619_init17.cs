using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MessageUserBlockList_IsBlocked_IsRemoved",
                table: "MessageUserBlockLists",
                columns: new[] { "IsBlocked", "IsRemoved" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageUserBlockList_IsBlocked_IsRemoved",
                table: "MessageUserBlockLists");
        }
    }
}
