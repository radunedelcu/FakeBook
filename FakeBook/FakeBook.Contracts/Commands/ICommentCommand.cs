using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands {
  public interface ICommentCommand {
    Task AddComment(int userId, int messageId, string content);
    Task<bool> DeleteComment(int userId, int messageId);
  }
}
