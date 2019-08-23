﻿// <auto-generated />
using CarInsuranceCalculator.Data;
using CarInsuranceCalculator.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CarInsuranceCalculator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.CarMake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CarMakes");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CarMakeId");

                    b.Property<int>("EngineCapacity");

                    b.Property<int>("Horsepower");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("ProductionYear");

                    b.Property<bool>("SpecialModel");

                    b.HasKey("Id");

                    b.HasIndex("CarMakeId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.InfoForInsurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarAge");

                    b.Property<int?>("CarInsurerId");

                    b.Property<int?>("CarMakeId")
                        .IsRequired();

                    b.Property<int?>("CarModelId")
                        .IsRequired();

                    b.Property<int?>("MTPLInsurerId");

                    b.Property<bool>("NewImport");

                    b.Property<int>("NumberOfPayments");

                    b.Property<int>("OwnerAge");

                    b.Property<int?>("RegionId")
                        .IsRequired();

                    b.Property<bool>("RightSideWheel");

                    b.Property<int?>("TypeOfInsuranceId")
                        .IsRequired();

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CarInsurerId");

                    b.HasIndex("CarMakeId");

                    b.HasIndex("CarModelId");

                    b.HasIndex("MTPLInsurerId");

                    b.HasIndex("RegionId");

                    b.HasIndex("TypeOfInsuranceId");

                    b.ToTable("InfoForInsurances");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.Insurer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<double>("MaximumDiscount");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Insurers");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("InfoForInsuranceId");

                    b.Property<int?>("InsurerId");

                    b.Property<double>("Premium");

                    b.HasKey("Id");

                    b.HasIndex("InfoForInsuranceId");

                    b.HasIndex("InsurerId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Abbreviation")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.RiskOrBonus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Nomenclature")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Nomenclature")
                        .IsUnique();

                    b.ToTable("RisksOrBonuses");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.Models.InsurerRiskOrBonus", b =>
                {
                    b.Property<int>("InsurerId");

                    b.Property<int>("RiskOrBonusId");

                    b.Property<double>("TariffNumberChange");

                    b.HasKey("InsurerId", "RiskOrBonusId");

                    b.HasIndex("RiskOrBonusId");

                    b.ToTable("InsurersRisksOrBonuses");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.Models.InsurerTypeOfInsurance", b =>
                {
                    b.Property<int>("InsurerId");

                    b.Property<int>("TypeOfInsuranceId");

                    b.Property<double>("TariffNumber");

                    b.HasKey("InsurerId", "TypeOfInsuranceId");

                    b.HasIndex("TypeOfInsuranceId");

                    b.ToTable("InsurersTypesOfInsurance");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.Models.TypeOfInsurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TypesOfInsurance");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.CarModel", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Data.Models.CarMake", "CarMake")
                        .WithMany()
                        .HasForeignKey("CarMakeId");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.InfoForInsurance", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Data.Models.Insurer", "CarInsurer")
                        .WithMany()
                        .HasForeignKey("CarInsurerId");

                    b.HasOne("CarInsuranceCalculator.Data.Models.CarMake", "CarMake")
                        .WithMany()
                        .HasForeignKey("CarMakeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarInsuranceCalculator.Data.Models.CarModel", "CarModel")
                        .WithMany()
                        .HasForeignKey("CarModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarInsuranceCalculator.Data.Models.Insurer", "MTPLInsurer")
                        .WithMany()
                        .HasForeignKey("MTPLInsurerId");

                    b.HasOne("CarInsuranceCalculator.Data.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarInsuranceCalculator.Models.Models.TypeOfInsurance", "TypeOfInsurance")
                        .WithMany()
                        .HasForeignKey("TypeOfInsuranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.Insurer", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.Offer", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Data.Models.InfoForInsurance", "InfoForInsurance")
                        .WithMany()
                        .HasForeignKey("InfoForInsuranceId");

                    b.HasOne("CarInsuranceCalculator.Data.Models.Insurer", "Insurer")
                        .WithMany()
                        .HasForeignKey("InsurerId");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Data.Models.RiskOrBonus", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Models.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.Models.InsurerRiskOrBonus", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Data.Models.Insurer", "Insurer")
                        .WithMany("InsurersRisksOrBonuses")
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarInsuranceCalculator.Data.Models.RiskOrBonus", "RiskOrBonus")
                        .WithMany("InsurersRisksOrBonuses")
                        .HasForeignKey("RiskOrBonusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CarInsuranceCalculator.Models.Models.InsurerTypeOfInsurance", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Data.Models.Insurer", "Insurer")
                        .WithMany("InsurersTypesOfInsurance")
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarInsuranceCalculator.Models.Models.TypeOfInsurance", "TypeOfInsurance")
                        .WithMany("InsurersTypesOfInsurance")
                        .HasForeignKey("TypeOfInsuranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarInsuranceCalculator.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarInsuranceCalculator.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
