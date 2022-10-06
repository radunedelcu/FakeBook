
using FakeBook.Application.Common.Interfaces;
using FakeBook.Contracts.Commands;
using FakeBook.Domain.Entities;
using FakeBook.Domain.Models.Requests.Commands.Message;
using FakeBook.Domain.Models.Responses.Queries.Friend;
using FakeBook.Domain.Models.Responses.Queries.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Application.Handlers.Commads {
  public class MessageCommand : IMessageCommand {
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MessageCommand(IApplicationDbContext applicationDbContext,
                          IWebHostEnvironment webHostEnvironment) {
      _applicationDbContext = applicationDbContext;
      _webHostEnvironment = webHostEnvironment;
    }

    public async Task<int> UploadMessage(int userId, string message, IFormFile file) {
      var user = await _applicationDbContext.Users.FindAsync(userId);

      if (user == null) {
        throw new Exception($"User {userId} was not found.");
      }
      var fileName = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                         .Substring(0, 6)
                         .Replace("\\", "_")
                         .Replace("+", "-");
      string filePath = String.Empty;
      if (file != null) {
        string directoryPath =
            Path.Combine(_webHostEnvironment.ContentRootPath, "Resources/Images");
        filePath = Path.Combine(directoryPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create)) {
          file.CopyTo(stream);
        }
      }
      var newMessage =
          new MessageEntity() { UserId = userId, Message = message, CreatedDate = DateTime.Now,
                                ImagePath = file == null ? "" : filePath };

      _applicationDbContext.Messages.Add(newMessage);
      if (await _applicationDbContext.SaveChangesAsync() == 0) {
        throw new Exception("Not Saved");
      }

      return newMessage.Id;
    }

    public async Task<IEnumerable<ResponseMessageModel>> GetMessages(int userId) {
      var user = await _applicationDbContext.Users.FindAsync(userId);

      return await _applicationDbContext.Messages.Where(f => f.UserId == userId)
          .Select(f => new ResponseMessageModel() { Message = f.Message, CreatedDate = DateTime.Now,
                                                    ImagePath = f.ImagePath, Name = f.User.Name })
          .OrderBy(f => f.CreatedDate)
          .ToListAsync();
    }

    public async Task<MessageEntity?> GetMessageForUser(int userId) {
      return await _applicationDbContext.Messages.Where(m => m.UserId == userId)
          .FirstOrDefaultAsync();
    }

    public async Task EditMessage(int userId,
                                  int messageId,
                                  JsonPatchDocument<RequestMessageUpdateModel> patch,
                                  IFormFile? file) {
      var messageEntity = await GetMessageForUser(userId);
      if (messageEntity == null) {
        throw new Exception("The message was not found.");
      }

      var message = new RequestMessageUpdateModel();
      message.Message = messageEntity.Message;
      if (messageEntity.ImagePath == null) {
        var fileName = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                           .Substring(0, 6)
                           .Replace("\\", "_")
                           .Replace("+", "-");
        string filePath = String.Empty;
        if (file != null) {
          string directoryPath =
              Path.Combine(_webHostEnvironment.ContentRootPath, "Resources/Images");
          filePath = Path.Combine(directoryPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create)) {
            file.CopyTo(stream);
          }
          message.ImagePath = file == null ? "" : filePath;
        } else {
          message.ImagePath = messageEntity.ImagePath;
        }

        patch.ApplyTo(message);
        messageEntity.Message = message.Message;
        messageEntity.ImagePath = message.ImagePath;
        await _applicationDbContext.SaveChangesAsync();
      }
    }
  }
}
