using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Services;
using Repres.Application.Models.Chat;
using Repres.Domain.Contracts;
using Repres.Domain.Entities.ExtendedAttributes;
using Repres.Domain.Entities.Misc;
using Repres.Domain.Entities.ThirdParty;
using Repres.Domain.Entities.ThirdParty.Oura;
using Repres.Infrastructure.Models.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Contexts
{
    public class BlazorHeroContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public BlazorHeroContext(DbContextOptions<BlazorHeroContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<ChatHistory<BlazorHeroUser>> ChatHistories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentExtendedAttribute> DocumentExtendedAttributes { get; set; }
        public DbSet<Api> Apis { get; set; }
        public DbSet<ApiByUser> ApiByUsers { get; set; }

        // OURA
        public DbSet<Sleep> SleepSummary { get; set; }
        public DbSet<Readiness> ReadinessSummary { get; set; }
        public DbSet<Activity> ActivitySummary { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
            builder.Entity<ChatHistory<BlazorHeroUser>>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<BlazorHeroUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<BlazorHeroRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<BlazorHeroRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });

            // OURA MODELS

            //builder.Entity<Sleep>()
            //    .Property(e => e.hr_5min)
            //    .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Cast<int>().ToArray());

            //builder.Entity<Sleep>()
            //    .Property(e => e.rmssd_5min)
            //    .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Cast<int>().ToArray());

            //builder.Entity<Activity>()
            //    .Property(e => e.met_1min)
            //    .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Cast<float>().ToArray());
        }
    }
}