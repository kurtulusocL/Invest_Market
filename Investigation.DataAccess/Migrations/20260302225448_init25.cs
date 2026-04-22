using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Audit_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audit_MacAddress",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audit_MacAddress_IsActive_IsDeleted",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Blockeds");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "MacAddress",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "MacAddress",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "MacAddress",
                table: "Blockeds",
                newName: "DeviceFingerprint");

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "BlackLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DeviceFingerprint",
                table: "BlackLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DeviceFingerprint",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "Audits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audit_RemoteIpAddress_IpAddressVPN",
                table: "Audits",
                columns: new[] { "RemoteIpAddress", "IpAddressVPN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Audit_RemoteIpAddress_IpAddressVPN",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "DeviceFingerprint",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "DeviceFingerprint",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "DeviceFingerprint",
                table: "Blockeds",
                newName: "MacAddress");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Blockeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "BlackLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "BlackLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MacAddress",
                table: "BlackLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "Audits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MacAddress",
                table: "Audits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "Audits",
                columns: new[] { "LocalIpAddress", "RemoteIpAddress", "IpAddressVPN" });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_MacAddress",
                table: "Audits",
                column: "MacAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_MacAddress_IsActive_IsDeleted",
                table: "Audits",
                columns: new[] { "MacAddress", "IsActive", "IsDeleted" });
        }
    }
}
