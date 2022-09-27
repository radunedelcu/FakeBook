using FakeBook.Application.Common.Interfaces;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Infrastructure.Data {
  public class ApplicationDbContext : DbContext, IApplicationDbContext {
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Role { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<FriendEntity> Friends { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      foreach (RoleEnum e in Enum.GetValues(typeof(RoleEnum))) {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { Id = (int)e, Name = e.ToString() });
      }
      modelBuilder.Entity<FriendEntity>()
          .HasOne(s => s.User)
          .WithMany(s => s.Friends)
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
