using FakeBook.Application.Handlers.Commads;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Models.Responses.Queries.Friend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FakeBook.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class FriendController : ControllerBase {
    private readonly IFriendCommand _friendCommand;

    public FriendController(IFriendCommand friendCommand) { _friendCommand = friendCommand; }

    [HttpPost("AddFriend")]
    [Authorize]
    public async Task<ActionResult> AddFriend(int id) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      if (data == null) {
        return NotFound();
      }

      var newData = Convert.ToInt32(data.Value);
      await _friendCommand.AddFriend(newData, id);

      return NoContent();
    }

    [HttpPost("AcceptFriendRequest")]
    [Authorize]
    public async Task<ActionResult> AcceptFriendRequest(int requesterId) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      if (data == null) {
        return NotFound();
      }

      var newData = Convert.ToInt32(data.Value);
      await _friendCommand.AcceptFriendRequest(newData, requesterId);

      return NoContent();
    }

    [HttpDelete("DeleteFriend")]
    [Authorize]
    public async Task<ActionResult> DeleteFriend(int friendId) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      if (data == null) {
        return NotFound();
      }

      var newData = Convert.ToInt32(data.Value);
      await _friendCommand.DeleteFriend(newData, friendId);

      return NoContent();
    }

    [HttpGet("GetFriends")]
    [Authorize]

    public async Task<IEnumerable<ResponseFriendModel>> GetFriends() {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      if (data == null) {
        return null;
      }

      var newData = Convert.ToInt32(data.Value);
      return await _friendCommand.GetFriends(newData);
    }
  }
}
