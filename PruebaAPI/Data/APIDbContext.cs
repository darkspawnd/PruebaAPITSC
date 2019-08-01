using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaAPI.Data
{
    public class APIDbContext : IdentityDbContext<IdentityUser>
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountrySubdivision> CountrySubdivisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "User", NormalizedName = "USER" }
            );
            modelBuilder.Entity<Country>().ToTable("tbl_countries").HasData(
                    new {
                        Id = 1,
                        Name = "Guatemala",
                        Alpha2Code = "GT",
                        Alpha3Code = "GTM",
                        NumericCode = 320,
                        ISOReference = "ISO 3166-2:GT",
                        Independent = true
                    },
                    new
                    {
                        Id = 2,
                        Name = "Slovenia",
                        Alpha2Code = "SI",
                        Alpha3Code = "SVN",
                        NumericCode = 705,
                        ISOReference = "ISO 3166-2:SI",
                        Independent = true
                    }
                );

            modelBuilder.Entity<CountrySubdivision>().ToTable("tbl_countries_subdivisions").HasData(
                    new
                    {
                        Id = 1,
                        Code = "GT-GU",
                        Name = "Guatemala",
                        LocalizedName = "Guatemala",
                        CountryId = 1
                    }, new
                    {
                        Id = 2,
                        Code = "GT-QZ",
                        Name = "Quetzaltenango",
                        LocalizedName = "Quetzaltenango",
                        CountryId = 1
                    }, new
                    {
                        Id = 3,
                        Code = "SI-213",
                        Name = "Ankaran",
                        LocalizedName = "Ancarano",
                        CountryId = 2
                    }, new
                    {
                        Id = 4,
                        Code = "SI-156",
                        Name = "Dobrovnik",
                        LocalizedName = "Dobronak",
                        CountryId = 2
                    }
                );

        }

    }
}
