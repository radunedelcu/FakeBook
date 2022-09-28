using FakeBook.Application.Handlers.Commads;
using FakeBook.Contracts.Commands;
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

    [HttpPost()]
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
  }
}
