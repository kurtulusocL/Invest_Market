using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hits_Ad_AdId",
                table: "Hits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ad",
                table: "Ad");

            migrationBuilder.RenameTable(
                name: "Ad",
                newName: "Ads");

            migrationBuilder.AddColumn<bool>(
                name: "HasTarget",
                table: "Ads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ads",
                table: "Ads",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AdTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinAge = table.Column<int>(type: "int", nullable: true),
                    MaxAge = table.Column<int>(type: "int", nullable: true),
                    TargetCountries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetCategoryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetCategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinInteractionCount = table.Column<int>(type: "int", nullable: false),
                    AdId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdTargets_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdTarget_AdId",
                table: "AdTargets",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdTarget_Id",
                table: "AdTargets",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdTarget_IsActive_IsDeleted",
                table: "AdTargets",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_Ads_AdId",
                table: "Hits",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hits_Ads_AdId",
                table: "Hits");

            migrationBuilder.DropTable(
                name: "AdTargets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ads",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "HasTarget",
                table: "Ads");

            migrationBuilder.RenameTable(
                name: "Ads",
                newName: "Ad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ad",
                table: "Ad",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_Ad_AdId",
                table: "Hits",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id");
        }
    }
}
