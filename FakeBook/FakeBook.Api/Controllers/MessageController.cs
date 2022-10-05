using FakeBook.Application.Handlers.Commads;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Models.Requests.Commands.Message;
using FakeBook.Domain.Models.Responses.Queries.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FakeBook.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class MessageController : ControllerBase {
    private readonly IMessageCommand _messageCommand;
    private readonly IFriendCommand _friendCommand;

    public MessageController(IMessageCommand messageCommand, IFriendCommand friendCommand) {
      _messageCommand = messageCommand;
      _friendCommand = friendCommand;
    }

    [HttpPost("UploadMessage")]
    [Authorize]
    public async Task PostMessage([FromForm] RequestMessageModel requestMessageModel) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return;
      }
      var newData = Convert.ToInt32(data.Value);

      var messageId = await _messageCommand.UploadMessage(newData, requestMessageModel.Message,
                                                          requestMessageModel.Image);
    }
    [HttpGet("GetMessages")]
    [Authorize]
    public async Task<IEnumerable<ResponseMessageModel>> GetMessages() {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return null;
      }
      var newData = Convert.ToInt32(data.Value);

      return await _messageCommand.GetMessages(newData);
    }

    [HttpGet("GetFriendMessages")]
    public async Task<IEnumerable<ResponseMessageModel>> GetFriendMessages() {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return null;
      }
      var newData = Convert.ToInt32(data.Value);

      var messageList = (List<ResponseMessageModel>)await _messageCommand.GetMessages(newData);
      var friendList = await _friendCommand.GetFriends(newData);

      foreach (var friend in friendList) {
        messageList.AddRange(await _messageCommand.GetMessages(friend.Id));
      }
      return messageList.OrderByDescending(f => f.CreatedDate);
    }
  }
}
