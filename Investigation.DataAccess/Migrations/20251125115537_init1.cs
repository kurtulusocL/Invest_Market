using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonUniqueHit = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmCode = table.Column<int>(type: "int", nullable: true),
                    IsAcceptedPolicies = table.Column<bool>(type: "bit", nullable: false),
                    IsLoginConfirmCodeActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRegisterConfirmCodeActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaAccessed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrowserVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMobile = table.Column<bool>(type: "bit", nullable: false),
                    DeviceModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MacAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalIpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddressVPN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProxyConnection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blockeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MacAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddressVPN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blockeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancelMembershipCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelMembershipCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationMap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mernis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLoggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionStackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLoggers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frequentlies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequentlies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HowItWorkses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowItWorkses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestorCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KVKKs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVKKs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UseFor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hit = table.Column<int>(type: "int", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PricingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectorNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    Hit = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhatWeOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhatWeOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileImages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileImages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MacAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddressVPN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlackLists_Audits_AuditId",
                        column: x => x.AuditId,
                        principalTable: "Audits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CancelMemberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequestCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestCancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hit = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CancelMembershipCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelMemberships_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CancelMemberships_CancelMembershipCategories_CancelMembershipCategoryId",
                        column: x => x.CancelMembershipCategoryId,
                        principalTable: "CancelMembershipCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlans_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvestArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SinceWhen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLookingForCompany = table.Column<bool>(type: "bit", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    InvestorCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investors_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investors_InvestorCategories_InvestorCategoryId",
                        column: x => x.InvestorCategoryId,
                        principalTable: "InvestorCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pricings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricingCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pricings_PricingCategories_PricingCategoryId",
                        column: x => x.PricingCategoryId,
                        principalTable: "PricingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSectors_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentPlanId = table.Column<int>(type: "int", nullable: false),
                    NextPlanId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_PaymentPlans_NextPlanId",
                        column: x => x.NextPlanId,
                        principalTable: "PaymentPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_PaymentPlans_PaymentPlanId",
                        column: x => x.PaymentPlanId,
                        principalTable: "PaymentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slogan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortBio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLookingForInvest = table.Column<bool>(type: "bit", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GitHub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyCategoryId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    SubSectorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyCategories_CompanyCategoryId",
                        column: x => x.CompanyCategoryId,
                        principalTable: "CompanyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_SubSectors_SubSectorId",
                        column: x => x.SubSectorId,
                        principalTable: "SubSectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecentlyInvests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExit = table.Column<bool>(type: "bit", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WebUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    SubSectorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentlyInvests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecentlyInvests_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecentlyInvests_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecentlyInvests_SubSectors_SubSectorId",
                        column: x => x.SubSectorId,
                        principalTable: "SubSectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserSubscriptionId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_UserSubscriptions_UserSubscriptionId",
                        column: x => x.UserSubscriptionId,
                        principalTable: "UserSubscriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnouncementCategoryId = table.Column<int>(type: "int", nullable: false),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_AnnouncementCategories_AnnouncementCategoryId",
                        column: x => x.AnnouncementCategoryId,
                        principalTable: "AnnouncementCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Announcements_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcements_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blogs_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyFinances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ARRIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    VisibilitySettingId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFinances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFinances_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyPinteches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketingStrategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowingPotantial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    VisibilitySettingId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPinteches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPinteches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    VisibilitySettingId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStages_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalExperienceDuration = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTeams_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCommentable = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    AllowMultipleResponses = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surveys_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSocialMedias_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSocialMedias_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VisibilitySettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsVisibleForCompanies = table.Column<bool>(type: "bit", nullable: false),
                    IsVisibleForInvestors = table.Column<bool>(type: "bit", nullable: false),
                    IsVisibleForNone = table.Column<bool>(type: "bit", nullable: false),
                    IsNotVisibleForCompanies = table.Column<bool>(type: "bit", nullable: false),
                    IsNotVisibleForInvestors = table.Column<bool>(type: "bit", nullable: false),
                    CompanyFinanceId = table.Column<int>(type: "int", nullable: true),
                    CompanyPintechId = table.Column<int>(type: "int", nullable: true),
                    CompanyStageId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisibilitySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisibilitySettings_CompanyFinances_CompanyFinanceId",
                        column: x => x.CompanyFinanceId,
                        principalTable: "CompanyFinances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisibilitySettings_CompanyPinteches_CompanyPintechId",
                        column: x => x.CompanyPintechId,
                        principalTable: "CompanyPinteches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisibilitySettings_CompanyStages_CompanyStageId",
                        column: x => x.CompanyStageId,
                        principalTable: "CompanyStages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pictures_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pictures_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavedContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSaved = table.Column<bool>(type: "bit", nullable: false),
                    SaveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisSaveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    SectorNewsId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedContents_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedContents_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedContents_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedContents_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedContents_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedContents_SectorNews_SectorNewsId",
                        column: x => x.SectorNewsId,
                        principalTable: "SectorNews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedContents_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalyticsDataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalResponses = table.Column<int>(type: "int", nullable: false),
                    CompletionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AverageCompletionTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnalytics_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentAnswers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentAnswers_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    SurveyQuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdId = table.Column<int>(type: "int", nullable: true),
                    AnnouncementId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    CommentAnswerId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyFinanceId = table.Column<int>(type: "int", nullable: true),
                    CompanyPintechId = table.Column<int>(type: "int", nullable: true),
                    CompanyStageId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hits_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hits_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_CommentAnswers_CommentAnswerId",
                        column: x => x.CommentAnswerId,
                        principalTable: "CommentAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_CompanyFinances_CompanyFinanceId",
                        column: x => x.CompanyFinanceId,
                        principalTable: "CompanyFinances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_CompanyPinteches_CompanyPintechId",
                        column: x => x.CompanyPintechId,
                        principalTable: "CompanyPinteches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_CompanyStages_CompanyStageId",
                        column: x => x.CompanyStageId,
                        principalTable: "CompanyStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    CommentAnswerId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_CommentAnswers_CommentAnswerId",
                        column: x => x.CommentAnswerId,
                        principalTable: "CommentAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    FixedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportCategoryId = table.Column<int>(type: "int", nullable: false),
                    AnnouncementId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    CommentAnswerId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    InvestorId = table.Column<int>(type: "int", nullable: true),
                    NewsId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    SectorNewsId = table.Column<int>(type: "int", nullable: true),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_CommentAnswers_CommentAnswerId",
                        column: x => x.CommentAnswerId,
                        principalTable: "CommentAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Newses_NewsId",
                        column: x => x.NewsId,
                        principalTable: "Newses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_ReportCategories_ReportCategoryId",
                        column: x => x.ReportCategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_SectorNews_SectorNewsId",
                        column: x => x.SectorNewsId,
                        principalTable: "SectorNews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SurveyResponseId = table.Column<int>(type: "int", nullable: true),
                    SurveyQuestionId = table.Column<int>(type: "int", nullable: true),
                    QuestionOptionId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_QuestionOptions_QuestionOptionId",
                        column: x => x.QuestionOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyResponses_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_About_Id",
                table: "Abouts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_About_IsActive_IsDeleted",
                table: "Abouts",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_Id",
                table: "Ad",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ad_IsActive_IsDeleted",
                table: "Ad",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_StartDate_FinishDate",
                table: "Ad",
                columns: new[] { "StartDate", "FinishDate" });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCategory_Id",
                table: "AnnouncementCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCategory_IsActive_IsDeleted",
                table: "AnnouncementCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_AnnouncementCategoryId",
                table: "Announcements",
                column: "AnnouncementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_CompanyId",
                table: "Announcements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_Id",
                table: "Announcements",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_InvestorId",
                table: "Announcements",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_IsActive_IsDeleted",
                table: "Announcements",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_Id",
                table: "AspNetRoles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_IsActive_IsDeleted",
                table: "AspNetRoles",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_CreatedDate_UpdatedDate_DeletedDate",
                table: "AspNetUsers",
                columns: new[] { "CreatedDate", "UpdatedDate", "DeletedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Id",
                table: "AspNetUsers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_IsActive_IsDeleted",
                table: "AspNetUsers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_IsAdmin",
                table: "AspNetUsers",
                column: "IsAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_IsLoginConfirmCodeActive_IsRegisterConfirmCodeActive",
                table: "AspNetUsers",
                columns: new[] { "IsLoginConfirmCodeActive", "IsRegisterConfirmCodeActive" });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_Id",
                table: "Audits",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audit_IsActive_IsDeleted",
                table: "Audits",
                columns: new[] { "IsActive", "IsDeleted" });

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

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_AuditId",
                table: "BlackLists",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_Id",
                table: "BlackLists",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_IsActive_IsDeleted",
                table: "BlackLists",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Blocked_Id",
                table: "Blockeds",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blocked_IsActive_IsDeleted",
                table: "Blockeds",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_Id",
                table: "BlogCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_IsActive_IsDeleted",
                table: "BlogCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_AppUserId",
                table: "Blogs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CompanyId",
                table: "Blogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Id",
                table: "Blogs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_InvestorId",
                table: "Blogs",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_IsActive_IsDeleted",
                table: "Blogs",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembershipCategory_Id",
                table: "CancelMembershipCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembershipCategory_IsActive_IsDeleted",
                table: "CancelMembershipCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembership_AppUserId",
                table: "CancelMemberships",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembership_CancelMembershipCategoryId",
                table: "CancelMemberships",
                column: "CancelMembershipCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembership_Id",
                table: "CancelMemberships",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembership_IsActive_IsDeleted",
                table: "CancelMemberships",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_AppUserId",
                table: "CommentAnswers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_BlogId",
                table: "CommentAnswers",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_Id",
                table: "CommentAnswers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_IsActive_IsDeleted",
                table: "CommentAnswers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AppUserId",
                table: "Comments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BlogId",
                table: "Comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CompanyId",
                table: "Comments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Id",
                table: "Comments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IsActive_IsDeleted",
                table: "Comments",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AppUserId",
                table: "Companies",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyCategoryId",
                table: "Companies",
                column: "CompanyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id",
                table: "Companies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_IsActive_IsDeleted",
                table: "Companies",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Company_SectorId",
                table: "Companies",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_SubSectorId",
                table: "Companies",
                column: "SubSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategory_Id",
                table: "CompanyCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategory_IsActive_IsDeleted",
                table: "CompanyCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_CompanyId",
                table: "CompanyContacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_Id",
                table: "CompanyContacts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_IsActive_IsDeleted",
                table: "CompanyContacts",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFinance_CompanyId",
                table: "CompanyFinances",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFinance_Id",
                table: "CompanyFinances",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFinance_IsActive_IsDeleted",
                table: "CompanyFinances",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFinance_VisibilitySettingId",
                table: "CompanyFinances",
                column: "VisibilitySettingId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPintech_CompanyId",
                table: "CompanyPinteches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPintech_Id",
                table: "CompanyPinteches",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPintech_IsActive_IsDeleted",
                table: "CompanyPinteches",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPintech_VisibilitySettingId",
                table: "CompanyPinteches",
                column: "VisibilitySettingId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStage_CompanyId",
                table: "CompanyStages",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStage_Id",
                table: "CompanyStages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStage_IsActive_IsDeleted",
                table: "CompanyStages",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStage_VisibilitySettingId",
                table: "CompanyStages",
                column: "VisibilitySettingId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTeam_CompanyId",
                table: "CompanyTeams",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTeam_Id",
                table: "CompanyTeams",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTeam_IsActive_IsDeleted",
                table: "CompanyTeams",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Id",
                table: "Contacts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_IsActive_IsDeleted",
                table: "Contacts",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Id",
                table: "Countries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_IsActive_IsDeleted",
                table: "Countries",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Id",
                table: "Currencies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_IsActive_IsDeleted",
                table: "Currencies",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_DataPolicy_Id",
                table: "DataPolicies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataPolicy_IsActive_IsDeleted",
                table: "DataPolicies",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionLogger_Id",
                table: "ExceptionLoggers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionLogger_IsActive_IsDeleted",
                table: "ExceptionLoggers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Frequently_Id",
                table: "Frequentlies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frequently_IsActive_IsDeleted",
                table: "Frequentlies",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Hit_AdId",
                table: "Hits",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_AnnouncementId",
                table: "Hits",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_AppUserId",
                table: "Hits",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_BlogId",
                table: "Hits",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CommentAnswerId",
                table: "Hits",
                column: "CommentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CommentId",
                table: "Hits",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CompanyFinanceId",
                table: "Hits",
                column: "CompanyFinanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CompanyId",
                table: "Hits",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CompanyPintechId",
                table: "Hits",
                column: "CompanyPintechId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CompanyStageId",
                table: "Hits",
                column: "CompanyStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_Id",
                table: "Hits",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hit_InvestorId",
                table: "Hits",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_IsActive_IsDeleted",
                table: "Hits",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Hit_PostId",
                table: "Hits",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_SurveyId",
                table: "Hits",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_HowItWorks_Id",
                table: "HowItWorkses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HowItWorks_IsActive_IsDeleted",
                table: "HowItWorkses",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_InvestorCategory_Id",
                table: "InvestorCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestorCategory_IsActive_IsDeleted",
                table: "InvestorCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Investor_AppUserId",
                table: "Investors",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Investor_CountryId",
                table: "Investors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Investor_Id",
                table: "Investors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investor_InvestorCategoryId",
                table: "Investors",
                column: "InvestorCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Investor_IsActive_IsDeleted",
                table: "Investors",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Investor_IsLookingForCompany",
                table: "Investors",
                column: "IsLookingForCompany");

            migrationBuilder.CreateIndex(
                name: "IX_KVKK_Id",
                table: "KVKKs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KVKK_IsActive_IsDeleted",
                table: "KVKKs",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_LayoutInfo_Id",
                table: "LayoutInfos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LayoutInfo_IsActive_IsDeleted",
                table: "LayoutInfos",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Like_AppUserId",
                table: "Likes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_BlogId",
                table: "Likes",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentAnswerId",
                table: "Likes",
                column: "CommentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CompanyId",
                table: "Likes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_Id",
                table: "Likes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_InvestorId",
                table: "Likes",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_IsActive_IsDeleted",
                table: "Likes",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_SurveyId",
                table: "Likes",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Logo_Id",
                table: "Logos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logo_IsActive_IsDeleted",
                table: "Logos",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_News_Id",
                table: "Newses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_IsActive_IsDeleted",
                table: "Newses",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlan_CurrencyId",
                table: "PaymentPlans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlan_Id",
                table: "PaymentPlans",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlan_IsActive_IsDeleted",
                table: "PaymentPlans",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_AppUserId",
                table: "Payments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Id",
                table: "Payments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IsActive_IsDeleted",
                table: "Payments",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserSubscriptionId",
                table: "Payments",
                column: "UserSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_Id",
                table: "PersonalDatas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_IsActive_IsDeleted",
                table: "PersonalDatas",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_BlogId",
                table: "Pictures",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_CompanyId",
                table: "Pictures",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_Id",
                table: "Pictures",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_IsActive_IsDeleted",
                table: "Pictures",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_PostId",
                table: "Pictures",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AppUserId",
                table: "Posts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CompanyId",
                table: "Posts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Id",
                table: "Posts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_InvestorId",
                table: "Posts",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IsActive_IsDeleted",
                table: "Posts",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_PricingCategory_Id",
                table: "PricingCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricingCategory_IsActive_IsDeleted",
                table: "PricingCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_Id",
                table: "Pricings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_IsActive_IsDeleted",
                table: "Pricings",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_PricingCategoryId",
                table: "Pricings",
                column: "PricingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_AppUserId",
                table: "ProfileImages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_Id",
                table: "ProfileImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_IsActive_IsDeleted",
                table: "ProfileImages",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_Id",
                table: "QuestionOptions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_IsActive_IsDeleted",
                table: "QuestionOptions",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_SurveyQuestionId",
                table: "QuestionOptions",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyInvest_Id",
                table: "RecentlyInvests",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyInvest_InvestorId",
                table: "RecentlyInvests",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyInvest_IsActive_IsDeleted",
                table: "RecentlyInvests",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyInvest_IsExit",
                table: "RecentlyInvests",
                column: "IsExit");

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyInvest_SectorId",
                table: "RecentlyInvests",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyInvest_SubSectorId",
                table: "RecentlyInvests",
                column: "SubSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategory_Id",
                table: "ReportCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategory_IsActive_IsDeleted",
                table: "ReportCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Report_AnnouncementId",
                table: "Reports",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_AppUserId",
                table: "Reports",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BlogId",
                table: "Reports",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CommentAnswerId",
                table: "Reports",
                column: "CommentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CommentId",
                table: "Reports",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CompanyId",
                table: "Reports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Id",
                table: "Reports",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_InvestorId",
                table: "Reports",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_IsActive_IsDeleted",
                table: "Reports",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Report_NewsId",
                table: "Reports",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_PostId",
                table: "Reports",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportCategoryId",
                table: "Reports",
                column: "ReportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SectorNewsId",
                table: "Reports",
                column: "SectorNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SurveyId",
                table: "Reports",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_AppUserId",
                table: "SavedContents",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_BlogId",
                table: "SavedContents",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_CompanyId",
                table: "SavedContents",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_Id",
                table: "SavedContents",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_InvestorId",
                table: "SavedContents",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_IsActive_IsDeleted",
                table: "SavedContents",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_IsSaved",
                table: "SavedContents",
                column: "IsSaved");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_PostId",
                table: "SavedContents",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_SectorNewsId",
                table: "SavedContents",
                column: "SectorNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_SurveyId",
                table: "SavedContents",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SectorNews_Id",
                table: "SectorNews",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectorNews_IsActive_IsDeleted",
                table: "SectorNews",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Sector_Id",
                table: "Sectors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sector_IsActive_IsDeleted",
                table: "Sectors",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Slider_Id",
                table: "Sliders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slider_IsActive_IsDeleted",
                table: "Sliders",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_Id",
                table: "SocialMedias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_IsActive_IsDeleted",
                table: "SocialMedias",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SubSector_Id",
                table: "SubSectors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubSector_IsActive_IsDeleted",
                table: "SubSectors",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SubSector_SectorId",
                table: "SubSectors",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnalytics_Id",
                table: "SurveyAnalytics",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnalytics_IsActive_IsDeleted",
                table: "SurveyAnalytics",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnalytics_SurveyId",
                table: "SurveyAnalytics",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_AppUserId",
                table: "SurveyAnswers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_Id",
                table: "SurveyAnswers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_IsActive_IsDeleted",
                table: "SurveyAnswers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_QuestionOptionId",
                table: "SurveyAnswers",
                column: "QuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_SurveyQuestionId",
                table: "SurveyAnswers",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_SurveyResponseId",
                table: "SurveyAnswers",
                column: "SurveyResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_Id",
                table: "SurveyQuestions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_IsActive_IsDeleted",
                table: "SurveyQuestions",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_AppUserId",
                table: "SurveyResponses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_Id",
                table: "SurveyResponses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_IsActive_IsDeleted",
                table: "SurveyResponses",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_AppUserId",
                table: "Surveys",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_CompanyId",
                table: "Surveys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_Id",
                table: "Surveys",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_InvestorId",
                table: "Surveys",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_IsActive_IsDeleted",
                table: "Surveys",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Survey_IsOnline",
                table: "Surveys",
                column: "IsOnline");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_StartDate_ClosedDate",
                table: "Surveys",
                columns: new[] { "StartDate", "ClosedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreement_Id",
                table: "UserAgreements",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreement_IsActive_IsDeleted",
                table: "UserAgreements",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileImage_AppUserId",
                table: "UserProfileImages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileImage_Id",
                table: "UserProfileImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileImage_IsActive_IsDeleted",
                table: "UserProfileImages",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_AppUserId",
                table: "UserSessions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_Id",
                table: "UserSessions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_IsActive_IsDeleted",
                table: "UserSessions",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_IsOnline",
                table: "UserSessions",
                column: "IsOnline");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_LoginDate",
                table: "UserSessions",
                column: "LoginDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_LogoutDate",
                table: "UserSessions",
                column: "LogoutDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMedia_CompanyId",
                table: "UserSocialMedias",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMedia_Id",
                table: "UserSocialMedias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMedia_InvestorId",
                table: "UserSocialMedias",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMedia_IsActive_IsDeleted",
                table: "UserSocialMedias",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_AppUserId",
                table: "UserSubscriptions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_Id",
                table: "UserSubscriptions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_IsActive_IsDeleted",
                table: "UserSubscriptions",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_NextPlanId",
                table: "UserSubscriptions",
                column: "NextPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_PaymentPlanId",
                table: "UserSubscriptions",
                column: "PaymentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_VisibilitySetting_CompanyFinanceId",
                table: "VisibilitySettings",
                column: "CompanyFinanceId",
                unique: true,
                filter: "[CompanyFinanceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VisibilitySetting_CompanyPintechId",
                table: "VisibilitySettings",
                column: "CompanyPintechId",
                unique: true,
                filter: "[CompanyPintechId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VisibilitySetting_CompanyStageId",
                table: "VisibilitySettings",
                column: "CompanyStageId",
                unique: true,
                filter: "[CompanyStageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VisibilitySetting_Id",
                table: "VisibilitySettings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisibilitySetting_IsActive_IsDeleted",
                table: "VisibilitySettings",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_WhatWeOffer_Id",
                table: "WhatWeOffers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WhatWeOffer_IsActive_IsDeleted",
                table: "WhatWeOffers",
                columns: new[] { "IsActive", "IsDeleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlackLists");

            migrationBuilder.DropTable(
                name: "Blockeds");

            migrationBuilder.DropTable(
                name: "CancelMemberships");

            migrationBuilder.DropTable(
                name: "CompanyContacts");

            migrationBuilder.DropTable(
                name: "CompanyTeams");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "DataPolicies");

            migrationBuilder.DropTable(
                name: "ExceptionLoggers");

            migrationBuilder.DropTable(
                name: "Frequentlies");

            migrationBuilder.DropTable(
                name: "Hits");

            migrationBuilder.DropTable(
                name: "HowItWorkses");

            migrationBuilder.DropTable(
                name: "KVKKs");

            migrationBuilder.DropTable(
                name: "LayoutInfos");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Logos");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PersonalDatas");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Pricings");

            migrationBuilder.DropTable(
                name: "ProfileImages");

            migrationBuilder.DropTable(
                name: "RecentlyInvests");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SavedContents");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "SurveyAnalytics");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropTable(
                name: "UserAgreements");

            migrationBuilder.DropTable(
                name: "UserProfileImages");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "UserSocialMedias");

            migrationBuilder.DropTable(
                name: "VisibilitySettings");

            migrationBuilder.DropTable(
                name: "WhatWeOffers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "CancelMembershipCategories");

            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "PricingCategories");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "CommentAnswers");

            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.DropTable(
                name: "ReportCategories");

            migrationBuilder.DropTable(
                name: "SectorNews");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "SurveyResponses");

            migrationBuilder.DropTable(
                name: "CompanyFinances");

            migrationBuilder.DropTable(
                name: "CompanyPinteches");

            migrationBuilder.DropTable(
                name: "CompanyStages");

            migrationBuilder.DropTable(
                name: "PaymentPlans");

            migrationBuilder.DropTable(
                name: "AnnouncementCategories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Investors");

            migrationBuilder.DropTable(
                name: "CompanyCategories");

            migrationBuilder.DropTable(
                name: "SubSectors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "InvestorCategories");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
