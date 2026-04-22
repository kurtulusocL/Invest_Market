using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFollowable",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFollowable",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFollowed = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    FollowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnfollowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanceledFollowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FollowerCompanyId = table.Column<int>(type: "int", nullable: true),
                    FollowedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FollowedCompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_AspNetUsers_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_AspNetUsers_FollowerUserId",
                        column: x => x.FollowerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_Companies_FollowedCompanyId",
                        column: x => x.FollowedCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_Companies_FollowerCompanyId",
                        column: x => x.FollowerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FollowedCompanyId",
                table: "Follows",
                column: "FollowedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FollowedUserId",
                table: "Follows",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FollowerUserId",
                table: "Follows",
                column: "FollowerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_Id",
                table: "Follows",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Follow_IsActive_IsDeleted",
                table: "Follows",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_IsCanceled",
                table: "Follows",
                column: "IsCanceled");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_IsFollowed",
                table: "Follows",
                column: "IsFollowed");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerCompanyId",
                table: "Follows",
                column: "FollowerCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropColumn(
                name: "IsFollowable",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsFollowable",
                table: "AspNetUsers");
        }
    }
}
