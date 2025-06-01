using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System.IO;

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
        public DbSet<CriterionValue> CriterionValues
            => Set<CriterionValue>();
        public DbSet<Contract> Contracts => Set<Contract>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User → Role (многие пользователи — одна роль)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tender → CreatedBy (один пользователь — много тендеров)
            modelBuilder.Entity<Tender>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // TenderCriterion → Tender
            modelBuilder.Entity<TenderCriterion>()
                .HasOne(tc => tc.Tender)
                .WithMany(t => t.Criteria)
                .HasForeignKey(tc => tc.TenderId)
                .OnDelete(DeleteBehavior.Cascade); // удаление тендера удаляет критерии

            // TenderCriterion → Criterion
            modelBuilder.Entity<TenderCriterion>()
                .HasOne(tc => tc.Criterion)
                .WithMany()
                .HasForeignKey(tc => tc.CriterionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Proposal → Tender
            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Tender)
                .WithMany(t => t.Proposals)
                .HasForeignKey(p => p.TenderId)
                .OnDelete(DeleteBehavior.NoAction); // ← чтобы разорвать цикл

            // Proposal → Byer (User)
            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Byer)
                .WithMany()
                .HasForeignKey(p => p.ByerId)
                .OnDelete(DeleteBehavior.Restrict);

            // CriterionValue → Proposal
            modelBuilder.Entity<CriterionValue>()
                .HasOne(cv => cv.Proposal)
                .WithMany(p => p.Values)
                .HasForeignKey(cv => cv.ProposalId)
                .OnDelete(DeleteBehavior.Cascade); // удаление заявки удаляет значения

            // CriterionValue → TenderCriterion
            modelBuilder.Entity<CriterionValue>()
                .HasOne(cv => cv.TenderCriterion)
                .WithMany()
                .HasForeignKey(cv => cv.TenderCriterionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contract → Proposal
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Proposal)
                .WithMany()
                .HasForeignKey(c => c.ProposalId)
                .OnDelete(DeleteBehavior.Cascade); // заявка удаляется — контракт тоже
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
