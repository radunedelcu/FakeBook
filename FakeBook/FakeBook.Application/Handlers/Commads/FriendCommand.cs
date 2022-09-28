using FakeBook.Application.Common.Interfaces;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads {
  public class FriendCommand : IFriendCommand {
    private readonly IApplicationDbContext _applicationDbContext;

    public FriendCommand(IApplicationDbContext applicationDbContext) {
      _applicationDbContext = applicationDbContext;
    }

    public async Task AddFriend(int userId, int friendId) {
      if (userId == friendId) {
        throw new Exception("You can't add yourself as a friend");
      }
      var user = await _applicationDbContext.Users.FindAsync(userId);
      if (user == null) {
        throw new Exception($"User {userId} was not found.");
      }
      var friend = await _applicationDbContext.Users.FindAsync(friendId);
      if (friend == null) {
        throw new Exception($"User {friendId} was not found.");
      }
      var isExist = await _applicationDbContext.Friends.AnyAsync(
          a => (a.User1Id == userId && a.User2Id == friendId) ||
               (a.User1Id == friendId && a.User2Id == userId));
      if (!isExist) {
        _applicationDbContext.Friends.Add(new FriendEntity() { User1Id = userId, User2Id = friendId,
                                                               CreatedDate = DateTime.Now });
        if (await _applicationDbContext.SaveChangesAsync() == 0) {
          throw new Exception("Not Saved");
        }
      }
    }
    // public async Task AcceptFriendRequest(int userId, int requesterId) {
    //   var user = await _applicationDbContext.Users.FindAsync(userId);
    //   if (user == null) {
    //     throw new Exception($"User {userId} was not found.");
    //   }
    //   var requester = await _applicationDbContext.Users.FindAsync(requesterId);
    //   if (requester == null) {
    //     throw new Exception($"User {requesterId} was not found.");
    //   }
    //   var isExist = await _applicationDbContext.Friends.AnyAsync(
    //       a => (a.User1Id == userId && a.User2Id == requesterId) ||
    //            (a.User1Id == requesterId && a.User2Id == userId));
    //   if (!isExist) {
    //     throw new Exception($"There is no friend request between {userId} and {requesterId} ");
    //   }
    //   _applicationDbContext.Friends.
    // }
  }
}
