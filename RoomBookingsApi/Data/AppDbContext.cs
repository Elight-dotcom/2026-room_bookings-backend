namespace RoomBookingsApi.Data;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RoomBookingsApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<BookingStatusHistory> BookingStatusHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tables and keys
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Room>().ToTable("Rooms");
        modelBuilder.Entity<Booking>().ToTable("Bookings");
        modelBuilder.Entity<Status>().ToTable("Statuses");
        modelBuilder.Entity<BookingStatusHistory>().ToTable("BookingStatusHistories");

        // Query filters for soft-delete
        modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Room>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<Booking>().HasQueryFilter(b => !b.IsDeleted);
        modelBuilder.Entity<Status>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<BookingStatusHistory>().HasQueryFilter(bsh => !bsh.IsDeleted);

        // Relationships
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
            .HasMany(b => b.StatusHistory)
            .WithOne(sh => sh.Booking)
            .HasForeignKey(sh => sh.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Status>()
            .HasMany<Booking>()
            .WithOne(b => b.Status)
            .HasForeignKey(b => b.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Status>()
            .HasMany<BookingStatusHistory>()
            .WithOne(bsh => bsh.Status)
            .HasForeignKey(bsh => bsh.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Data seeding
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "Admin" },
            new Role { Id = Guid.NewGuid(), Name = "Staff" },
            new Role { Id = Guid.NewGuid(), Name = "Lecturer" },
            new Role { Id = Guid.NewGuid(), Name = "Student" }
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = Guid.NewGuid(), Name = "Pending" },
            new Status { Id = Guid.NewGuid(), Name = "Approved" },
            new Status { Id = Guid.NewGuid(), Name = "Rejected" },
            new Status { Id = Guid.NewGuid(), Name = "Cancelled" }
        );
    }

    private void ApplyChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is BaseEntity))
        {
            var entity = (BaseEntity)entry.Entity;

            // CreatedAt 
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = entity.CreatedAt == default ? now : entity.CreatedAt;
            }

            // UpdatedAt
            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
            }

            // Soft Delete
            if (entry.State == EntityState.Deleted)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = now;
                entry.State = EntityState.Modified;
            }
        }
    }

    // Override SaveChanges methods to apply changes
    public override int SaveChanges()
    {
        ApplyChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }



    public AppDbContext() { }
}