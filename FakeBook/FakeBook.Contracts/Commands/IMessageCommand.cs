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
    Task<MessageEntity?> GetMessageForUser(int userId);
    Task EditMessage(int userId, int messageId, JsonPatchDocument<RequestMessageUpdateModel> patch);
  }
}
