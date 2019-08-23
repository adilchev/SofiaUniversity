using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarInsuranceCalculator.Migrations
{
    public partial class messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarMakes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMakes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfInsurance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfInsurance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "Insurers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    MaximumDiscount = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarMakeId = table.Column<int>(nullable: true),
                    EngineCapacity = table.Column<int>(nullable: false),
                    Horsepower = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductionYear = table.Column<int>(nullable: false),
                    SpecialModel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarMakes_CarMakeId",
                        column: x => x.CarMakeId,
                        principalTable: "CarMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RisksOrBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    Nomenclature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RisksOrBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RisksOrBonuses_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsurersTypesOfInsurance",
                columns: table => new
                {
                    InsurerId = table.Column<int>(nullable: false),
                    TypeOfInsuranceId = table.Column<int>(nullable: false),
                    TariffNumber = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurersTypesOfInsurance", x => new { x.InsurerId, x.TypeOfInsuranceId });
                    table.ForeignKey(
                        name: "FK_InsurersTypesOfInsurance_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurersTypesOfInsurance_TypesOfInsurance_TypeOfInsuranceId",
                        column: x => x.TypeOfInsuranceId,
                        principalTable: "TypesOfInsurance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoForInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarAge = table.Column<int>(nullable: false),
                    CarInsurerId = table.Column<int>(nullable: true),
                    CarMakeId = table.Column<int>(nullable: false),
                    CarModelId = table.Column<int>(nullable: false),
                    MTPLInsurerId = table.Column<int>(nullable: true),
                    NewImport = table.Column<bool>(nullable: false),
                    NumberOfPayments = table.Column<int>(nullable: false),
                    OwnerAge = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    RightSideWheel = table.Column<bool>(nullable: false),
                    TypeOfInsuranceId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoForInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoForInsurances_Insurers_CarInsurerId",
                        column: x => x.CarInsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfoForInsurances_CarMakes_CarMakeId",
                        column: x => x.CarMakeId,
                        principalTable: "CarMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoForInsurances_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoForInsurances_Insurers_MTPLInsurerId",
                        column: x => x.MTPLInsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfoForInsurances_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoForInsurances_TypesOfInsurance_TypeOfInsuranceId",
                        column: x => x.TypeOfInsuranceId,
                        principalTable: "TypesOfInsurance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsurersRisksOrBonuses",
                columns: table => new
                {
                    InsurerId = table.Column<int>(nullable: false),
                    RiskOrBonusId = table.Column<int>(nullable: false),
                    TariffNumberChange = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurersRisksOrBonuses", x => new { x.InsurerId, x.RiskOrBonusId });
                    table.ForeignKey(
                        name: "FK_InsurersRisksOrBonuses_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurersRisksOrBonuses_RisksOrBonuses_RiskOrBonusId",
                        column: x => x.RiskOrBonusId,
                        principalTable: "RisksOrBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InfoForInsuranceId = table.Column<int>(nullable: true),
                    InsurerId = table.Column<int>(nullable: true),
                    Premium = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_InfoForInsurances_InfoForInsuranceId",
                        column: x => x.InfoForInsuranceId,
                        principalTable: "InfoForInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarMakes_Name",
                table: "CarMakes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarMakeId",
                table: "CarModels",
                column: "CarMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoForInsurances_CarInsurerId",
                table: "InfoForInsurances",
                column: "CarInsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoForInsurances_CarMakeId",
                table: "InfoForInsurances",
                column: "CarMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoForInsurances_CarModelId",
                table: "InfoForInsurances",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoForInsurances_MTPLInsurerId",
                table: "InfoForInsurances",
                column: "MTPLInsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoForInsurances_RegionId",
                table: "InfoForInsurances",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoForInsurances_TypeOfInsuranceId",
                table: "InfoForInsurances",
                column: "TypeOfInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurers_ApplicationUserId",
                table: "Insurers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurers_Name",
                table: "Insurers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurersRisksOrBonuses_RiskOrBonusId",
                table: "InsurersRisksOrBonuses",
                column: "RiskOrBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurersTypesOfInsurance_TypeOfInsuranceId",
                table: "InsurersTypesOfInsurance",
                column: "TypeOfInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InfoForInsuranceId",
                table: "Offers",
                column: "InfoForInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InsurerId",
                table: "Offers",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Abbreviation",
                table: "Regions",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RisksOrBonuses_CategoryId",
                table: "RisksOrBonuses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RisksOrBonuses_Nomenclature",
                table: "RisksOrBonuses",
                column: "Nomenclature",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfInsurance_Name",
                table: "TypesOfInsurance",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "InsurersRisksOrBonuses");

            migrationBuilder.DropTable(
                name: "InsurersTypesOfInsurance");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "RisksOrBonuses");

            migrationBuilder.DropTable(
                name: "InfoForInsurances");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Insurers");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "TypesOfInsurance");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CarMakes");
        }
    }
}
