using FakeBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Common.Interfaces {
  public interface IApplicationDbContext {
    DbSet<UserEntity> Users { get; set; }
    DbSet<RoleEntity> Role { get; set; }
    DbSet<MessageEntity> Messages { get; set; }
    DbSet<FriendEntity> Friends { get; set; }
    DbSet<CommentEntity> Comments { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
  }
}
