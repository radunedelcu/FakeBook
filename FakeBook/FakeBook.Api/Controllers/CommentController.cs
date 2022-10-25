using FakeBook.Contracts.Commands;
using FakeBook.Domain.Models.Requests.Commands.Comment;
using FakeBook.Domain.Models.Responses.Queries.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FakeBook.Api.Controllers {
  [Route("api/message/{messageId}/[controller]")]
  [ApiController]
  public class CommentController : ControllerBase {
    private readonly ICommentCommand _commentCommand;
    private readonly IMessageCommand _messageCommand;
    public CommentController(ICommentCommand commentCommand, IMessageCommand messageCommand) {
      _commentCommand = commentCommand;
      _messageCommand = messageCommand;
    }
    [HttpPost("AddComment")]
    [Authorize]
    public async Task<ActionResult> AddComment(int messageId, RequestCommentModel comment) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return null;
      }
      var newData = Convert.ToInt32(data.Value);
      await _commentCommand.AddComment(newData, messageId, comment.Content);
      return Ok();
    }

    [HttpDelete("DeleteComment/{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId) {
      var data = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (data == null) {
        return Unauthorized();
      }
      var newData = Convert.ToInt32(data.Value);

      if (await _commentCommand.DeleteComment(newData, commentId) == true) {
        return Ok();
      } else
        return Problem();
    }
  }
}
