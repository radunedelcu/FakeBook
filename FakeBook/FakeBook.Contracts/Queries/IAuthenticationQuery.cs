using FakeBook.Domain.Models.Requests.Queries.Authentication;
using FakeBook.Domain.Models.Responses.Queries.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBook.Contracts.Queries {
  public interface IAuthenticationQuery {
    Task<ResponseLoginModel> LoginIfUserExists(RequestLoginModel requestLoginModel);
  }
}
