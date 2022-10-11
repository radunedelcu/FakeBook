using FakeBook.Domain.Models.Requests.Commands.Authentication;
using FakeBook.Domain.Models.Responses.Commands.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Commands {
  public interface IAuthenticationCommand {
    Task<ResponseRegisterModel> Register(RequestRegisterModel requestRegisterModel);
  }
}
