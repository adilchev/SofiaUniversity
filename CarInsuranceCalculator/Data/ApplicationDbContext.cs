using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarInsuranceCalculator.Models;
using CarInsuranceCalculator.Models.Models;
using Microsoft.AspNetCore.Identity;
using CarInsuranceCalculator.Models.Interfaces;

namespace CarInsuranceCalculator.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarMake> CarMakes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<InfoForInsurance> InfoForInsurances { get; set; }
        public DbSet<Insurer> Insurers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RiskOrBonus> RisksOrBonuses { get; set; }
        public DbSet<TypeOfInsurance> TypesOfInsurance{ get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<InsurerTypeOfInsurance> InsurersTypesOfInsurance { get; set; }
        public DbSet<InsurerRiskOrBonus> InsurersRisksOrBonuses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Region>().HasIndex(r => r.Name).IsUnique();
            builder.Entity<Region>().HasIndex(r => r.Abbreviation).IsUnique();

            builder.Entity<CarMake>().HasIndex(cm => cm.Name).IsUnique();

            builder.Entity<TypeOfInsurance>().HasIndex(t => t.Name).IsUnique();

            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

            builder.Entity<RiskOrBonus>().HasIndex(r => r.Nomenclature).IsUnique();

            builder.Entity<Insurer>().HasIndex(i => i.Name).IsUnique();

            builder.Entity<InsurerTypeOfInsurance>().HasKey(i => new {i.InsurerId, i.TypeOfInsuranceId});
            builder.Entity<InsurerTypeOfInsurance>().HasOne(i => i.Insurer).WithMany(i => i.InsurersTypesOfInsurance)
                .HasForeignKey(i => i.InsurerId);
            builder.Entity<InsurerTypeOfInsurance>().HasOne(i => i.TypeOfInsurance).WithMany(i => i.InsurersTypesOfInsurance)
                .HasForeignKey(i => i.TypeOfInsuranceId);

            builder.Entity<InsurerRiskOrBonus>().HasKey(i => new {i.InsurerId, i.RiskOrBonusId});
            builder.Entity<InsurerRiskOrBonus>().HasOne(i => i.Insurer).WithMany(i => i.InsurersRisksOrBonuses)
                .HasForeignKey(i => i.InsurerId);
            builder.Entity<InsurerRiskOrBonus>().HasOne(i => i.RiskOrBonus).WithMany(i => i.InsurersRisksOrBonuses)
                .HasForeignKey(i => i.RiskOrBonusId);
        }
    }
}
