using FakeBook.Application.Common.Interfaces;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads {
  public class MessageCommand : IMessageCommand {
    private readonly IApplicationDbContext _applicationDbContext;

    public MessageCommand(IApplicationDbContext applicationDbContext) {
      _applicationDbContext = applicationDbContext;
    }
    public async Task UploadPhoto(int messageId, IFormFile file) {
      var image = new ImageEntity();
      var message = await _applicationDbContext.Users.FindAsync(messageId);
      if (message == null) {
        throw new Exception($"User {messageId} was not found.");
      }
      if (file != null) {
        if (file.Length > 0) {
          var fileName = Path.GetFileName(file.FileName);
          var fileExtension = Path.GetExtension(fileName);
          var newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
          image.Name = newFileName;
          image.FileType = fileExtension;
          image.MessageId = messageId;
          using (var target = new MemoryStream()) {
            file.CopyTo(target);
            image.DataFile = target.ToArray();
          }
          _applicationDbContext.Images.Add(image);
          await _applicationDbContext.SaveChangesAsync();
        }
      } else
        throw new Exception("There was a problem when uploading");
    }

    public async Task<int> UploadMessage(int userId, string message) {
      var user = await _applicationDbContext.Users.FindAsync(userId);
      if (user == null) {
        throw new Exception($"User {userId} was not found.");
      }

      var newMessage =
          new MessageEntity() { UserId = userId, Message = message, CreatedDate = DateTime.Now };

      _applicationDbContext.Messages.Add(newMessage);
      if (await _applicationDbContext.SaveChangesAsync() == 0) {
        throw new Exception("Not Saved");
      }

      return newMessage.Id;
    }
  }
}
