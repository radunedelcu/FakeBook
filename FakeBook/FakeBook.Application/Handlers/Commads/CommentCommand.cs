using FakeBook.Application.Common.Interfaces;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads {
  public class CommentCommand : ICommentCommand {
    private readonly IApplicationDbContext _applicationDbContext;

    public CommentCommand(IApplicationDbContext applicationDbContext) {
      _applicationDbContext = applicationDbContext;
    }

    public async Task AddComment(int userId, int messageId, string content) {
      var user = await _applicationDbContext.Users.FindAsync(userId);
      var message = await _applicationDbContext.Messages.FindAsync(messageId);
      if (message == null) {
        throw new Exception($"Message {messageId} was not found.");
      }
      if (user == null) {
        throw new Exception($"User {userId} was not found.");
      }
      List<CommentEntity> comments =
          _applicationDbContext.Comments.Where(c => c.MessageId == messageId).ToList();
      message.Comments = comments;
      _applicationDbContext.Comments.Add(
          new CommentEntity() { UserId = userId, MessageId = messageId, Content = content });
      if (await _applicationDbContext.SaveChangesAsync() == 0) {
        throw new Exception("Not Saved");
      }
    }

    public async Task<bool> DeleteComment(int userId, int commentId) {
      var comment =
          await _applicationDbContext.Comments.Where(c => c.Id == commentId && c.UserId == userId)
              .FirstOrDefaultAsync();
      if (comment == null) {
        throw new Exception($"Comment {commentId} not found");
      }

      _applicationDbContext.Comments.Remove(comment);
      return await _applicationDbContext.SaveChangesAsync() > 0;
    }
  }
}
