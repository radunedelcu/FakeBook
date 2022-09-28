using FakeBook.Domain.Models.Responses.Queries.Friend;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands {
  public interface IFriendCommand {
    Task AddFriend(int userId, int friendId);
    Task AcceptFriendRequest(int userId, int requesterId);

    Task DeleteFriend(int userId, int friendId);
    Task<IEnumerable<ResponseFriendModel>> GetFriends(int id);
  }
}
