using LogisticAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryConnection> CountryConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 1,
                CountryCode = "CAN",
                CountryName = "Canada"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 2,
                CountryCode = "USA",
                CountryName = "United States of America"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 3,
                CountryCode = "MEX",
                CountryName = "Mexico"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 4,
                CountryCode = "BLZ",
                CountryName = "Belize"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 5,
                CountryCode = "GTM",
                CountryName = "Guatemala"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 6,
                CountryCode = "SLV",
                CountryName = "Salvador"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 7,
                CountryCode = "HND",
                CountryName = "Honduras"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 8,
                CountryCode = "NIC",
                CountryName = "Nicaragua"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 9,
                CountryCode = "CRI",
                CountryName = "Costa Rica"
            });
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 10,
                CountryCode = "PAN",
                CountryName = "Panama"
            });

            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 1,
                CountryAId = 1,
                CountryBId = 2,
                CostOfRoad = 1
            });

            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 2,
                CountryAId = 2,
                CountryBId = 3,
                CostOfRoad = 1
            });

            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 3,
                CountryAId = 3,
                CountryBId = 4,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 4,
                CountryAId = 3,
                CountryBId = 5,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 5,
                CountryAId = 4,
                CountryBId = 5,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 6,
                CountryAId = 5,
                CountryBId = 6,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 7,
                CountryAId = 5,
                CountryBId = 7,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 8,
                CountryAId = 6,
                CountryBId = 7,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 9,
                CountryAId = 7,
                CountryBId = 8,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 10,
                CountryAId = 7,
                CountryBId = 8,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 11,
                CountryAId = 8,
                CountryBId = 9,
                CostOfRoad = 1
            });
            modelBuilder.Entity<CountryConnection>().HasData(new CountryConnection
            {
                Id = 12,
                CountryAId = 9,
                CountryBId = 10,
                CostOfRoad = 1
            });
        }
    }
}