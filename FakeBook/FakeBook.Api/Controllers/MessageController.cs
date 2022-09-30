using FakeBook.Contracts.Commands;
using FakeBook.Domain.Models.Requests.Commands.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FakeBook.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class MessageController : ControllerBase {
    private readonly IMessageCommand _messageCommand;

    public MessageController(IMessageCommand messageCommand) { _messageCommand = messageCommand; }

    [HttpPost("UploadMessage")]
    [Authorize]
    public async Task PostMessage(RequestMessageModel requestMessageModel) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return;
      }
      var newData = Convert.ToInt32(data.Value);

      var messageId = await _messageCommand.UploadMessage(newData, requestMessageModel.Message);
      await _messageCommand.UploadPhoto(messageId, requestMessageModel.Image);
    }
  }
}
