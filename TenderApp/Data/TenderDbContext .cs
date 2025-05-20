using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TenderApp.Models;

namespace TenderApp.Data
{
    public class TenderDbContext(DbContextOptions<TenderDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Tender> Tenders => Set<Tender>();
        public DbSet<Criterion> Criteria => Set<Criterion>();
        public DbSet<TenderCriterion> TenderCriteria 
            => Set<TenderCriterion>();
        public DbSet<Proposal> Proposals => Set<Proposal>();
        public DbSet<ProposalValue> ProposalValues 
            => Set<ProposalValue>();
        public DbSet<Contract> Contracts => Set<Contract>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User -> Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tender -> CreatedBy
            modelBuilder.Entity<Tender>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // TenderCriterion -> Tender + Criterion
            modelBuilder.Entity<TenderCriterion>()
                .HasOne(tc => tc.Tender)
                .WithMany(t => t.Criteria)
                .HasForeignKey(tc => tc.TenderId);

            modelBuilder.Entity<TenderCriterion>()
                .HasOne(tc => tc.Criterion)
                .WithMany()
                .HasForeignKey(tc => tc.CriterionId);

            // Proposal -> Tender + Byer
            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Tender)
                .WithMany(t => t.Proposals)
                .HasForeignKey(p => p.TenderId);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Byer)
                .WithMany()
                .HasForeignKey(p => p.ByerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProposalValue -> Proposal + Criterion
            modelBuilder.Entity<ProposalValue>()
                .HasOne(pv => pv.Proposal)
                .WithMany(p => p.Values)
                .HasForeignKey(pv => pv.ProposalId);

            modelBuilder.Entity<ProposalValue>()
                .HasOne(pv => pv.Criterion)
                .WithMany()
                .HasForeignKey(pv => pv.CriterionId);

            // Contract -> Tender + Winner
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Tender)
                .WithMany()
                .HasForeignKey(c => c.TenderId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Winner)
                .WithMany()
                .HasForeignKey(c => c.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public class ContextFactory : IDesignTimeDbContextFactory<TenderDbContext>
        {
            public TenderDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = 
                    new DbContextOptionsBuilder<TenderDbContext>();
                optionsBuilder.UseSqlServer(ConnectionString);
                return new TenderDbContext(optionsBuilder.Options);
            }

            public static string? ConnectionString
            {
                get
                {
                    var config = new ConfigurationBuilder()
                        .SetBasePath(Directory
                            .GetCurrentDirectory())
                        .AddJsonFile("config.json")
                        .Build();

                    return config.GetConnectionString("DefaultConnection");
                }
            }
        }
    }
}
