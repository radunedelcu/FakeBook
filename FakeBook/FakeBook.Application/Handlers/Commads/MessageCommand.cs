using FakeBook.Application.Common.Interfaces;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Models.Responses.Queries.Friend;
using FakeBook.Domain.Models.Responses.Queries.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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

      var newMessage = new MessageEntity() {
        UserId = userId,
        Message = message,
        CreatedDate = DateTime.Now,
      };

      _applicationDbContext.Messages.Add(newMessage);
      if (await _applicationDbContext.SaveChangesAsync() == 0) {
        throw new Exception("Not Saved");
      }

      return newMessage.Id;
    }

    public async Task<IEnumerable<ResponseMessageModel>> GetMessages(int userId) {
      var user = await _applicationDbContext.Users.FindAsync(userId);

      return await _applicationDbContext.Messages.Where(f => f.UserId == userId)
          .Join(_applicationDbContext.Images, m => m.Id, i => i.MessageId,
                (m, i) =>
                    new ResponseMessageModel() { Message = m.Message, CreatedDate = m.CreatedDate,
                                                 Image = i.DataFile, FileType = i.FileType })
          .ToListAsync();
    }
  }
}
