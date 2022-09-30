using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands {
  public interface IMessageCommand {
    Task<int> UploadMessage(int userId, string message);
    Task UploadPhoto(int messageId, IFormFile file);
  }
}
