using FakeBook.Application.Common.Interfaces;
using FakeBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Infrastructure.Data {
  public class ApplicationDbContext : DbContext, IApplicationDbContext {
    public DbSet<UserEntity> Users { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
  }
}
