using FakeBook.Domain.Models.Requests.Commands.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands {
  public interface IAuthenticationCommand {
    Task<string> Register(RequestRegisterModel requestRegisterModel);
  }
}
