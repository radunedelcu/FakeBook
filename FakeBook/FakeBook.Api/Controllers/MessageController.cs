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

    public MessageController(IMessageCommand messageCommand) { _messageCommand = messageCommand; }

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
    public async Task<IEnumerable<ResponseMessageModel>> GetMessages(int userId) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return null;
      }
      var newData = Convert.ToInt32(data.Value);

      return await _messageCommand.GetMessages(newData);
    }
  }
}
