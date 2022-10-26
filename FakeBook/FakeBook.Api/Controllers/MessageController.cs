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
    [HttpPost("EditMessage/{messageId}")]
    [Authorize]
    public async Task<IActionResult> EditMessage([FromForm] RequestMessageUpdateModel newPost) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return NotFound();
      }
      var newData = Convert.ToInt32(data.Value);

      await _messageCommand.EditMessage(newData, newPost.MessageId, newPost.Message, newPost.Image);
      return NoContent();
    }

    [HttpGet("GetMessages/{userId}")]
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

    [HttpDelete("DeleteMessage/{messageId}")]
    [Authorize]
    public async Task<IActionResult> DeleteMessage(int messageId) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return NotFound();
      }
      var newData = Convert.ToInt32(data.Value);
      if (await _messageCommand.DeleteMessage(messageId, newData) == true) {
        return Ok();
      } else
        return Problem();
    }

    [HttpGet("{messageId}")]
    public async Task<ResponseMessageModel> GetMessage(int messageId) {
      var message = await _messageCommand.GetMessage(messageId);
      if (message == null) {
        throw new Exception("Message not found");
      }
      return new ResponseMessageModel() { Name = message.User.Name, Message = message.Message,
                                          ImagePath = message.ImagePath,
                                          CreatedDate = message.CreatedDate,
                                          UserProfilePicture = message.User.ProfilePicture };
    }
  }
}
