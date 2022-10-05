using FakeBook.Application.Handlers.Commads;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Models.Requests.Commands.Message;
using FakeBook.Domain.Models.Responses.Queries.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    [HttpPatch("EditMessage")]
    public async Task<ActionResult> EditMessage(
        int messageId,
        JsonPatchDocument<RequestMessageUpdateModel> patch) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return NotFound();
      }
      var newData = Convert.ToInt32(data.Value);

      var messageEntity = await _messageCommand.GetMessageForUser(newData);
      if (messageEntity == null) {
        return NotFound();
      }

      var message = new RequestMessageUpdateModel();
      message.Message = messageEntity.Message;
      if (messageEntity.ImagePath != null) {
        message.ImagePath = messageEntity.ImagePath;
      }

      patch.ApplyTo(message);
      messageEntity.Message = message.Message;
      messageEntity.ImagePath = message.ImagePath;
      await _messageCommand.SaveChangesAsync();
      return NoContent();
    }

    [HttpGet("GetMessages")]
    [Authorize]
    public async Task<IEnumerable<ResponseMessageModel>> GetMessages() {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return new List<ResponseMessageModel>();
      }
      var newData = Convert.ToInt32(data.Value);

      return await _messageCommand.GetMessages(newData);
    }

    [HttpGet("GetFriendMessages")]
    public async Task<IEnumerable<ResponseMessageModel>> GetFriendMessages() {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return new List<ResponseMessageModel>();
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
