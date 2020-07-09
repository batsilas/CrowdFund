using CrowFund.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowFund.Core.Data
{
    public class CrowFundDbContext : IdentityDbContext
    {

        public CrowFundDbContext(DbContextOptions<CrowFundDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<FundingPackage>()
                .ToTable("FundingPackage");

            modelBuilder
                .Entity<Photo>()
                .ToTable("Photo");

            modelBuilder
                .Entity<Video>()
                .ToTable("Video");


            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
                .Entity<Status>()
                .ToTable("Status");

            modelBuilder
                .Entity<UserBacker>()
                .ToTable("UserBacker");


        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
