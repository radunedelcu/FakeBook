
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
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
    public string UploadImage(IFormFile file) {
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
      return filePath;
    }
    public async Task<int> UploadMessage(int userId, string message, IFormFile file) {
      var user = await _applicationDbContext.Users.FindAsync(userId);

      if (user == null) {
        throw new Exception($"User {userId} was not found.");
      }

      var filePath = UploadImage(file);
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

    public async Task<MessageEntity?> GetMessage(int messageId, int userId) {
      return await _applicationDbContext.Messages
          .Where(m => m.Id == messageId && m.UserId == userId)
          .FirstOrDefaultAsync();
    }

    public async Task<bool> EditMessage(int userId,
                                        int messageId,
                                        string message,
                                        IFormFile image) {
      var messageEntity = await GetMessage(messageId, userId);
      if (messageEntity == null) {
        throw new Exception("The message was not found.");
      }
      // string filePath = string.Empty;
      if (image != null) {
        var filePath = UploadImage(image);
        messageEntity.ImagePath = filePath;
        _applicationDbContext.Messages.Update(messageEntity);
      }
      if (message != null) {
        messageEntity.Message = message;
      }

      // message.Message = messageEntity.Message;
      // message.ImagePath = messageEntity.ImagePath;

      return await _applicationDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteMessage(int messageId, int userId) {
      var message = await GetMessage(messageId, userId);
      if (message == null) {
        return false;
      }

      _applicationDbContext.Messages.Remove(message);
      return await _applicationDbContext.SaveChangesAsync() > 0;
    }
  }
}
