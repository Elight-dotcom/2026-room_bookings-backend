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
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Room>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<Booking>().HasQueryFilter(b => !b.IsDeleted);
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
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Staff" },
            new Role { Id = 3, Name = "Lecturer" },
            new Role { Id = 4, Name = "Student" }
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "Pending" },
            new Status { Id = 2, Name = "Approved" },
            new Status { Id = 3, Name = "Rejected" },
            new Status { Id = 4, Name = "Cancelled" }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Name = "SAW 01.01", Location = "SAW", Capacity = 120, Description = "projector, whiteboard, speaker", IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Room { Id = 2, Name = "C 201", Location = "D4", Capacity = 90, Description = "projector, whiteboard", IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Room { Id = 3, Name = "HH 101", Location = "D3", Capacity = 30, Description = "projector, whiteboard", IsAvailable = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Room { Id = 4, Name = "HH 102", Location = "D3", Capacity = 30, Description = "whiteboard", IsAvailable = false, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@pens.ac.id", Password = "password", RoleId = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new User { Id = 2, Name = "Egit", Email = "egit@pens.ac.id", Password = "password", RoleId = 2, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new User { Id = 3, Name = "Saki", Email = "saki@pens.ac.id", Password = "password", RoleId = 3, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new User { Id = 4, Name = "Toriq", Email = "toriq@pens.ac.id", Password = "password", RoleId = 4, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
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