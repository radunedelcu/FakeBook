using FakeBook.Domain.Entities;
using FakeBook.Domain.Models.Requests.Commands.Message;
using FakeBook.Domain.Models.Responses.Queries.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands {
  public interface IMessageCommand {
    Task<int> UploadMessage(int userId, string message, IFormFile? file);
    Task<IEnumerable<ResponseMessageModel>> GetMessages(int userId);
    Task<MessageEntity?> GetMessage(int messageId, int userId);
    Task<bool> EditMessage(int userId, int messageId, string message, IFormFile image);
    Task<bool> DeleteMessage(int messageId, int userId);
  }
}
